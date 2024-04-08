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
    public class ExamRepository : BaseRepository<Exam>, IExamRepository
    {
        public ExamRepository(AppDbContext context) : base(context)
        {
        }

        public void AddExamToCourse(string examId, string courseId, DateTime startDate, DateTime endDate)
        {
            //complete this function that will add an exam to a course
            if (!dataContext.ExamCourses.Any(uc => uc.ExamId == examId && uc.CourseId == courseId))
            {
                dataContext.ExamCourses
                    .Add(new ExamCourse { ExamId = examId, CourseId = courseId, StartDate = startDate, EndDate = endDate });
                dataContext.SaveChanges();
            }
        }

        public IEnumerable<Exam> GetAllExamsOfUser(string userId)
        {
            // Assuming you have DbSet<Message> named DbSet in your context
            return dataContext.Exams
                .Where(m => m.AuthorId == userId)
                .ToList();
        }

        public IEnumerable<Exam> GetExamsInCourse(string courseId)
        {
            return dataContext.ExamCourses
                .Where(uc => uc.CourseId == courseId)
                .Select(uc => uc.Exam)
                .ToList();
        }

        public IEnumerable<Grade> GetUserExamAttempts(string userId, string examId)
        {
            return dataContext.Grades
                .Where(g => g.UserId == userId && g.ExamId == examId)
                .ToList();
        }
    }
}
