using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friday.Models.Logs
{
    /// <summary>
    /// Contains the information of a single transaction involving Items. Tracks what Item was changed, the amount (negative for subtraction, positive for addition) and when it took place.
    /// </summary>
    public class ItemLog
    {
        public int Id { get; set; }
        public Item Item { get; set; }
        public int ItemId { get; set; }
        public int Amount { get; set; }
        public DateTime Time { get; set; }
    }
}
//TODO Add User that used transaction (customer for orders, admin for orders and inventory management)
