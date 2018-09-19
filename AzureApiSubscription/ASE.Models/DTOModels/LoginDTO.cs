namespace ASE.Models.DTOModels
{
    using System.ComponentModel.DataAnnotations;

    using ASE.Models.Contracts;

    public class LoginDTO : IRegisterLoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
