﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Study.Core.Models
{
    public class Course
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<UserCourse> UserCourses { get; set; } = new HashSet<UserCourse>();
        public ICollection<ExamCourse> ExamCourses { get; set; } = new HashSet<ExamCourse>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
