namespace ASE.Server.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using ASE.Data.Contracts;
    using ASE.Models.DTOModels;
    using ASE.Server.Services.Contracts;
    using System.Net;
    using System.IO;
    using System;
    using System.Linq;
    using Newtonsoft.Json;
    using System.Text;

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly IUnitOfWork data;
        private readonly ISubscriptionService subscriptionService;
        private readonly IHttpContextAccessor contextAccessor;

        public SubscriptionController(IUnitOfWork data,
            ISubscriptionService subscriptionService,
            IHttpContextAccessor contextAccessor)
        {
            this.data = data;
            this.subscriptionService = subscriptionService;
            this.contextAccessor = contextAccessor;
        }

        [HttpPost]
        public ActionResult GetToken(TokenDTO model)
        {
            if (ModelState.IsValid)
            {
                var retrievedToken = subscriptionService.RetrieveTokenFromAD(model);
                Token token = JsonConvert.DeserializeObject<Token>(retrievedToken);

                this.contextAccessor.HttpContext.Response.Cookies
                    .Append("token", token.access_token, new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddHours(1)
                    });
                return this.Ok();
            }
            else
            {
                return this.BadRequest("Invalid attempt!");
            }
        }

        [HttpPost]
        public ActionResult CreateSubscription(SubscriptionDTO model)
        {
            string userIdFromCookie = this.contextAccessor.HttpContext.Request.Cookies["userId"];

            if (userIdFromCookie == null)
            {
                return this.BadRequest("Please login!");
            }

            if (ModelState.IsValid)
            {
                this.subscriptionService.SaveSubscription(model, userIdFromCookie);

                return this.Ok("New Subscription was created!");
            }

            return this.BadRequest("Invalid data");
        }

        [HttpGet]
        public ActionResult GetSubscription(string subscriptionId)
        {
            string userIdFromCookie = this.contextAccessor.HttpContext.Request.Cookies["userId"];
            string cookieToken = this.contextAccessor.HttpContext.Request.Cookies["token"];

            if (userIdFromCookie == null || cookieToken == null)
            {
                return this.BadRequest("Please login!");
            }

            var subscription = this.subscriptionService.GetSubscriptionBySubscriptionId(subscriptionId);
            var azureUrl = "https://management.azure.com/subscriptions/" + subscription.SubscriptionAzureId + "/providers/Microsoft.Compute/locations/west%20europe/usages?api-version=2017-12-01";

            try
            {
                var result = this.subscriptionService.DownloadAzureSubscription(cookieToken, azureUrl);

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
    }
}