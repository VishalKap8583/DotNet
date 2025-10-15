using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Enterprise.Entities;
using Enterprise.Repositories.Interface;
using Enterprise.Services.Interface;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _service;

    public CustomerController(ICustomerService service)
    {
        _service = service;
    }

    //(http://localhost:5174/api/Customer/GetAll)
    [HttpGet("GetAll")]
    public async Task<List<Customer>> GetAll()
    {
        Task<List<Customer>> customers = _service.GetAllCustomer();
        return await customers;
    }

    //(http://localhost:5174/api/Customer/GetById/)
    [HttpGet("GetById/{id}")]
    public async Task<Customer> Get(int id)
    {
        Task<Customer> customer = _service.GetCustomer(id);
        return await customer;
    }

    //(http://localhost:5174/api/Customer/Add)
    [HttpPost("Add")]
    public async Task<bool> Add(Customer customer)
    {
        return await _service.AddCustomer(customer);
    }

    //(http://localhost:5174/api/Customer/DeleteById/)
    [HttpDelete("DeleteById/{id}")]
    public async Task<bool> Delete(int id)
    {
        return await _service.DeleteCustomer(id);
    }

    //(http://localhost:5174/api/Customer/UpdateById/)
    [HttpPut("UpdateById/{id}")]
    public async Task<bool> Update(Customer customer)
    {
        return await _service.UpdateCustomer(customer);
    }

    //(http://localhost:5174/api/Customer/GetByCheck/)
    [HttpGet("GetByCheck/{checkNum}")]
    public async Task<Customer> GetCustomerByCheck(string checkNum)
    {
        Task<Customer> customer = _service.GetCustomerByCheck(checkNum);
        return await customer;
    }

    //(http://localhost:5174/api/Customer/OrderById/)
    [HttpGet("OrderById/{id}")]
    public async Task<List<Order>> GetOrdersByCustomerNumber(int id)
    {
        Task<List<Order>> orders = _service.GetOrdersByCustomerNumber(id);
        return await orders;
    }

    //(http://localhost:5174/api/Customer/PaymentById/)
    [HttpGet("PaymentById/{id}")]
    public async Task<List<Payment>> GetPaymentDetailsByCustomer(int id)
    {
        Task<List<Payment>> payments = _service.GetPaymentDetailsByCustomer(id);
        return await payments;
    }
}
