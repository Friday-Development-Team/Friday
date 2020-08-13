using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Friday.Models;

namespace Friday.DTOs {
    public class OrderDTO {
        //[Required]
        //public string Username { get; set; }
        [Required]
        public IList<OrderItemDTO> Items { get; set; }
        /// <summary>
        /// Checks if this object is valid.
        /// </summary>
        /// <returns>True if valid, else false</returns>
        public bool IsValid() {
            return /*Username != null &&*/ Items != null && Items.Count != 0 &&
                   Items.Select(s => s.Amount).All(s => s >= 0);
        }
    }
}
