using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Study.ViewModel
{
    public class QnAsViewModel
    {
        public string Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Type { get; set; }
        public string ExamId { get; set; }
        public ExamViewModel Exam { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string? SelectedAnswer { get; set; }

    }
}
