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
    public class ExamCourseRepository : BaseRepository<ExamCourse>, IExamCourseRepository
    {
        public ExamCourseRepository(AppDbContext context) : base(context)
        {
        }
        public void Delete(string examId, string courseId)
        {
            var examCourse = dbSet.Find(courseId, examId);
            if (examCourse != null)
            {
                dbSet.Remove(examCourse);
            }
        }

    }
}
