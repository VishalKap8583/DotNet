namespace Enterprise.Entities;

public class OrderDetails
{
    // public int? OrderNumber { get; set; }
    public string? ProductCode { get; set; }
    public int? QuantityOrdered { get; set; }
    public decimal? PriceEach { get; set; }
    public int? OrderLineNumber { get; set; }
}
