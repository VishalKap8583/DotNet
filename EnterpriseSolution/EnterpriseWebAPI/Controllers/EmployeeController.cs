using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Enterprise.Entities;
using Enterprise.Repositories.Interface;
using Enterprise.Services.Interface;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _service;

    public EmployeeController(IEmployeeService service)
    {
        _service = service;
    }

    //(http://localhost:5174/api/Employee/GetAll)
    [HttpGet("GetAll")]
    public async Task<List<Employee>> GetAll()
    {
        Task<List<Employee>> employees = _service.GetAllEmployee();
        return await employees;
    }

    //(http://localhost:5174/api/Employee/GetById/)
    [HttpGet("GetById/{id}")]
    public async Task<Employee> Get(int id)
    {
        Task<Employee> employee = _service.GetEmployee(id);
        return await employee;
    }

    //(http://localhost:5174/api/Employee/Add)
    [HttpPost("Add")]
    public async Task<bool> Add(Employee employee)
    {
        return await _service.AddEmployee(employee);
    }

    //(http://localhost:5174/api/Employee/DeleteById/)
    [HttpDelete("DeleteById/{id}")]
    public async Task<bool> Delete(int id)
    {
        return await _service.DeleteEmployee(id);
    }

    //(http://localhost:5174/api/Employee/UpdateById/)
    [HttpPut("UpdateById/{id}")]
    public async Task<bool> Update(Employee employee)
    {
        return await _service.UpdateEmployee(employee);
    }

    //(http://localhost:5174/api/Employee/GetByOfficeCode/)
    [HttpGet("GetByOfficeCode/{ofcCode}")]
    public async Task<List<Employee>> GetEmployeeByOfficeCode(string ofcCode)
    {
        Task<List<Employee>> employee = _service.GetEmployeeByOfficeCode(ofcCode);
        return await employee;
    }
}
