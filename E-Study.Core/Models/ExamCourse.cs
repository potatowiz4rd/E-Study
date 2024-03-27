using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Study.Core.Models
{
    public class ExamCourse
    {
        public string CourseId { get; set; }
        public Course Course { get; set; }

        public string ExamId { get; set; }
        public Exam Exam { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
