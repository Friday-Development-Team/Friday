using Friday.Data.IServices;
using Friday.Models;
using Friday.Models.Logs;
using Friday.Models.Out;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.DTOs;

namespace Friday.Data.ServiceInstances {
    public class LogsService : ILogsService {

        private readonly Context context;
        private readonly DbSet<CurrencyLog> currencyLogs;
        private readonly DbSet<ItemLog> itemLogs;
        private readonly DbSet<Item> items;

        public LogsService(Context ctext) {
            context = ctext;
            currencyLogs = context.CurrencyLogs;
            itemLogs = context.ItemLogs;
            items = context.Items;
        }

        /// <inheritdoc />
        public IList<LogDTO> GetAllCurrencyLogs() {
            return currencyLogs.Include(s => s.User).AsNoTracking().ToList().Select(s =>
                {
                    s.User = RemoveLogsAndOrders(s.User);
                    return s;
                }).Select(LogDTO.FromCurrencyLog).ToList();
        }
        /// <inheritdoc />
        public IList<LogDTO> GetAllItemLogs() {
            return itemLogs.Include(s => s.Item).AsNoTracking().ToList().Select(s =>
            {
                s.Item = RemoveLogsAndOrders(s.Item);
                return s;
            }).Select(LogDTO.FromItemLog).ToList();
        }
        /// <inheritdoc />
        public IList<LogDTO> GetByUser(string name) {
            return currencyLogs.Include(s => s.User).Where(s => s.User.Name == name).OrderBy(s => s.Time).ToList().Select(s =>
            {
                s.User = RemoveLogsAndOrders(s.User);
                return s;
            }).Select(LogDTO.FromCurrencyLog).ToList();
        }
        /// <inheritdoc />
        public IList<LogDTO> GetPerItem(int id) {
            return itemLogs.AsNoTracking().Include(s => s.Item).Where(s => s.Item.Id == id).ToList().Select(s =>
            {
                s.Item = RemoveLogsAndOrders(s.Item);
                return s;
            }).Select(LogDTO.FromItemLog).ToList();
        }
        /// <inheritdoc />
        public IList<ItemAmountDTO> GetRemainingStock() {
            return items.Select(s => new ItemAmountDTO { Item = s, Amount = s.Count }).ToList();
        }
        /// <inheritdoc />
        public double GetTotalIncome() {
            return currencyLogs.Where(s => s.Count > 0).Sum(s => s.Count);//Only positive amounts. Those logs mean that money was added, thus income.
        }
        /// <inheritdoc />
        public IList<ItemAmountDTO> GetTotalStockSold() {
            //var result = itemLogs.Where(s => s.Amount < 0).ToList();
            //return result.ToDictionary(s => s.Item, s => result.Where(t => t.Equals(s))).Select(s => new ItemAmountDTO { Item = s.Key, Amount = s.Value.Sum(t => t.Amount) }).ToList();
            return itemLogs.Where(s => s.Amount < 0).GroupBy(s => s.Item).Select(s => new ItemAmountDTO
            { Item = s.Key, Amount = s.Sum(t => Math.Abs(t.Amount)) }).ToList();
        }

        private dynamic RemoveLogsAndOrders(dynamic item) {
            var temp = item;
            temp.Logs = null;
            if (temp is ShopUser)
                temp.Orders = null;
            return temp;
        }
    }
}
