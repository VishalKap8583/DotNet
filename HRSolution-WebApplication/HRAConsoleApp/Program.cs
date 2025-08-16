using HRAStaffing.Entities;
using HRAStaffing.Repositories.Interface;
using HRAStaffing.Services.Implementation;
using HRAStaffing.Services.Interface;
using HRAStaffing.Repositories.Implementation;
using System.Diagnostics;
using System.Transactions;
using System.Diagnostics.Eventing.Reader;
using Org.BouncyCastle.Asn1;

{
    IStaffingRepo staffingRepo = new StaffingRepo();
    IStaffingService staffingService = new StaffingService(staffingRepo);

    List<Employee> employees = staffingService.GetAll();
    foreach (var employee in employees)
    {
        Console.WriteLine($"Id: {employee.Id}, Name: {employee.Name}, Position: {employee.Position}");
    }

    Console.WriteLine("1.Add Employee");
    Console.WriteLine("2.Delete Employee");
    Console.WriteLine("3.Update Employee");

    Console.WriteLine(" Select option:");
    int option = int.Parse(Console.ReadLine());
    switch (option)
    {
        case 1:
            Employee newEmployee = new Employee();
            Console.WriteLine("Enter Employee Name:");
            newEmployee.Name = Console.ReadLine();
            Console.WriteLine("Enter Employee Position:");
            newEmployee.Position = Console.ReadLine();
            bool status1 = staffingService.Create(newEmployee);
            if (status1)
            {
                Console.WriteLine("Employee addded successfully.");
            }
            else
            {
                Console.WriteLine("Failed to add employee.");
            }
            break;
        case 2:
            Console.WriteLine("Enter Employee ID to delete:");
            bool status2 = staffingService.Delete(int.Parse(Console.ReadLine()));
            if (status2)
            {
                Console.WriteLine("Employee deleted successfully.");
            }
            else
            {
                Console.WriteLine("Failed to delete employee.");
            }
            break;
        case 3:
            Console.WriteLine("Enter Employee ID:");
            Employee updateEmployee = new Employee();
            int employeeId = int.Parse(Console.ReadLine());
            Employee emp = staffingService.Get(employeeId);
            Console.WriteLine($"Id: {emp.Id},Name:{emp.Name},Postion:{emp.Position}");
            Console.WriteLine("Enter Employee Name:");
            updateEmployee.Name = Console.ReadLine();
            Console.WriteLine("Enter Employee Position:");
            updateEmployee.Position = Console.ReadLine();
            if (emp != null)
            {
                emp.Name = updateEmployee.Name;
                emp.Position = updateEmployee.Position;
            }
            bool updatestatus = staffingService.Update(emp);
            if (updatestatus)
            {
                Console.WriteLine("Employee updated successfully.");
            }
            else
            {
                Console.WriteLine("Failed to update employee.");
            }
            break;
        default:
            Console.WriteLine("Invalid option selected.");
            break;
    }    
}


    
    

    


