using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Enterprise.Entities;
using Enterprise.Repositories.Implementation;
using Enterprise.Repositories.Interface;
using Enterprise.Services.Interface;

namespace Enterprise.Services.Implementation;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepo repo;
    public EmployeeService(IEmployeeRepo repo)
    {
        this.repo = repo;
    }
    public async Task<bool> AddEmployee(Employee employee)
    {
        return await repo.AddEmployee(employee);
    }
    public async Task<bool> UpdateEmployee(Employee employee)
    {
        return await repo.UpdateEmployee(employee);
    }
    public async Task<bool> DeleteEmployee(int id)
    {
        return await repo.DeleteEmployee(id);
    }
    public async Task<List<Employee>> GetAllEmployee()
    {
        return await repo.GetAllEmployee();
    }
    public async Task<Employee> GetEmployee(int id)
    {
        return await repo.GetEmployee(id);
    }
    public async Task<List<Employee>> GetEmployeeByOfficeCode(string ofcCode)
    {
        return await repo.GetEmployeeByOfficeCode(ofcCode);
    }
}