using Friday.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friday.DTOs.Items {
    public class ItemDetailsDTO {
        public string Size { get; set; }
        public double Calories { get; set; }
        public double SugarContent { get; set; }
        public double SaltContent { get; set; }
        public string Allergens { get; set; }

        public ItemDetails ToItemDetails() {
            return new ItemDetails { Size = Size, Calories = Calories, SugarContent = SugarContent, SaltContent = SaltContent, Allergens = Allergens };
        }
    }
}
