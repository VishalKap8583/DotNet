using Enterprise.Entities;

namespace Enterprise.Repositories.Interface;

public interface IEmployeeRepo
{
    public Task<bool> AddEmployee(Employee employee);
    public Task<bool> UpdateEmployee(Employee employee);
    public Task<bool> DeleteEmployee(int id);
    public Task<List<Employee>> GetAllEmployee();
    public Task<Employee> GetEmployee(int id);
    public Task<List<Employee>> GetEmployeeByOfficeCode(string ofcCode);
}
