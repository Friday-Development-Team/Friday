using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friday.Models
{
    /// <summary>
    /// Contains the information of a single transaction involving currency. Tracks who made the transaction, the amount (negative for subtraction, positive for addition) and when it took place.
    /// </summary>
    public class CurrencyLog
    {
        public int Id { get; set; }
        public ShopUser User { get; set; }
        public int UserId { get; set; }
        public double Count { get; set; }
        public DateTime Time { get; set; }
    }
}
