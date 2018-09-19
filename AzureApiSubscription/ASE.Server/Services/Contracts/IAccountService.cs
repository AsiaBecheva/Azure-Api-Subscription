namespace ASE.Server.Services.Contracts
{
    using System.Collections.Generic;

    using ASE.Models;
    using ASE.Models.Contracts;
    using ASE.Models.DTOModels;

    public interface IAccountService
    {
        User GetUserByEmail<T>(T model) where T : IRegisterLoginDTO;       

        void RegisterAndSaveUser(RegisterDTO model);

        List<Subscription> GetUserSubscriptions(LoginDTO model);

        void AddCookieUser(User user);

        void DeleteCookieUser();
    }
}
