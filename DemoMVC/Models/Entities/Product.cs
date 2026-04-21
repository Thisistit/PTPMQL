using System.ComponentModel.DataAnnotations;
namespace DemoMVC.Models.Entities
{
public class Product
{
    public int ProductId { get; set; }

    [Required]
    public string ProductName { get; set; }="";

    public decimal Price { get; set; }

    public List<OrderDetail> OrderDetails { get; set; }=[];
}
}