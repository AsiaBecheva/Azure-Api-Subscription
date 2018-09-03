namespace ASE.Server.Controllers
{
    using ASE.Data.Contracts;
    using ASE.Models;
    using ASE.Models.DTOModels;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;
    using System.Net;
    using System.Collections.Specialized;
    using System.Text;

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork data;

        public AccountController(IUnitOfWork data)
        {
            this.data = data;
        }
        
        [HttpPost]
        public ActionResult Token(TokenRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var azureUrl = "https://login.microsoftonline.com/" +  model.TenantId + "/oauth2/token";

                using (WebClient client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

                    var paramCollection = new NameValueCollection();
                    paramCollection.Add("grant_type", model.GrantType);
                    paramCollection.Add("client_id", model.ClientId);
                    paramCollection.Add("client_secret", model.ClientSecret);
                    paramCollection.Add("resource", model.Resource);

                    byte[] responceBytes =  client.UploadValues(azureUrl, "POST", paramCollection);
                    string resonceBody = Encoding.UTF8.GetString(responceBytes);
                    
                    return this.Ok(resonceBody);
                }
            }
            else
            {
                return this.BadRequest("Invalid attempt!");
            }
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = this.data.Users
                    .All()
                    .Where(x => x.Email == model.Email)
                    .FirstOrDefault();

                if(user == null)
                {
                    return this.BadRequest("Invalid login!");
                }

                var password = user.Password;
                if (model.Password != user.Password)
                {
                    return this.BadRequest("Invalid password!");
                }

                return this.Ok(model);
            }
            else
            {
                return this.BadRequest("Invalid login data!");
            }
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                var checkUser = this.data
                    .Users
                    .All()
                    .Where(x => x.Email == model.Email)
                    .FirstOrDefault();

                if (checkUser != null)
                {
                    return this.BadRequest("Already registered email!");
                }

                var userForRegister = new User();
                userForRegister.Email = model.Email;
                userForRegister.Password = model.Password;
                this.data.Users.Add(userForRegister);
                this.data.SaveChanges();

                return this.Redirect("Here will be the URL for login page!");
            }
            else
            {
                return this.BadRequest("Invalid register data");
            }
        }

        [HttpPost]
        public ActionResult Logout()
        {
            //Delete ApplicatonCookie

            return this.Redirect("www.asd.com");
        }
    }
}