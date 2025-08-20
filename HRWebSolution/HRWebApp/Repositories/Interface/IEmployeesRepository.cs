using HRWebApp.Entities;

namespace HRWebApp.Repositories.Interface
{
    public interface IEmployeesRepository
    {
        Task<bool> Create(Employee employee);
        Task<bool> Update(Employee employee);
        Task<bool> Delete(int id);
        Task<List<Employee>> GetAll();
        Task<Employee> Get(int id);
    }
}
