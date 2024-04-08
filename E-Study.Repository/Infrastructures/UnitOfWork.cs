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
        private IPostRepository _postRepository;
        private ICommentRepository _commentRepository;
        private IExamRepository _examRepository;
        private IQnAsRepository _qnAsRepository;
        private IExamResultRepository _examResultRepository;
        private IEventRepository _eventRepository;
        private IGradeRepository _gradeRepository;

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

        public IPostRepository PostRepository
        {
            get
            {
                if (_postRepository == null)
                {
                    _postRepository = new PostRepository(_context);
                }
                return _postRepository;
            }
        }

        public ICommentRepository CommentRepository
        {
            get
            {
                if (_commentRepository == null)
                {
                    _commentRepository = new CommentRepository(_context);
                }
                return _commentRepository;
            }
        }
        public IExamRepository ExamRepository
        {
            get
            {
                if (_examRepository == null)
                {
                    _examRepository = new ExamRepository(_context);
                }
                return _examRepository;
            }
        }

        public IQnAsRepository QnAsRepository
        {
            get
            {
                if (_qnAsRepository == null)
                {
                    _qnAsRepository = new QnAsRepository(_context);
                }
                return _qnAsRepository;
            }
        }

        public IExamResultRepository ExamResultRepository
        {
            get
            {
                if (_examResultRepository == null)
                {
                    _examResultRepository = new ExamResultRepository(_context);
                }
                return _examResultRepository;
            }
        }

        public IEventRepository EventRepository
        {
            get
            {
                if (_eventRepository == null)
                {
                    _eventRepository = new EventRepository(_context);
                }
                return _eventRepository;
            }
        }

        public IGradeRepository GradeRepository
        {
            get
            {
                if (_gradeRepository == null)
                {
                    _gradeRepository = new GradeRepository(_context);
                }
                return _gradeRepository;
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
