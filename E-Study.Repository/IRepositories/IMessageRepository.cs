﻿using E_Study.Core.Models;
using E_Study.Repository.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Study.Repository.IRepositories
{
    public interface IMessageRepository : IBaseRepository<Message>
    {
        Task<List<Message>> GetMessagesByCourseIdAsync(string courseId);

    }
}
