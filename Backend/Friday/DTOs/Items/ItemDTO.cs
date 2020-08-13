using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Models;
using Friday.Models.Logs;

namespace Friday.DTOs.Items {
    public class ItemDTO {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Type { get; set; }
        public int Count { get; set; }
        public ItemDetailsDTO Details { get; set; }
        public string ImageName { get; set; }

        public Item ToItem() {
            return new Item { Name = Name, Price = Price, Type = ItemTools.FromString(Type), NormalizedImageName = ImageName, Count = Count, Logs = new List<ItemLog>() };
        }
    }
}
