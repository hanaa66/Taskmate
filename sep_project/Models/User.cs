using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sep_project.Models
{
    public class User
    {
        [Key]
        public int User_Id { get; set; }

        [StringLength(20, ErrorMessage = "Please Enter Name Less Than 20 char")]
        [Column(TypeName = "nvarchar(20)")]
        [Required(ErrorMessage = "User Name Is Required")]
        public string User_Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(50, ErrorMessage = "Please Enter Email Less Than 50 Char")]
        public string User_Email { get; set; }

        [Required]
        public string Employee_Country { get; set; }

        [Required]
        public string Job_Title { get; set; }

        

    }
}
