using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Study.Core.Models
{
    public class Course
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<UserCourse> UserCourses { get; set; } = new HashSet<UserCourse>();
        public ICollection<Exam> Exams { get; set; } = new HashSet<Exam>();
        public ICollection<PdfFile> PdfFiles { get; set; } = new List<PdfFile>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();

    }
}
