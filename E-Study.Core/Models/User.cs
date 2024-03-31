using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Study.Core.Models
{
    public class User : IdentityUser
    {
        //[MaxLength(100)]
        //public string FullName { set; get; }

        //[MaxLength(255)]
        //public string Address { set; get; }

        //[DataType(DataType.Date)]
        public DateTime? Birthday { set; get; }
        public ICollection<Exam> Exams { get; set; } = new HashSet<Exam>();
        public ICollection<ExamResult> ExamResults { get; set; } = new HashSet<ExamResult>();
        public ICollection<UserCourse> UserCourses { get; set; } = new HashSet<UserCourse>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Grade> Grades { get; set; } = new List<Grade>();
    }
}
