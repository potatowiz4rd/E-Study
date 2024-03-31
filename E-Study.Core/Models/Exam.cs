using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Study.Core.Models
{
    public class Exam
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Time { get; set; }
        public double MaxScore { get; set; }
        public string AuthorId { get; set; }
        public User Author { get; set; }
        public ICollection<ExamResult> ExamResults { get; set; }
        public ICollection<QnAs> QnAs { get; set; } = new HashSet<QnAs>();
        public ICollection<ExamCourse> ExamCourses { get; set; } = new HashSet<ExamCourse>();
        public ICollection<Grade> Grades { get; set; } = new HashSet<Grade>();
    }
}
