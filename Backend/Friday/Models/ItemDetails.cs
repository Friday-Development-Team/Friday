using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friday.Models

{
    /// <summary>
    /// Contains the details of an Item.
    /// </summary>
    public class ItemDetails
    {
        /// <summary>
        /// ID of this object
        /// </summary>
        public int Id { get; set; }
        public int ItemId { get; set; }
        /// <summary>
        /// How much one item is. Item is responsible for both amount and unit (33cl, 200g, 1 hot dog).
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        /// Calories per serving
        /// </summary>
        public double Calories { get; set; }
        /// <summary>
        /// Amount of sugar per serving
        /// </summary>
        public double SugarContent { get; set; }
        /// <summary>
        /// Amount of salt per serving
        /// </summary>
        public double SaltContent { get; set; }
        /// <summary>
        /// Known allergens in the item (nuts, milk, etc)
        /// </summary>
        public string Allergens { get; set; }

    }
}
