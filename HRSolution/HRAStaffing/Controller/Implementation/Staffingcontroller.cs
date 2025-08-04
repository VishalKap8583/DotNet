using HRAStaffing.Entities;
using HRAStaffing.Repositories.Interface;
using HRAStaffing.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAStaffing.Controller.Implementation
{
    public class Staffingcontroller : HRAStaffing.Controller.Interface.IstaffingController
    {
        private readonly IStaffingService _staffingService;
        public Staffingcontroller(IStaffingService staffingService)
        {
            _staffingService = staffingService;
        }

        public bool Create(Employee employee)
        {
            bool status = false;
            status = _staffingService.Create(employee);
            return status;
        }

        public bool Delete(int employeeId)
        {
            bool status = false;
            status = _staffingService.Delete(employeeId);
            return status;
        }

        public Employee Get(int id)
        {
            Employee employee = _staffingService.Get(id);
            return new Employee();
        }

        public List<Employee> GetAll()
        {
            List<Employee> employees = _staffingService.GetAll();
            return new List<Employee>();
        }

        public bool Update(Employee employee)
        {
            bool status = false;
            status = _staffingService.Update(employee);
            return status;
        }
    }
}
