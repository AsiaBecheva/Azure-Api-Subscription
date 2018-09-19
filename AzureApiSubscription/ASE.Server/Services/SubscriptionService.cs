namespace ASE.Server.Services
{
    using ASE.Common;
    using ASE.Data.Contracts;
    using ASE.Models;
    using ASE.Models.DTOModels;
    using ASE.Server.Services.Contracts;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Net;
    using System.Text;

    public class SubscriptionService : ISubscriptionService
    {
        private readonly IUnitOfWork data;

        public SubscriptionService(IUnitOfWork data)
        {
            this.data = data;
        }

        public string RetrieveTokenFromAD(TokenDTO model)
        {
            var azureUrl = "https://login.microsoftonline.com/" + model.TenantId + "/oauth2/token";

            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

            var paramCollection = new NameValueCollection();
            paramCollection.Add("grant_type", model.GrantType);
            paramCollection.Add("client_id", model.ClientId);
            paramCollection.Add("client_secret", model.ClientSecret);
            paramCollection.Add("resource", model.Resource);

            byte[] responceBytes = client.UploadValues(azureUrl, "POST", paramCollection);
            client.Dispose();
            string responceBody = Encoding.UTF8.GetString(responceBytes);

            return responceBody;
        }

        public void SaveSubscription(SubscriptionDTO model, string cookie)
        {
            var subs = new Subscription();
            subs.Alias = model.Alias;
            subs.ClientId = model.ClientId;
            subs.ClientSecret = model.ClientSecret;
            subs.Resource = Constants.Resource;
            subs.GrantType = Constants.GrantType;
            subs.SubscriptionAzureId = model.SubscriptionAzureId;
            subs.TenantId = model.TenantId;
            subs.UserId = int.Parse(cookie);

            this.data.Subscriptions.Add(subs);
            this.data.SaveChanges();
        }

        public Subscription GetSubscriptionBySubscriptionId(string subscriptionId)
        {
            return this.data.Subscriptions
                .All()
                .Where(x => x.SubscriptionAzureId == subscriptionId)
                .FirstOrDefault();
        }

        public string DownloadAzureSubscription(string cookieToken, string url)
        {
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.Authorization] = "Bearer " + cookieToken;
            var dataBuffer = client.DownloadData(url);
            string result = Encoding.ASCII.GetString(dataBuffer);

            return result;
        }
    }
}
