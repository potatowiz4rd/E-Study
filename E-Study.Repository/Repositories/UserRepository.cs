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
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {

        }

        public IList<User> GetUsersInCourse(string courseId)
        {
            // Fetch the users associated with the course
            var users = dataContext.UserCourses
                .Where(uc => uc.CourseId == courseId)
                .Select(uc => uc.User)
                .ToList();

            return users;
        }
        public async Task<IList<User>> GetUsersInCourseAsync(string courseId)
        {
            var users = await dataContext.UserCourses
                .Where(uc => uc.CourseId == courseId)
                .Select(uc => uc.User)
                .ToListAsync();

            return users;
        }

        public async Task<IList<User>> GetUsersNotInCourse(string courseId)
        {
            // Fetch all users
            var allUsers = await dataContext.Users.ToListAsync();

            // Fetch the user IDs associated with the specified course
            var usersInCourseIds = await dataContext.UserCourses
                .Where(uc => uc.CourseId == courseId)
                .Select(uc => uc.UserId)
                .ToListAsync();

            // Return users not in the specified course
            var usersNotInCourse = allUsers.Where(u => !usersInCourseIds.Contains(u.Id)).ToList();

            return usersNotInCourse;
        }

    }
}
