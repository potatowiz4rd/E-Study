using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Study.Core.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string ExamId { get; set; }
        public Exam Exam { get; set; }
        public double Score { get; set; }
        public DateTime DateAssigned { get; set; }
        // Additional properties as needed
    }
}
