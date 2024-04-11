using E_Study.Core.Models;
using E_Study.Repository.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Study.Repository.IRepositories
{
    public interface IGradeRepository : IBaseRepository<Grade>
    {
        List<Grade> GetGradesInCourse(string courseId);
        List<Grade> GetGradesOfExamInCourse(string courseId, string examId);
        List<Grade> GetGradesOfCurrentUser(string userId);
        List<Grade> GetExamGradesOfUser(string userId, string examId);
    }
}
