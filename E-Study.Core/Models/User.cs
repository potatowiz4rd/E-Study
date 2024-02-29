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
        public ICollection<ExamResult> ExamResults { get; set; } = new HashSet<ExamResult>();
        public ICollection<UserCourse> UserCourses { get; set; } = new HashSet<UserCourse>();

    }
}
