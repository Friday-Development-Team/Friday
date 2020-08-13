using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Data.IServices;
using Friday.DTOs;
using Friday.Models;
using Friday.Models.Out;
using Friday.Models.Out.Order;
using Microsoft.EntityFrameworkCore;

namespace Friday.Data.ServiceInstances {
    public class OrderService : IOrderService {

        private readonly Context context;
        private readonly DbSet<Order> orders;
        private readonly DbSet<ShopUser> users;
        private readonly DbSet<Item> items;

        private readonly IItemService itemService;
        private readonly IUserService userService;

        public OrderService(Context context, IItemService itemsService, IUserService userService) {
            this.context = context;
            orders = context.Orders;
            users = context.ShopUsers;
            items = context.Items;
            this.itemService = itemsService;
            this.userService = userService;
        }
        /// <inheritdoc />
        public OrderHistory GetHistory(string username) {
            if (users.Single(s => s.Name == username) == null)
                return null;

            return new OrderHistory
            {
                UserName = username,
                Orders = orders.Include(s => s.Items).Include(s => s.User)
                    .Where(s => s.User.Name == username)
                    .Where(s => (s.StatusBeverage == OrderStatus.Completed || s.StatusBeverage == OrderStatus.None) &&
                                (s.StatusFood == OrderStatus.Completed || s.StatusFood == OrderStatus.None))
                    .OrderBy(s => s.OrderTime)
                    .Select(s =>
                        new HistoryOrder
                        {
                            OrderTime = s.OrderTime,
                            CompletionTimeBeverage = s.CompletionTimeBeverage,
                            CompletionTimeFood = s.CompletionTimeFood,
                            TotalPrice = s.Items.Select(t => t.Amount * t.Item.Price).Sum(),
                            Items = s.Items.Select(t => new HistoryOrderItem { ItemName = t.Item.Name, Amount = t.Amount }).ToList()
                        })
                    .ToList(),

            };

        }
        /// <inheritdoc />
        public bool SetAccepted(int id, bool value, bool toKitchen) {

            var needsKitchen = context.Configuration.Single();

            var changed = value ? (toKitchen && !needsKitchen.CombinedCateringKitchen ? OrderStatus.SentToKitchen : OrderStatus.Accepted) : OrderStatus.Pending;
            var item = orders.SingleOrDefault(s => s.Id == id);

            if (item == null || item.StatusBeverage == OrderStatus.Completed || item.StatusFood == OrderStatus.Completed || !item.IsOngoing() ||
                (item.StatusFood == OrderStatus.None && toKitchen))
                return false;



            if (item.StatusFood != OrderStatus.None)
                item.StatusFood = changed;
            if (item.StatusBeverage != OrderStatus.None)
                item.StatusBeverage = value ? OrderStatus.Accepted : OrderStatus.Pending;

            orders.Update(item);
            context.SaveChanges();
            return true;
        }
        /// <inheritdoc />
        public int PlaceOrder(string username, OrderDTO orderdto) {
            if (orderdto == null || !orderdto.IsValid())
                return 0;

            var user = users.SingleOrDefault(s => s.Name == username);
            if (user == null)
                return 0;

            Order order = new Order
            {
                User = user,
                UserId = user.Id,
                OrderTime = DateTime.Now
            };

            var orderitems = orderdto.Items.Select(s =>
                    new OrderItem
                    {
                        Order = order,
                        Item = items.SingleOrDefault(t => t.Id == s.Id),
                        Amount = s.Amount
                    })
                .ToList();
            var totalPrice = orderitems.Select(s => (s.Amount * s.Item.Price)).Sum();

            if (!user.HasBalance(totalPrice))
                return 0;

            var hasBev = orderitems.Any(s => s.Item.Type == ItemType.Beverage);
            var hasFood = orderitems.Any(s => s.Item.Type == ItemType.Food);

            order.StatusBeverage = (hasBev ? OrderStatus.Pending : OrderStatus.None);
            order.StatusFood = (hasFood ? OrderStatus.Pending : OrderStatus.None);

            order.Items = orderitems;
            // IList<Item> rem = new List<Item>();
            Dictionary<Item, int> log = new Dictionary<Item, int>();
            foreach (var item in orderdto.Items) {
                var temp = items.SingleOrDefault(s => s.Id == item.Id);
                if (temp == null)
                    return 0;

                //temp.Amount -= item.Amount;
                if (itemService.ChangeCount(temp.Id, -item.Amount)) //Should it fail, the item is rejected. This will be notified to the catering. This will not affect the Users balance.
                    log.Add(temp, -item.Amount);
                else
                    RevertUser(user, temp.Count * temp.Price);//Refunds the failed item. It will not show up in the history.


                items.Update(temp);//Updated Item
            }

            var result = orders.Add(order);//Order added
            //user.UpdateBalance(-totalPrice);

            //users.Update(user);//Updated user

            context.SaveChanges();

            userService.ChangeBalance(user.Name, -totalPrice, true);

            return result.Entity.Id;
        }


        private void RevertUser(ShopUser user, double amount) {
            userService.ChangeBalance(user.Id, amount, false);
            user.UpdateBalance(amount);
        }

        /// <inheritdoc />
        public bool SetCompleted(int id, bool forBeverage) {
            var order = orders.SingleOrDefault(s => s.Id == id);
            if (order == null || (forBeverage ? order.StatusBeverage != OrderStatus.Accepted : order.StatusFood != OrderStatus.Accepted))//Only accepted orders can be completed. SentToKitchen has to return them to Catering.
                return false;

            if (forBeverage) {
                order.StatusBeverage = OrderStatus.Completed;
                order.CompletionTimeBeverage = DateTime.Now;
            } else {
                order.StatusFood = OrderStatus.Completed;
                order.CompletionTimeFood = DateTime.Now;
            }

            orders.Update(order);
            context.SaveChanges();
            return true;
        }
        /// <inheritdoc />
        public bool Cancel(int id) {//#TODO Config for option to allow accepted orders to be cancelled
            var order = orders.SingleOrDefault(s => s.Id == id);
            if (order == null || !order.CanBeCancelled(context.Configuration.Single().CancelOnAccepted))
                return false;
            order.StatusBeverage = OrderStatus.Cancelled;
            order.StatusFood = OrderStatus.Cancelled;
            orders.Update(order);
            context.SaveChanges();
            return true;
        }
        /// <inheritdoc />
        public string GetStatus(int id) {
            var order = orders.SingleOrDefault(s => s.Id == id);
            return order?.StatusBeverage.ToString() + order?.StatusFood.ToString();
        }
        /// <inheritdoc />
        public IList<CateringOrder> GetAll(bool isKitchen) {
            var result = orders
                .Include(s => s.Items)
                .ThenInclude(s => s.Item)
                .ThenInclude(s => s.ItemDetails)
                .Include(s => s.User)
                .Where(s => (isKitchen ? s.StatusFood == OrderStatus.SentToKitchen : s.IsOngoing()))
                .OrderBy(s => (int)s.StatusBeverage).ThenBy(s => (int)s.StatusFood).ThenBy(s => s.OrderTime).AsNoTracking()
                .Select(s => new CateringOrder
                {
                    Id = s.Id,
                    StatusBeverage = isKitchen ? null : s.StatusBeverage.ToString(),
                    StatusFood = s.StatusFood.ToString(),
                    User = s.User.Name,
                    Items = s.Items.Select(t => new HistoryOrderItem { Amount = t.Amount, ItemName = t.Item.Name })
                        .ToList(),
                    OrderTime = s.OrderTime,
                    TotalPrice = s.Items.Sum(t => t.Amount * t.Item.Price)
                })
                .ToList();

            return result;
        }
    }
}
