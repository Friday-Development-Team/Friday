using System.Collections.Generic;
using Friday.DTOs;
using Friday.Models;
using Friday.Models.Out;
using Friday.Models.Out.Order;

namespace Friday.Data.IServices {
    public interface IOrderService {
        /// <summary>
        /// Returns all the completed Orders of a specified user.
        /// </summary>
        /// <param name="username">Username of the user</param>
        /// <returns>Object containing all the Orders</returns>
        OrderHistory GetHistory(string username);

        /// <summary>
        /// Sets the Accepted flag to the specified value.
        /// </summary>
        /// <param name="id">Id of the Order</param>
        /// <param name="value">New value</param>
        /// <returns>True if the value was correctly changed, false if the Order wasn't found or the old value was equal to the new value</returns>
        bool SetAccepted(int id, bool value, bool toKitchen);
        /// <summary>
        /// Places an Order. Checks if the Order is valid and can be placed.
        /// </summary>
        /// <param name="orderdto">DTO</param>
        /// <returns>True if the Order is valid and could be placed</returns>
        int PlaceOrder(string user, OrderDTO orderdto);
        /// <summary>
        /// Sets the Order status to completed.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if successful</returns>
        bool SetCompleted(int id, bool forBeverage);
        /// <summary>
        /// Cancels an Order. An Order can only be cancelled if it isn't Completed or, depending on configuration, not Accepted.
        /// </summary>
        /// <param name="id">Id of the Order</param>
        /// <returns>True if successful</returns>
        bool Cancel(int id);
        /// <summary>
        /// Returns the status of the specified Order
        /// </summary>
        /// <param name="id">Id of the Order</param>
        /// <returns>String representation of the Status of the Order</returns>
        string GetStatus(int id);
        /// <summary>
        /// Returns a List containing all the non-Completed and non-Cancelled Ordered. Accepted first, then ordered by date asc.
        /// </summary>
        /// <returns>List of all current orders</returns>
        IList<CateringOrder> GetAll(bool isKitchen);

    }
}
