namespace ASE.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using ASE.Data.Contracts;
    using ASE.Models.DTOModels;
    using ASE.Common;
    using ASE.Server.Services.Contracts;

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork data;
        private readonly IAccountService accountService;

        public AccountController(IUnitOfWork data,
            IAccountService accountService)
        {
            this.data = data;
            this.accountService = accountService;
        }
        
        [HttpPost]
        public ActionResult Login(LoginDTO model)
        {
            if (ModelState.IsValid)
            {
                var user = this.accountService.GetUserByEmail(model);

                if (user == null)
                {
                    return this.BadRequest("User or password is invalid!");
                }        
              
                if (user.Password != model.Password.GenerateHashWithSalt())
                {
                    return this.BadRequest("User or password is invalid!");
                }

                this.accountService.AddCookieUser(user);

                var subscriptions = this.accountService.GetUserSubscriptions(model);

                if (subscriptions.Count == 0)
                {
                    return this.Ok($"Your subscriptions - {subscriptions.Count}");
                }

                return this.Ok(subscriptions);
            }
            else
            {
                return this.BadRequest("User or password is invalid!");
            }
        }

        [HttpPost]
        public ActionResult Register(RegisterDTO model)
        {
            if (ModelState.IsValid)
            {
                var user = accountService.GetUserByEmail(model);

                if (user != null)
                {
                    return this.BadRequest("This user is already registered!");
                }

                if (string.IsNullOrEmpty(model.Email) || string.IsNullOrWhiteSpace(model.Email))
                {
                    return this.BadRequest("The Email field is not a valid e-mail address.");
                }

                if (!model.Email.IsValidMail())
                {
                    return this.BadRequest("The Email field is not a valid e-mail address.");
                }

                if (string.IsNullOrEmpty(model.Password) || string.IsNullOrWhiteSpace(model.Password)) {
                    return this.BadRequest("Password can not be empty string!");
                }

                this.accountService.RegisterAndSaveUser(model);

                return this.Ok("User was registered!");
            }
            else
            {
                return this.BadRequest("Invalid register data");
            }
        }

        [HttpPost]
        public ActionResult Logout()
        {
            this.accountService.DeleteCookieUser();

            return this.Redirect("www.bulpros.com");
        }
    }
}