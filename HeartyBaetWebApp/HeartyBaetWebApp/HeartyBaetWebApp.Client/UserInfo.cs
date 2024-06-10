using System.ComponentModel.DataAnnotations;
using System.Net.Http;

namespace HeartyBaetWebApp.Client
{

    public class UserInfo
    {
        [Required, EmailAddress]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
        [Required, Compare(nameof(Password), ErrorMessage = "The passwords didn't match.")]
        public required string ConfirmPassword { get; set; }
    }
}


