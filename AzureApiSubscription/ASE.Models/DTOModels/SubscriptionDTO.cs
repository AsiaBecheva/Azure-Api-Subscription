namespace ASE.Models.DTOModels
{
    using System.ComponentModel.DataAnnotations;

    public class SubscriptionDTO
    {
        [Required]
        public string TenantId { get; set; }

        [Required]
        public string ClientId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string Alias { get; set; }

        [Required]
        public string SubscriptionAzureId { get; set; }
        
        public int UserId { get; set; }
    }
}
