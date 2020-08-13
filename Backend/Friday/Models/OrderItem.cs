using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friday.Models
{
    /// <summary>
    /// Wrapper object linking an Order to an Item and an amount. Database and JSON-friendly alternative to a Map/Dictionary.
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// ID of the Order
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        /// The Order this belongs to
        /// </summary>
        public Order Order { get; set; }
        /// <summary>
        /// ID of the Item
        /// </summary>
        public int ItemId { get; set; }
        /// <summary>
        /// Item ordered
        /// </summary>

        public Item Item { get; set; }
        /// <summary>
        /// Amount of the Item that was ordered
        /// </summary>

        public int Amount { get; set; }
    }
}
