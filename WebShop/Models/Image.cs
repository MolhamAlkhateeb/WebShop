using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public bool IsMain { get; set; }
        public int Position { get; set; }

    }
}
