using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Data.IServices;
using Friday.Models;
using Friday.Models.Out;
using Microsoft.EntityFrameworkCore;

namespace Friday.Data.ServiceInstances {
    public class UserService : IUserService {

        private readonly Context context;
        private readonly DbSet<ShopUser> users;
        private readonly DbSet<CurrencyLog> logs;

        public UserService(Context context) {
            this.context = context;
            users = this.context.ShopUsers;
            logs = this.context.CurrencyLogs;
        }
        /// <inheritdoc/>
        public bool AddUser(ShopUser user) {
            if (user == null)
                return false;
            users.Add(user);
            context.SaveChanges();
            return true;
        }
        /// <inheritdoc/>
        public bool ChangeBalance(int id, double amount, bool log) {
            var user = users.SingleOrDefault(s => s.Id == id);
            if (!user.UpdateBalance(amount))
                return false;
            users.Update(user);
            context.SaveChanges();
            if (log)
                LogMoney(user, amount);
            return true;
        }

        /// <inheritdoc/>
        public bool ChangeBalance(string name, double amount, bool log) {
            var user = users.SingleOrDefault(s => s.Name.Equals(name));
            return user != null && ChangeBalance(user.Id, amount, log);
        }

        /// <inheritdoc/>
        public IList<ShopUserDTO> GetAll() {
            var list = users.AsNoTracking().Select(s => new ShopUserDTO { Name = s.Name, Balance = s.Balance }).ToList();
            return list;
        }
        /// <inheritdoc/>
        public ICollection<Order> GetOrderHistory(int id, DateTime orderTime) {
            var user = users.Single(s => s.Id == id);
            return user.Orders.Where(t => t.OrderTime == orderTime).ToList();
        }
        /// <inheritdoc/>
        public ShopUserDTO GetUser(string username) {
            var user = users.FirstOrDefault(t => t.Name == username);
            return user != null ? new ShopUserDTO { Name = user.Name, Balance = user.Balance } : null;
        }
        private void LogMoney(ShopUser user, double count) {
            var log = new CurrencyLog { User = user, Count = count, Time = DateTime.Now };
            logs.Add(log);
            context.SaveChanges();
        }
    }
}
