using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace WebShop.Services.ERPService
{
    public class ERPAuthService
    {
        private readonly HttpClient httClient;
        private readonly IConfiguration configuration;
        private readonly MediaTypeFormatter formatter = new JsonMediaTypeFormatter();
        public ERPAuthService(HttpClient httClient, IConfiguration configuration)
        {
            this.httClient = httClient;
            this.configuration = configuration;
        }

        public async Task<string> GetERPSeviceAccessToken()
        {
            var url = $"{configuration["ERPService:BaseUrl"]}/api/Authorization";
            var userName = configuration["ERPService:UserName"];
            var password = configuration["ERPService:Password"];
            var response = await httClient.PostAsync(url, new { userName, password }, formatter);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
