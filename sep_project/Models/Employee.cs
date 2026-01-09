using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sep_project.Models
{
    public class Employee
    {
        [Key]
        public int Employee_Id { get; set; }

        [StringLength(50, ErrorMessage = "please enter name less than 100 char")]
        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "employee name is required")]
        public string Employee_Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(50, ErrorMessage = "please enter email less than 50 char")]
        public string Employee_Email { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "enter valid salary")]
        [Column(TypeName = "decimal(10,2)")]

        public decimal Employee_Salary { get; set; }

        [Required]
        public string Employee_Country { get; set; }

        [DataType(DataType.Date)]
        public DateTime Hire_Date { get; set; }

        [Required]
        //public string? Image_Name { get; set; }
        public int? Department_Id { get; set; }
        [ForeignKey("Department_Id")]
        [ValidateNever]
        public Department department { get; set; }



    }
}
