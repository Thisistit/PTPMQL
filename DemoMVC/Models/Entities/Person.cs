using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace DemoMVC.Models.Entities
{
    public class Person
    {
        [Key]
        [Required(ErrorMessage = "Mã sinh viên không được để trống")]
        public string StudentCode { get; set; } = default!;
        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(50, ErrorMessage = "Tên tối đa 50 ký tự")]
        public string FullName { get; set; } = default!;
    }
}