namespace ASE.Server.Services
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Http;

    using ASE.Data.Contracts;
    using ASE.Models;
    using ASE.Common;
    using ASE.Models.DTOModels;
    using ASE.Server.Services.Contracts;
    using ASE.Models.Contracts;

    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork data;
        private readonly IHttpContextAccessor contextAccessor;

        public AccountService(IUnitOfWork data, IHttpContextAccessor contextAccessor)
        {
            this.data = data;
            this.contextAccessor = contextAccessor;
        }

        public User GetUserByEmail<T>(T model) where T : IRegisterLoginDTO
        {
            return this.data.Users.All().FirstOrDefault(x => x.Email == model.Email);
        }

        public void RegisterAndSaveUser(RegisterDTO model)
        {
            var userForRegister = new User();
            userForRegister.Email = model.Email;
            userForRegister.Password = model.Password.GenerateHashWithSalt();

            this.data.Users.Add(userForRegister);
            this.data.SaveChanges();
        }

        public List<Subscription> GetUserSubscriptions(LoginDTO model)
        {
            return this.data
                .Subscriptions
                .All()
                .Where(x => x.User.Email == model.Email)
                .ToList();
        }

        public void AddCookieUser(User user)
        {
            this.contextAccessor.HttpContext.Response.Cookies
                    .Append("UserId", user.Id.ToString(), new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddHours(1)
                    });
        }

        public void DeleteCookieUser()
        {
            this.contextAccessor.HttpContext.Response.Cookies.Delete("UserId");
        }
    }
}
