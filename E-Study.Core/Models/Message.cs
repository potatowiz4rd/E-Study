using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Study.Core.Models
{
    public class Message
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public Course Course { get; set; }
        public string CourseId { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
    }
}
