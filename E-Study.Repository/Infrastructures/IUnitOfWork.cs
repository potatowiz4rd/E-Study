using E_Study.Repository.IRepositories;
using System;

namespace E_Study.Repository.Infrastructures
{
    public interface IUnitOfWork : IDisposable
    {
        public ICourseRepository CourseRepository { get; } // read only
        public IUserRepository UserRepository { get; } // read only
        public IUserCourseRepository UserCourseRepository { get; } // read only
        public IMessageRepository MessageRepository { get; } // read only

        int SaveChanges();
        Task<int> SaveChangesAsync();

    }
}
