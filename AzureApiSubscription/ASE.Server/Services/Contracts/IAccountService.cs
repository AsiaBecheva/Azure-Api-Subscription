namespace ASE.Server.Services.Contracts
{
    using ASE.Models;
    using ASE.Models.DTOModels;

    public interface IAccountService
    {
        User CheckUserByEmail(RegisterDTO model);

        void RegisterAndSaveUser(RegisterDTO model);

        User TakeUser(LoginDTO model);
    }
}
