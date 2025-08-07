using StudentAssessmentLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAssessmentLibrary.Repositories.Interface
{
    public interface IStudentRepo
    {
        bool Create(Students student);
        bool Update(Students student);
        bool delete(Students Id);
        List<Students> Getall();
        List<Students> Get(int Id);
        bool delete(int id);
    }
}
