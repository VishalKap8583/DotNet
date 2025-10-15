namespace Enterprise.Entities;

public class Payment
{
    public int? CustomerNumber { get; set; }
    public string? CheckNumber { get; set; }
    public DateOnly? PaymentDate { get; set; }
    public decimal? Amount { get; set; }
}
