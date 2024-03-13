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
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(AppDbContext context) : base(context)
        {

        }
        public IList<Course> GetAllCourseOfUser(string userId)
        {
            var courses = dataContext.UserCourses
            .Where(uc => uc.UserId == userId)
            .Select(uc => uc.Course)
            .ToList();

            return courses;
        }

        public void AddUserToCourse(string userId, string courseId)
        {
            // Check if the user is already in the course (similar to your JoinCourse method)
            if (!dataContext.Set<UserCourse>().Any(uc => uc.UserId == userId && uc.CourseId == courseId))
            {
                // Add the user to the course
                dataContext.Set<UserCourse>().Add(new UserCourse { UserId = userId, CourseId = courseId });
                dataContext.SaveChanges();
            }
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

        public IList<User> GetUsersNotInCourse(string courseId)
        {
            // Fetch the users associated with the course
            var users = dataContext.UserCourses
                .Where(uc => uc.CourseId != courseId)
                .Select(uc => uc.User)
                .ToList();

            return users;
        }

    }
}
