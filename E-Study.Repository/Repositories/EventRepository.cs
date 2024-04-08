using E_Study.Core.Data;
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
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(AppDbContext context) : base(context)
        {

        }

        public List<Event> GetEventsInCourse(string courseId)
        {
            // Assuming you have DbSet<Message> named DbSet in your context
            return dataContext.Events
                .Where(m => m.CourseId == courseId)
                .ToList();
        }
    }
}
