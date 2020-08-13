using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friday.Models.Out
{/// <summary>
/// Links an Item to an Amount (amount sold, amount remaining, used to make a Dictionary easier to send via JSON)
/// </summary>
    public class ItemAmountDTO
    {
        public Item Item { get; set; }
        public int Amount { get; set; }
    }
}
