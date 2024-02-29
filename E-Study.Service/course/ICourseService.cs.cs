using E_Study.ViewModel;
using E_Study.ViewModel.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Study.Service.course
{
    public interface ICourseService
    {
        ResponseResult<CourseViewModel> GetAll();

    }
}
