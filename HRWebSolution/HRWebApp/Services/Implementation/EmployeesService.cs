using HRWebApp.Entities;
using HRWebApp.Repositories.Implementation;
using HRWebApp.Repositories.Interface;
using HRWebApp.Services.Interface;

namespace HRWebApp.Services.Implementation
{
    public class EmployeesService : IEmployeesService
    {
        private readonly IEmployeesRepository repo;
        public EmployeesService(IEmployeesRepository repo)
        {
            this.repo = repo;
        }
        public Task<bool> create(Employee employee)
        {
            Task<bool> status = repo.Create(employee);
            return status;
        }

        public Task<bool> delete(int id)
        {
            Task<bool> status = repo.Delete(id);
            return status;
        }

        public Task<Employee> get(int id)
        {
            return repo.Get(id);  
        }

        public Task<List<Employee>> getall()
        {
            return repo.GetAll();   
        }

        public Task<bool> update(Employee employee)
        {
            Task<bool> status = repo.Update(employee);
            return status;
        }
    }
}
