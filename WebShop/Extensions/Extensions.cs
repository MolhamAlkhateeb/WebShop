using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Extensions
{
    public static class Extensions
    {


        public static string ToQueryString(this Object model)
        {
            if (model == null) return string.Empty;
            var serialized = JsonConvert.SerializeObject(model);
            var deserialized = JsonConvert.DeserializeObject<Dictionary<string, string>>(serialized);
            var notNull = deserialized.Where(i => i.Value != null).ToList();
            if (notNull.Count < 1) return string.Empty;
            var pairs = notNull.Select((kvp) => kvp.Key.ToString() + "=" + Uri.EscapeDataString(kvp.Value)).ToList();
            var result = pairs.Aggregate((p1, p2) => p1 + "&" + p2);
            return result;
        }
    }
}
