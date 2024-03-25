using E_Study.ViewModel.Responses;
using E_Study.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Study.Service.post
{
    public interface IPostService
    {
        ResponseResult<PostViewModel> GetAllPostsInCourse(string courseId);
        Task<ResponseResult<PostViewModel>> GetAllPostsInCourseAsync(string courseId);

    }
}
