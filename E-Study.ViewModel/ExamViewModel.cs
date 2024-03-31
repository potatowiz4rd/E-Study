using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Study.ViewModel
{
    public class ExamViewModel
    {
        public string Id { get; set; }
        public string AuthorId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Time { get; set; }
        public double MaxScore { get; set; }
        public List<QnAsViewModel> QnAs { get; set; }
    }
}
