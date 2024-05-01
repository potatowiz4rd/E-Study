using E_Study.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Study.ViewModel
{
    public class EventViewModel
    {
        public string Text { get; set; }
        public string EventType { get; set; }
        public string? Url { get; set; }
        public string? ExamId { get; set; } // This will bind to the dropdown list
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public IFormFile? File { get; set; }
    }
}
