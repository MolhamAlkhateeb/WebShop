using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebShop.Extensions;
using WebShop.Models;

namespace WebShop.Services.ERPService
{
    public class ERPService : IERPService
    {
        private readonly HttpClient client;

        public ERPService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<Product>> GetProducts(SearchCriteria criteria)
        {
            var response = await client.GetAsync($"/api/products/{criteria.ToQueryString()}");
            response.EnsureSuccessStatusCode();
            List<Product> products = await response.Content.ReadAsAsync<List<Product>>();
            return products;
        }
        public async Task<List<Product>> GetProducts()
        {
            var response = await client.GetAsync($"/api/products");
            response.EnsureSuccessStatusCode();
            List<Product> products = await response.Content.ReadAsAsync<List<Product>>();
            return products;
        }
    }
}
