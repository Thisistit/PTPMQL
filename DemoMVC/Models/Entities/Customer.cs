using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Tên không được để trống")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        public string SDT { get; set; } =string.Empty;
        public List<Order> Orders { get; set; }= [];
    }
}