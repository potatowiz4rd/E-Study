using E_Study.Core.Models;
using E_Study.Repository.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Study.Repository.IRepositories
{
    public interface ICourseRepository : IBaseRepository<Course>
    {
        IList<User> GetUsersInCourse(string courseId);
        IList<User> GetUsersNotInCourse(string courseId);
        IList<Course> GetAllCourseOfUser(string userId);
        void AddUserToCourse(string userId, string courseId);
        void LoadUserCourses(Course course);
        void LoadExamCourses(Course course);

    }
}
