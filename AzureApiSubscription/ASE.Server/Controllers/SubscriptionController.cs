namespace ASE.Server.Controllers
{
    using ASE.Common;
    using ASE.Data.Contracts;
    using ASE.Models;
    using ASE.Models.DTOModels;
    using ASE.Server.Services.Contracts;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

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
        private ActionResult GetToken(TokenDTO model)
        {
            if (ModelState.IsValid)
            {
                var retrievedToken = subscriptionService.RetrieveTokenFromAD(model);

                return this.Ok(retrievedToken);
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

            if (ModelState.IsValid)
            {
                var subs = new Subscription();
                subs.Alias = model.Alias;
                subs.ClientId = model.ClientId;
                subs.ClientSecret = model.ClientSecret;
                subs.Resource = Constants.Resource;
                subs.GrantType = Constants.GrantType;
                subs.SubscriptionAzureId = model.SubscriptionAzureId;
                subs.TenantId = model.TenantId;
                subs.UserId = int.Parse(userIdFromCookie);

                this.data.Subscriptions.Add(subs);
                this.data.SaveChanges();

                return this.Ok();
            }

            return this.BadRequest("Invalid data");
        }
    }
}