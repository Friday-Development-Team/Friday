using System.Collections.Generic;
using Friday.Models;

namespace Friday.Data.IServices
{
    public interface IItemService
    {

        /// <summary>
        /// Retrieves a List of all Item objects.
        /// </summary>
        /// <returns>List of all Items</returns>
        IList<Item> GetAll();

        /// <summary>
        /// Adds a new Item. ID will be generated.
        /// </summary>
        /// <param name="item">New Item object</param>
        /// <param name="details">ItemDetails object linked to the Item. </param>
        /// <returns>True if the change was successful</returns>
        bool AddItem(Item item, ItemDetails details);
        /// <summary>
        /// Changes the count of a single Item. Works additively. Use a negative int to subtract. Used for convenience.
        /// </summary>
        /// <param name="id">ID of the Item</param>
        /// <param name="amount">Amount to add. Negative to subtract.</param>
        /// <returns>True if the change was successful</returns>
        bool ChangeCount(int id, int amount);
        /// <summary>
        /// Changes an Item.
        /// </summary>
        /// <param name="item">ID of the Item</param>
        /// <returns>True if the change was successful</returns>
        bool ChangeItem(Item item);
        /// <summary>
        /// Deletes an Item.
        /// </summary>
        /// <param name="id">ID of the Item</param>
        /// <returns>True if the change was successful</returns>
        bool DeleteItem(int id);
    }
}
