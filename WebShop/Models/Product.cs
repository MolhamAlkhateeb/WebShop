using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Image> Images { get; set; }
        public IEnumerable<PriceQuote> Prices { get; set; }
        public string ItemNumber { get; set; }

        public IEnumerable<ProductSpecification> Specifications { get; set; }
    }
}
