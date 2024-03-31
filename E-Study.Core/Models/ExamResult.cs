using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Study.Core.Models
{
    public class ExamResult
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string ExamId { get; set; }
        public Exam Exam { get; set; }
        public string QnAsId { get; set; }
        public QnAs QnAs { get; set; }
        public string Answer { get; set; }
        public bool IsCorrect { get; set; }
    }
}
