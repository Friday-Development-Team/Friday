﻿namespace Friday.Models.Out
{/// <summary>
/// Links an Item to an Amount (amount sold, amount remaining, used to make a Dictionary easier to send via JSON)
/// </summary>
    public class ItemAmountDTO
    {
        /// <summary>
        /// Item
        /// </summary>
        public string Item { get; set; }
        /// <summary>
        /// Amount of said Item
        /// </summary>
        public int Amount { get; set; }
    }
}
