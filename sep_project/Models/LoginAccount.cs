using System.ComponentModel.DataAnnotations;

namespace sep_project.Models
{
    public class LoginAccount
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
