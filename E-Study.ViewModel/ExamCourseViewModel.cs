using E_Study.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Study.ViewModel
{
    public class ExamCourseViewModel
    {
        public string CourseId { get; set; }
        public CourseViewModel Course { get; set; }

        public string ExamId { get; set; }
        public ExamViewModel Exam { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
