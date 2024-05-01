using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Study.Core.Models
{
    public class Event
    {
        public string Id { get; set; }
        public string? EventType { get; set; } 
        public string Text { get; set; }
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public string? Url { get; set; }
        public string CourseId { get; set; }
        public Course Course { get; set; }
        public ExamCourse? ExamCourse { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [NotMapped]
        public IFormFile? File { get; set; }
    }
}
