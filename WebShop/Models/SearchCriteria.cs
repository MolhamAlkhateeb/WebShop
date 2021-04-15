using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class SearchCriteria
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }
        public string StartsWith { get; set; }
        public string Contains { get; set; }
        public string Property { get; set; }
    }
}
