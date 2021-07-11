using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Models;
using WebShop.Services.Interfaces;
using WebShop.Services.MailService;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderQuoteController : ControllerBase
    {
        private readonly IMailService MailService;
        private readonly IViewRenderService RenderService;
        private readonly MailSettings MailSettings;
        public OrderQuoteController(IMailService mailService, IOptions<MailSettings> mailSettings, IViewRenderService renderService)
        {
            MailService = mailService;
            RenderService = renderService;
            MailSettings = mailSettings.Value;
        }

        [HttpPost]
        public async Task<bool> Create([FromBody] OrderQuote quote)
        {
            var isQuoteSent = await SendQuoteRequest(quote);
            var isConfirmationSent = await SendConfirmationEmail(quote);
            return isQuoteSent && isConfirmationSent;
        }

        private async Task<bool> SendQuoteRequest(OrderQuote quote)
        {
            try
            {
                var emailBody = await RenderService.RenderViewToStringAsync("/EmailTemplates/QuoteRequest.cshtml", quote);
                var mailResquest = new MailRequest()
                {
                    Body = emailBody,
                    Subject = $"Order Quote from {quote.Email}",
                    ToEmail = MailSettings.Mail
                };
                await MailService.SendEmailAsync(mailResquest);
                return true;

            }
            catch
            {

                return false;
            }
        }

        private async Task<bool> SendConfirmationEmail(OrderQuote quote)
        {
            try
            {
                var emailBody = await RenderService.RenderViewToStringAsync("/EmailTemplates/QuoteRequestConfirmation.cshtml", quote);
                var mailResquest = new MailRequest()
                {
                    Body = emailBody,
                    Subject = $"Order Quote Confirmation",
                    ToEmail = quote.Email
                };
                await MailService.SendEmailAsync(mailResquest);
                return true;

            }
            catch
            {

                return false;
            }
        }
    }
}
