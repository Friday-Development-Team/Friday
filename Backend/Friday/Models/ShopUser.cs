using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friday.Models
{
    /// <summary>
    /// User object for the user using the system. Contains their name and balance.
    /// </summary>
    public class ShopUser
    {
        /// <summary>
        /// ID of the object
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Username of this User
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The amount of money this User has in their account and can use to place orders
        /// </summary>
        public double Balance { get; set; }
        /// <summary>
        /// List of Orders placed by this user. Used for EF. Set to null for serialization to JSON
        /// </summary>
        public ICollection<Order> Orders { get; set; }
        /// <summary>
        /// List of CurrencyLogs concerning this user. Used for EF. Set to null for serialization to JSON
        /// </summary>
        public ICollection<CurrencyLog> Logs { get; set; }


        /// <summary>
        /// Adds the specified amount to the User's Balance. Adding a negative amount is the same as a subtraction.
        /// </summary>
        /// <param name="amount">Amount to be added/subtracted</param>
        /// <returns>True if it was successful</returns>
        public bool UpdateBalance(double amount)
        {
            if (amount < 0 && Math.Abs(amount) > Balance)//If negative and would reduce balance below zero. 
                return false;
            var original = Balance;
            this.Balance += amount;
            return Balance != original;
        }

        /// <summary>
        /// Checks if the user has enough balance.
        /// </summary>
        /// <param name="amount">Amount</param>
        /// <returns>True if the User has enough Balance</returns>
        public bool HasBalance(double amount)
        {
            return Balance >= amount;
        }

    }
}
