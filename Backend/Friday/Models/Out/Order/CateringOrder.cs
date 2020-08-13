using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friday.Models.Out.Order
{
    /// <summary>
    /// Contains the data of an ongoing, not-completed Order. Used to give whoever handles the catering all the needed information.
    /// </summary>
    public class CateringOrder
    {
        /// <summary>
        /// ID of the Order
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// List of Items ordered, stored in an object that only exposes necessary information
        /// </summary>
        public IList<HistoryOrderItem> Items { get; set; }
        //Time of Order placement
        public DateTime OrderTime { get; set; }
        /// <summary>
        /// User that placed this Order
        /// </summary>
        public string User { get; set; }
        /// <summary>
        /// Status of the beverages
        /// </summary>
        public string StatusBeverage { get; set; }
        /// <summary>
        /// Status of the food
        /// </summary>
        public string StatusFood { get; set; }
        /// <summary>
        /// Total amount paid for the Order, precalculated for convenience.
        /// </summary>
        public double TotalPrice { get; set; }

    }
}
