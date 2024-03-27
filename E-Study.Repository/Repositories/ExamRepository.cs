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

        public IEnumerable<Exam> GetExamsInCourse(string courseId)
        {
            return dataContext.ExamCourses
                .Where(uc => uc.CourseId == courseId)
                .Select(uc => uc.Exam)
                .ToList();
        }
    }
}
