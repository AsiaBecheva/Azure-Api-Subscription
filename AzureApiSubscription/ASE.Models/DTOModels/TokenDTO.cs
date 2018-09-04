namespace ASE.Models.DTOModels
{
    using ASE.Common;
    using System.ComponentModel.DataAnnotations;

    public class TokenDTO
    {
        public string GrantType { get; set; } = Constants.GrantType;

        [Required]
        public string ClientId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        public string Resource { get; set; } = Constants.Resource;

        [Required]
        public string TenantId { get; set; }
    }
}
