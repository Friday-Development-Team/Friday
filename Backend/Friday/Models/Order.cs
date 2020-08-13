using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friday.Models
{
    /// <summary>
    /// Contains the information of a placed Order.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// ID of this object
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ID of the User that placed this Order
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// User that placed this Order
        /// </summary>
        public ShopUser User { get; set; }
        /// <summary>
        /// List of Items in this Order and the amounts, wrapped in an OrderItem
        /// </summary>
        public IList<OrderItem> Items { get; set; }
        /// <summary>
        /// When this Order was placed
        /// </summary>
        public DateTime OrderTime { get; set; }
        /// <summary>
        /// When the beverages were completed
        /// </summary>
        public DateTime CompletionTimeBeverage { get; set; }
        /// <summary>
        /// When the food was completed
        /// </summary>
        public DateTime CompletionTimeFood { get; set; }
        /// <summary>
        /// Current status of the beverages
        /// </summary>
        public OrderStatus StatusBeverage { get; set; }
        /// <summary>
        /// Current status of the food
        /// </summary>
        public OrderStatus StatusFood { get; set; }

        /// <summary>
        /// Check if the Order is still ongoing and not completed yet.
        /// </summary>
        /// <returns>True if the Order hasn't been fully completed yet</returns>
        public bool IsOngoing()
        {
            return StatusBeverage == OrderStatus.Pending || StatusBeverage == OrderStatus.Accepted ||
                   StatusFood == OrderStatus.SentToKitchen || StatusFood == OrderStatus.Pending ||
                   StatusFood == OrderStatus.Accepted;
        }

        /// <summary>
        /// Checks if the Order can still be cancelled
        /// </summary>
        /// <param name="OnAccept">Config option. Allow cancelling even when the Order has been accepted</param>
        /// <returns>True if the Order can be cancelled</returns>
        public bool CanBeCancelled(bool OnAccept)
        {
            return (StatusFood == OrderStatus.Pending && StatusBeverage == OrderStatus.Pending) || (OnAccept && StatusFood == OrderStatus.Accepted && StatusBeverage == OrderStatus.Accepted);
        }
    }
    /// <summary>
    /// Represents the Status of an Order
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// Order has been Accepted and is being handlead
        /// </summary>
        Accepted,//Should be first to be able to order all orders
        /// <summary>
        /// Order is placed but hasn't been accepted yet
        /// </summary>
        Pending,
        /// <summary>
        /// Order has been sent to Kitchen, awaiting release to Catering on completion
        /// </summary>
        SentToKitchen,
        /// <summary>
        /// Order has been completed 
        /// </summary>
        Completed,
        /// <summary>
        /// Order has been cancelled and cannot be processed in any way
        /// </summary>
        Cancelled,
        /// <summary>
        /// Signals that this Order has no Status. Only applicable when an Order only has beverages and no food or vice versa
        /// </summary>
        None
    }
}
