using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sep_project.Models
{
    public class Department
    {
        [Key]
        public int? Department_Id { get; set; }
        [Required]
        [StringLength(20)]
        [Column(TypeName = "nvarchar(20)")]
        public string Department_Name { get; set; }
        [ValidateNever]
        public ICollection<Employee> Employees { get; set; }

    }
}
