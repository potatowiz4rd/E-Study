using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Study.ViewModel
{
    public class CourseViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<UserCourseViewModel> UserCourses { get; set; } = new HashSet<UserCourseViewModel>();
        //public ICollection<Exam> Exams { get; set; } = new HashSet<Exam>();
        //public ICollection<PdfFile> PdfFiles { get; set; } = new List<PdfFile>();
    }
}
