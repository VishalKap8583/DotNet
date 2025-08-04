using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRAStaffing.Entities;

namespace HRAStaffing.Controller.Interface
{
    public interface IstaffingController
    {
        bool Create(Employee employee);
        bool Update(Employee employee);
        bool Delete(int employeeId);
        List<Employee> GetAll();
        Employee Get(int id);
    }
}
