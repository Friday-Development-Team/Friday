using Friday.Models;
using Friday.Models.Logs;
using Friday.Models.Out;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.DTOs;

namespace Friday.Data.IServices
{
    public interface ILogsService
    {
        /// <summary>
        /// Gets the transactions made by a single User
        /// </summary>
        /// <param name="name">Name of User</param>
        /// <returns>All logs by the User</returns>
        IList<LogDTO> GetByUser(string name);
        /// <summary>
        /// Gets all transactions made involving a specific Item
        /// </summary>
        /// <param name="id">ID of the Item</param>
        /// <returns>All logs for the Item</returns>
        IList<LogDTO> GetPerItem(int id);
        /// <summary>
        /// Gets all logs made involving currency
        /// </summary>
        /// <returns>All currency logs</returns>
        IList<LogDTO> GetAllCurrencyLogs();
        /// <summary>
        /// Gets all logs involving Items
        /// </summary>
        /// <returns>All item logs</returns>
        IList<LogDTO> GetAllItemLogs();
        /// <summary>
        /// Gets the total amount made (currency transferred)
        /// </summary>
        /// <returns>All income</returns>
        double GetTotalIncome();
        /// <summary>
        /// Shows how much of each Item was sold
        /// </summary>
        /// <returns>How much of each item was sold</returns>
        IList<ItemAmountDTO> GetTotalStockSold();


        /// <summary>
        /// Shows how much stock is left
        /// </summary>
        /// <returns>How much of each item remains</returns>
        IList<ItemAmountDTO> GetRemainingStock();
    }
}
