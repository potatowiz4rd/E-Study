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

        public Exam GetExamWithQnAsId(string examId)
        {
            return dataContext.Exams
                                  .Include(e => e.QnAs)
                                  .FirstOrDefault(e => e.Id == examId);
            // If you want to return the exam, you can store it in a field or property.
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

        public IEnumerable<ExamCourse> GetExamCoursesInCourse(string courseId)
        {
            return dataContext.ExamCourses
                .Where(uc => uc.CourseId == courseId).Include(uc => uc.Exam)
                .ToList();
        }

        public IEnumerable<Grade> GetUserExamAttempts(string userId, string examId)
        {
            return dataContext.Grades
                .Where(g => g.UserId == userId && g.ExamId == examId)
                .ToList();
        }

        public async Task DeleteExamAsync(string examId)
        {
            // Fetch the exam along with related entities
            var exam = await dataContext.Exams
                .Include(e => e.QnAs)
                .ThenInclude(q => q.ExamResults)
                .Include(e => e.ExamResults)
                .Include(e => e.ExamCourses)
                .FirstOrDefaultAsync(e => e.Id == examId);

            if (exam != null)
            {
                // Remove related ExamResults
                dataContext.ExamResults.RemoveRange(exam.ExamResults);

                // Remove related QnAs and their ExamResults
                foreach (var qna in exam.QnAs)
                {
                    dataContext.ExamResults.RemoveRange(qna.ExamResults);
                    dataContext.QnAs.Remove(qna);
                }

                // Remove related ExamCourses
                dataContext.ExamCourses.RemoveRange(exam.ExamCourses);

                // Remove the exam itself
                dataContext.Exams.Remove(exam);

                await dataContext.SaveChangesAsync();
            }
        }
    }
}
