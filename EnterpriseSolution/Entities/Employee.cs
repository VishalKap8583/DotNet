namespace Enterprise.Entities;

public class Employee
{
    public int? EmployeeNumber { get; set; }
    public string? LastName { get; set; }
    public string? FirstName { get; set; }
    public string? Extension { get; set; }
    public string? Email { get; set; }
    public string? OfficeCode { get; set; }
    public int? ReportsTo { get; set; }
    public string? JobTitle { get; set; } 
}
