using E_Study.ViewModel;
using E_Study.ViewModel.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Study.Service.exam
{
    public interface IExamService
    {
        Task<ResponseResult<ExamViewModel>> AddExamAsync(ExamViewModel examViewModel);
        ResponseResult<ExamViewModel> GetAllExamCreatedByUser(string userId);
        ResponseResult<ExamCourseViewModel> AddExamToCourse(ExamCourseViewModel model);
        ResponseResult<ExamViewModel> GetExamById(string examId);
        ResultViewModel GetExamResult(string userId, string examId, int attempt);
        bool SetExamResult(StartExamViewModel model);
        bool SetGrade(StartExamViewModel model);

    }
}
