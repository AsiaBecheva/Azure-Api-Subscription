namespace ASE.Server.Services.Contracts
{
    using ASE.Models;
    using ASE.Models.DTOModels;

    public interface ISubscriptionService
    {
        string RetrieveTokenFromAD(TokenDTO model);

        void SaveSubscription(SubscriptionDTO model, string cookie);

        Subscription GetSubscriptionBySubscriptionId(string subscriptionId);

        string DownloadAzureSubscription(string cookieToken, string url);
    }
}
