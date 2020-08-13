using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Friday.Data.IServices;
using Friday.Models;
using Friday.Models.Logs;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Friday.Data.ServiceInstances
{
    public class ItemService : IItemService
    {

        //private IList<Item> list;
        //#TODO Inject context
        private readonly Context context;
        private readonly DbSet<Item> items;
        private readonly DbSet<ItemLog> logs;

        public ItemService(Context context)
        {
            this.context = context;
            items = this.context.Items;
            logs = this.context.ItemLogs;

        }
        public IList<Item> GetAll()
        {
            var result = items.Include(s => s.ItemDetails).AsNoTracking().ToList();
            return result;
        }
        ///// <summary>
        ///// Returns the details of a specified Item
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns>ItemDetails, null if not found</returns>
        //public ItemDetails GetDetails(int id) {
        //    return details.AsNoTracking().SingleOrDefault(s => s.ItemId == id);
        //}
        /// <summary>
        /// Changes the Amount of an Item. Will Add the specified amount to the Amount. Will subtract if amount if negative. Addition/subtraction is needed to avoid concurrency issues.
        /// </summary>
        /// <param name="id">Id of the item</param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public bool ChangeCount(int id, int amount)
        {
            var item = items.SingleOrDefault(s => s.Id == id);
            if (item == null || (amount < 0 && Math.Abs(amount) > item.Count))//Avoid negative numbers
                return false;
            item.Count += amount;

            items.Update(item);

            context.SaveChanges();

            LogItem(item, amount);

            return true;
        }

        private void LogItem(Item item, int count)
        {
            var log = new ItemLog { Item = item, Amount = count, Time = DateTime.Now };
            logs.Add(log);
            context.SaveChanges();
        }

        public bool AddItem(Item item, ItemDetails details)
        {
            items.Add(item);//Add Item itself to data
            context.SaveChanges();//Save to generate the ID

            details.ItemId = item.Id;//Set ItemId with newly generated value from Item

            items.Update(item);//Sets the Item as updated to new Details are saved too
            context.SaveChanges();//Save to generated ID

            return item.Id != 0 && details.Id != 0 && items.Contains(item);//Check if Item was succesfully added and all values generated. This ensures proper saving.
        }

        public bool DeleteItem(int id)
        {
            var item = items.SingleOrDefault(s => s.Id == id);
            if (item == null)
                return false;
            items.Remove(item);
            return context.SaveChanges() != 0;//False if nothing was written and the operation failed.

        }

        public bool ChangeItem(Item item)
        {
            throw new NotImplementedException();
        }
    }
}
