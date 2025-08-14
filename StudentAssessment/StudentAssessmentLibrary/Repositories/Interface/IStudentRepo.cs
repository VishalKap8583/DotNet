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
        List<Students> Getall();
        List<Students> Get(int id);
        bool Delete(int id);
    }
}
