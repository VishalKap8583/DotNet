using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentAssessmentLibrary.Entities;
using StudentAssessmentLibrary.Repositories.Implementation;
using StudentAssessmentLibrary.Repositories.Interface;

namespace StudentAssessmentLibrary.Services.Implementation
{
    public class StudentService : Interface.IStudentService
    {
        public readonly IStudentRepo _StudentRepo;

        public StudentService(IStudentRepo StudentRepo)
        {
            _StudentRepo = StudentRepo;
        }
        public bool Create(Students student)
        {
            bool Status = false;
            Status = _StudentRepo.Create(student);
            return Status;
        }

        public bool Delete(int Id)
        {
            bool Status = false;
            Status = _StudentRepo.delete(Id);
            return Status;
        }

        public List<Students> Get(int Id)
        {
            return _StudentRepo.Get(Id);
        }

        public List<Students> GetAll()
        {
            return _StudentRepo.Getall();
        }

        public bool Update(Students student)
        {
            bool Status = false;
            Status = _StudentRepo.Update(student);
            return Status;
        }
    }
}
