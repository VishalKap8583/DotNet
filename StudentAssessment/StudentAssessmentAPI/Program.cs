using StudentAssessmentLibrary.Entities;
using StudentAssessmentLibrary.Repositories.Implementation;
using StudentAssessmentLibrary.Repositories.Interface;
using StudentAssessmentLibrary.Services.Implementation;
using StudentAssessmentLibrary.Services.Interface;

{
    IStudentRepo studentRepo = new StudentsRepo();
    IStudentService studentService = new StudentService(studentRepo);

    {
        List<Students> students = studentService.GetAll();
        foreach (var student in students)
        {
            Console.WriteLine($"Id: {student.Id}, Name: {student.Name}, Field: {student.Field}");
        }
    }

    {
        Console.WriteLine("1. Add Student");
        Console.WriteLine("2. Delete Student");
        Console.WriteLine("3. Update Student");
        Console.WriteLine("Select Option: ");

        int option = int.Parse( Console.ReadLine() );
        switch (option)
        { 
            case 1:
                Students AddStudent = new Students();
                Console.WriteLine("Enter Student Name: ");
                AddStudent.Name = Console.ReadLine();
                Console.WriteLine("Enter Student Field: ");
                AddStudent.Field = Console.ReadLine();
                bool status = studentService.Create(AddStudent);
                if (status)
                {
                    Console.WriteLine("Student Added Successfully");
                }
                else
                {
                    Console.WriteLine("Operation Failed");
                }
                break;
            case 2:
                {
                    bool status2 = studentService.Delete(int.Parse(Console.ReadLine()));
                    if (status2)
                    {
                        Console.WriteLine("Student Deleted Successfully");
                    }
                    else
                    {
                        Console.WriteLine("operation Failed");
                    }
                }
                break;
            case 3:
                Students UpdateStudent = new Students();
                Console.WriteLine("Enter Id: ");
                UpdateStudent.Id = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter New Name: ");
                UpdateStudent.Name = Console.ReadLine();
                Console.WriteLine("Enter New Field: ");
                UpdateStudent.Field = (Console.ReadLine());
                bool Status3 = studentService.Update(UpdateStudent);
                if (Status3)
                {
                    Console.WriteLine("Student Updated Successfully");
                }
                else
                {
                    Console.WriteLine("Operation Failed");
                }
                 break;
            default:
                {
                    Console.WriteLine("Invalid Option");
                }
                break;
        }
    }




}
