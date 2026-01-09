using System.ComponentModel.DataAnnotations;

namespace sep_project.Models
{
    public class UserAccount
    {
        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        public string User_Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [Compare("Confirm_Password", ErrorMessage = "Password and Confirm Password do not match")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        public string Confirm_Password { get; set; }
    }
}
