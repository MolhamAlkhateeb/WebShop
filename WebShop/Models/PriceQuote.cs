using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class PriceQuote
    {
        public int Id { get; set; }

        [Required]
        public int MinQuantity { get; set; }

        [Required]
        public int MaxQuantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Currency { get; set; }
    }
}
