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
    public class UserCourseRepository : BaseRepository<UserCourse>, IUserCourseRepository
    {
        public UserCourseRepository(AppDbContext context) : base(context)
        {

        }

    }
}
