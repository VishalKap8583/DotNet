using HRAStaffing.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HRAStaffing.Repositories.Interface;
namespace HRAStaffing.Services.Implementation
{
    public class StaffingService : HRAStaffing.Services.Interface.IStaffingService
    {

        private readonly IStaffingRepo _staffingRepo;

        public StaffingService(IStaffingRepo staffingRepo)
        {
            _staffingRepo = staffingRepo;
        }
        public bool Create(Employee employee)
        {
            bool status = false;
            // Logic to create an employee
            status = _staffingRepo.Create(employee);
            return status;
        }

        public bool Delete(int employeeId)
        {
           bool status = false;
            // Logic to delete an employee by ID
            status = _staffingRepo.Delete(employeeId);
            return status;
        }

        public Employee Get(int id)
        {
            return _staffingRepo.Get(id);
        }

        public List<Employee> GetAll()
        {
            return _staffingRepo.GetAll();
        }

        public bool Update(Employee employee)
        {
           bool status = false;
            // Logic to update an employee
            status = _staffingRepo.Update(employee);
            return status;
        }
    }
}
