using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Study.ViewModel 
{ 
    public class ResultViewModel
    {
        public string StudentId { get; set; }
        public string ExamName { get; set; }
        public int Attempt { get; set; }
        public int TotalQuestion { get; set; }
        public int CorrectAnswer { get; set; }
        public int WrongAnswer { get; set; }      
    }
}