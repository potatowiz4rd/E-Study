using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Study.Core.Models
{
    public class UserCourse
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public string CourseId { get; set; }
        public Course Course { get; set; }
    }
}
