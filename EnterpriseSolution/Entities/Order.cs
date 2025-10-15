
namespace Enterprise.Entities;

public class Order
{
    public int? OrderNumber { get; set; }
    public DateOnly? OrderDate { get; set; }
    public DateOnly? RequiredDate { get; set; }
    public DateOnly? ShippedDate { get; set; }
    public string? Status { get; set; }
    public string? Comments { get; set; }
    public int? CustomerNumber { get; set; }
    public OrderDetails? orderDetails { get; set; }

}
