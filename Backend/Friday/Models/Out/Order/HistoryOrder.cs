using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friday.Models.Out.Order
{
    /// <summary>
    /// Contains information about a completed Order. Used in giving a User their Order history. Can be used to give Admins a full overview of placed Orders, past and present.
    /// </summary>
    public class HistoryOrder
    {
        /// <summary>
        /// Total price of the Order
        /// </summary>
        public double TotalPrice { get; set; }
        /// <summary>
        /// Time the Order was placed
        /// </summary>
        public DateTime OrderTime { get; set; }
        /// <summary>
        /// Time all food was completed
        /// </summary>
        public DateTime CompletionTimeFood { get; set; }
        /// <summary>
        /// Time all beverages were completed
        /// </summary>
        public DateTime CompletionTimeBeverage { get; set; }
        /// <summary>
        /// List of ordered Items, stored in an object that only exposes necessary information
        /// </summary>
        public IList<HistoryOrderItem> Items { get; set; }
    }
}
