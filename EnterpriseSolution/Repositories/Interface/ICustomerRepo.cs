using Enterprise.Entities;

namespace Enterprise.Repositories.Interface;

public interface ICustomerRepo
{
    public Task<bool> AddCustomer(Customer customer);
    public Task<bool> UpdateCustomer(Customer customer);
    public Task<bool> DeleteCustomer(int id);
    public Task<List<Customer>> GetAllCustomer();
    public Task<Customer> GetCustomer(int id);
    public Task<Customer> GetCustomerByCheck(string checkNum);
    public Task<List<Order>> GetOrdersByCustomerNumber(int id);
    public Task<List<Payment>> GetPaymentDetailsByCustomer(int id);
}