using System;
using System.Collections.Generic;
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
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Time { get; set; }
        public string CourseId { get; set; }
        public Course Course { get; set; }
        public ICollection<ExamResult> ExamResults { get; set; }
        public ICollection<QnAs> QnAs { get; set; } = new HashSet<QnAs>();


    }
}
