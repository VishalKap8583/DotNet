using Enterprise.Entities;
using Enterprise.Repositories.Implementation;
using Enterprise.Repositories.Interface;
using Enterprise.Services.Interface;

namespace Enterprise.Services.Implementation;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepo repo;
    public CustomerService(ICustomerRepo repo)
    {
        this.repo = repo;
    }
    public async Task<bool> AddCustomer(Customer customer)
    {
        return await repo.AddCustomer(customer);
    }
    public async Task<bool> UpdateCustomer(Customer customer)
    {
        return await repo.UpdateCustomer(customer);
    }
    public async Task<bool> DeleteCustomer(int id)
    {
        return await repo.DeleteCustomer(id);
    }
    public async Task<List<Customer>> GetAllCustomer()
    {
        return await repo.GetAllCustomer();
    }
    public async Task<Customer> GetCustomer(int id)
    {
        return await repo.GetCustomer(id);
    }
    public async Task<Customer> GetCustomerByCheck(string checkNum)
    {
        return await repo.GetCustomerByCheck(checkNum);
    }

    public async Task<List<Order>> GetOrdersByCustomerNumber(int id)
    {
        return await repo.GetOrdersByCustomerNumber(id);
    }
    public async Task<List<Payment>> GetPaymentDetailsByCustomer(int id)
    {
        return await repo.GetPaymentDetailsByCustomer(id);
    }

}