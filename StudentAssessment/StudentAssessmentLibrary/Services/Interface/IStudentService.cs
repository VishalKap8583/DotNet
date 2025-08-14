using StudentAssessmentLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAssessmentLibrary.Services.Interface
{
    public interface IStudentService
    {
        bool Create(Students student);
        bool Delete(int id);
        bool Update(Students student);
        List<Students> GetAll();
        List<Students> Get(int id);
    }
}
