using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace DemoMVC.Models.Entities
{
    public class Student
    {
        [Key]
        [Required]
        public  string StudentCode { get; set; } = default!;
        public string FullName { get; set; } = default!;
    }
}