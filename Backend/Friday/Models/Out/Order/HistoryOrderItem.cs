using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friday.Models.Out.Order
{
    /// <summary>
    /// Links an Item's name to an amount ordered. Used to visually show how much of this Item was ordered.
    /// </summary>
    public class HistoryOrderItem
    {
        /// <summary>
        /// Name of the Item
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// Amount ordered.
        /// </summary>
        public int Amount { get; set; }
    }
}
