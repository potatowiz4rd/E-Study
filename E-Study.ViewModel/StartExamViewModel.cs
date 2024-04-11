using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Study.ViewModel
{
    public class StartExamViewModel
    {
        public string ExamId { get; set; }
        public int Attempt { get; set; }
        public string StudentId { get; set; }
        public string ExamName { get; set; }
        public List<QnAsViewModel> QnAs { get; set; }
        public TimeSpan RemainingTime { get; set; }
            
    }
}
