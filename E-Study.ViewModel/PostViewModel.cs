﻿using E_Study.Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Study.ViewModel
{
    public class PostViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int Votes { get; set; }
        public DateTime CreatedAt { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public Course Course { get; set; }
        public string CourseId { get; set; }
        public ICollection<CommentViewModel> Comments { get; set; } = new List<CommentViewModel>();
    }
}
