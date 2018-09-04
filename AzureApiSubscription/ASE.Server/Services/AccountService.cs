namespace ASE.Server.Services
{
    using System.Linq;

    using ASE.Data.Contracts;
    using ASE.Models;
    using ASE.Common;
    using ASE.Models.DTOModels;
    using ASE.Server.Services.Contracts;

    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork data;

        public AccountService(IUnitOfWork data)
        {
            this.data = data;
        }

        public User CheckUserByEmail(RegisterDTO model)
        {
            return this.data
                .Users
                .All()
                .Where(x => x.Email == model.Email)
                .FirstOrDefault();
        }

        public User TakeUser(LoginDTO model)
        {
            return this.data.Users
                .All()
                .Where(x => x.Email == model.Email)
                .FirstOrDefault();
        }

        public void RegisterAndSaveUser(RegisterDTO model)
        {
            var userForRegister = new User();
            userForRegister.Email = model.Email;
            userForRegister.Password = model.Password.GenerateHashWithSalt("@%!");

            this.data.Users.Add(userForRegister);
            this.data.SaveChanges();
        }
    }
}
