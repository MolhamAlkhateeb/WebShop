using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace WebShop.Services.ERPService
{
    public class ERPAuthHandler : DelegatingHandler
    {
        private readonly ERPAuthService authService;

        public ERPAuthHandler(ERPAuthService authService)
        {
            this.authService = authService;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = await authService.GetERPSeviceAccessToken();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await base.SendAsync(request, cancellationToken);

            return response;
        }

        protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = authService.GetERPSeviceAccessToken().GetAwaiter().GetResult();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = base.Send(request, cancellationToken);
            return response;
        }
    }
}
