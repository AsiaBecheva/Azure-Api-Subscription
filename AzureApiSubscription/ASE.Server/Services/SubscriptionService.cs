namespace ASE.Server.Services
{
    using ASE.Models.DTOModels;
    using ASE.Server.Services.Contracts;
    using System.Collections.Specialized;
    using System.Net;
    using System.Text;

    public class SubscriptionService : ISubscriptionService
    {
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
            string responceBody = Encoding.UTF8.GetString(responceBytes);

            return responceBody;
        }
    }
}
