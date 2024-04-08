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
        public string StudentId { get; set; }
        public string ExamName { get; set; }
        public List<QnAsViewModel> QnAs { get; set; }
        public TimeSpan RemainingTime { get; set; }
        public int CurrentPage { get; set; } // Current page number
        public int TotalItems { get; set; } // Total number of items
        public int ItemsPerPage { get; set; } // Number of items per page

        // Method to retrieve QnAs for the current page
        public List<QnAsViewModel> GetQnAsForCurrentPage()
        {
            int startIndex = (CurrentPage - 1) * ItemsPerPage;
            int endIndex = Math.Min(startIndex + ItemsPerPage, TotalItems);

            return QnAs.GetRange(startIndex, endIndex - startIndex);
        }
    }
}
