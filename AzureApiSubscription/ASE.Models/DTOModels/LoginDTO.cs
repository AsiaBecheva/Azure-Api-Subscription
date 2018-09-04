﻿namespace ASE.Models.DTOModels
{
    using System.ComponentModel.DataAnnotations;

    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}