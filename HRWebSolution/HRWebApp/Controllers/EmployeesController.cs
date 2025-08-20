using Microsoft.AspNetCore.Mvc;
using HRWebApp.Entities;
using HRWebApp.Repositories.Implementation;
using HRWebApp.Repositories.Interface;
using HRWebApp.Services.Implementation;
using HRWebApp.Services.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HRWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService svc;
        public EmployeesController(IEmployeesService service)
        {
            this.svc = service;
        }
        // GET: api/<EmployeesController>
        [HttpGet]
        public Task<List<Employee>> Get()
        {
            Task<List<Employee>> employees = svc.getall();
            return employees;
        }

        // GET api/<EmployeesController>/5
        [HttpGet("{id}")]
        public Task<Employee> Get(int id)
        {
            Task<Employee> employee = svc.get(id);
            return employee;
        }

        // POST api/<EmployeesController>
        [HttpPost]
        public Task<bool> Post([FromBody] Employee employee)
        {
            Task<bool> status = svc.create(employee);
            return status;
        }

        // PUT api/<EmployeesController>/5
        [HttpPut("{id}")]
        public Task<bool> Put(int id, [FromBody] Employee employee)
        {
            Task<bool> status = svc.update(employee);
            return status;
        }

        // DELETE api/<EmployeesController>/5
        [HttpDelete("{id}")]
        public Task<bool> Delete(int id)
        {
            Task<bool> status = svc.delete(id);
            return status;
        }
    }
}
