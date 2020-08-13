using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friday.Models.Out
{
    /// <summary>
    /// Links a User's name to their balance. Used for showing and changing their balance without needing other information. Used to make a Dictionary easier to send via JSON.
    /// </summary>
    public class ShopUserDTO
    {
        public String Name { get; set; }
        public double Balance { get; set; }
    }
}
