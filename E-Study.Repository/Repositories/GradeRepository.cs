using E_Study.Core.Data;
using E_Study.Core.Models;
using E_Study.Repository.Infrastructures;
using E_Study.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Study.Repository.Repositories
{
    public class GradeRepository : BaseRepository<Grade>, IGradeRepository
    {
        public GradeRepository(AppDbContext context) : base(context)
        {

        }
        public List<Grade> GetGradesInCourse(string courseId)
        {
            // Retrieve grades based on the provided courseId
            return dataContext.Grades
                .Include(g => g.Exam)          
                .Include(g => g.User)
                .Where(g => g.Exam != null && g.Exam.ExamCourses.Any(ec => ec.CourseId == courseId))
                .ToList();
        }

        public List<Grade> GetGradesOfExamInCourse(string courseId, string examId)
        {
            // Retrieve grades based on the provided courseId and examId
            return dataContext.Grades
                .Include(g => g.Exam)
                .Include(g => g.User)
                .Where(g => g.Exam != null && g.Exam.ExamCourses.Any(ec => ec.CourseId == courseId) && g.ExamId == examId)
                .ToList();
        }

        public List<Grade> GetGradesOfCurrentUser(string userId)
        {
            // Retrieve grades based on the provided userId
            return dataContext.Grades
                .Include(g => g.Exam)
                .Include(g => g.User)
                .Where(g => g.UserId == userId)
                .ToList();
        }

        public List<Grade> GetExamGradesOfUser(string userId, string examId)
        {
            // Retrieve grades based on the provided userId and examId
            return dataContext.Grades
                .Include(g => g.Exam)
                .Include(g => g.User)
                .Where(g => g.UserId == userId && g.ExamId == examId)
                .ToList();
        }
    }
}
