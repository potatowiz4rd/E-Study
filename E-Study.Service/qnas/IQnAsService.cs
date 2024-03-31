using E_Study.ViewModel.Responses;
using E_Study.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Study.Service.qnas
{
    public interface IQnAsService
    {
        ResponseResult<List<QnAsViewModel>> GetAllQnAsInExam(string examId);
    }
}
