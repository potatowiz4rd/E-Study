using E_Study.Core.Data;
using E_Study.Core.Models;
using E_Study.Repository.Infrastructures;
using E_Study.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Study.Repository.Repositories
{
    public class QnAsRepository : BaseRepository<QnAs>, IQnAsRepository
    {
        public QnAsRepository(AppDbContext context) : base(context)
        {

        }

        public IEnumerable<QnAs> GetAllQnAsInExam(string examId)
        {
            return dataContext.QnAs
                .Where(q => q.ExamId == examId)
                .ToList();
        }
    }
}
