namespace ASE.Server.Controllers
{
    using ASE.Data.Contracts;
    using ASE.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly IUnitOfWork data;

        public SubscriptionController(IUnitOfWork data)
        {
            this.data = data;
        }

        public ActionResult<IEnumerable<Subscription>> GetAll()
        {
            var subscriptions = this.data
                .Subscriptions
                .All()
                .ToList();

            return this.Ok(subscriptions);
        }
    }
}