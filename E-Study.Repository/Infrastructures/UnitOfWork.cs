using Microsoft.EntityFrameworkCore;
using E_Study.Core;
using E_Study.Core.Data;
using E_Study.Repository.IRepositories;
using E_Study.Repository.Repositories;

namespace E_Study.Repository.Infrastructures
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private ICourseRepository _courseRepository;
        private IUserRepository _userRepository;
        private IUserCourseRepository _userCourseRepository;
        private IMessageRepository _messageRepository;

        public UnitOfWork(AppDbContext context = null)
        {
            _context = context ?? new AppDbContext();
        }

        public ICourseRepository CourseRepository
        {
            get
            {
                if (_courseRepository == null)
                {
                    _courseRepository = new CourseRepository(_context);
                }
                return _courseRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_context);
                }
                return _userRepository;
            }
        }

        public IUserCourseRepository UserCourseRepository
        {
            get
            {
                if (_userCourseRepository == null)
                {
                    _userCourseRepository = new UserCourseRepository(_context);
                }
                return _userCourseRepository;
            }
        }

        public IMessageRepository MessageRepository
        {
            get
            {
                if (_messageRepository == null)
                {
                    _messageRepository = new MessageRepository(_context);
                }
                return _messageRepository;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }
            this._context?.Dispose();
        }
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
