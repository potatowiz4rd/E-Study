using E_Study.Core.Models;
using E_Study.Repository.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Study.Repository.IRepositories
{
    public interface IExamRepository : IBaseRepository<Exam>
    {
        IEnumerable<Exam> GetExamsInCourse(string courseId);
        IEnumerable<Exam> GetAllExamsOfUser(string userId);
        void AddExamToCourse(string examId, string courseId, DateTime startDate, DateTime endDate);
        IEnumerable<Grade> GetUserExamAttempts(string userId, string examId);
        IEnumerable<ExamCourse> GetExamCoursesInCourse(string courseId);

    }
}
