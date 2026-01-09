using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sep_project.Models
{
    public class Task
    {

     
        [Key]
        public int Task_Id { get; set; }

        [Required]
        [StringLength(20,ErrorMessage ="Please Enter The Task Name ")]
        public string Title { get; set; }

        [StringLength(200, ErrorMessage = "Please Enter The Task Description less than 200 char")]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Deadline { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        //public int User_Id { get; set; }

        //[ForeignKey("User_Id")]
        //[ValidateNever]

        //public User user { get; set; }

        public string? UserId { get; set; }







    }
}
