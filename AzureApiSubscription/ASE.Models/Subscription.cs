namespace ASE.Models
{
    using ASE.Common;

    public class Subscription
    {
        public int Id { get; set; }

        public string TenantId { get; set; }

        public string GrantType { get; set; } = Constants.GrantType;

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string Resource { get; set; } = Constants.Resource;

        public string BearerToken { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
