﻿using E_Study.Core.Data;
using E_Study.Core.Models;
using E_Study.Repository.Infrastructures;
using E_Study.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Study.Repository.Repositories
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Comment>> GetCommentsByPostIdAsync(string postId)
        {
            // Assuming you have DbSet<Message> named DbSet in your context
            return await dataContext.Comments
                .Where(m => m.PostId == postId).Include(p => p.User)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }
    }
}
