using HRWebApp.Entities;
using HRWebApp.Repositories.Implementation;
using HRWebApp.Repositories.Interface;

namespace HRWebApp.Services.Interface
{
    public interface IEmployeesService
    {
        Task<bool> create(Employee employee);
        Task<bool> delete(int id);
        Task<Employee> get(int id);
        Task<List<Employee>> getall();
        Task<bool> update(Employee employee);
    }       
}
