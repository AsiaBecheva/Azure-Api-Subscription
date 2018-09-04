namespace ASE.Server.Services.Contracts
{
    using ASE.Models.DTOModels;

    public interface ISubscriptionService
    {
        string RetrieveTokenFromAD(TokenDTO model);
    }
}
