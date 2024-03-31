using AutoMapper;
using E_Study.Repository.Infrastructures;
using E_Study.ViewModel;
using E_Study.ViewModel.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Study.Service.course
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public CourseService (IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public ResponseResult<CourseViewModel> GetAll()
        {
            ResponseResult<CourseViewModel> response = new ResponseResult<CourseViewModel>();

            try
            {
          
                var courses = uow.CourseRepository.GetAll();                
                if (courses == null || courses.Count() == 0)
                {
                    return new ResponseResult<CourseViewModel>() { Message = "No data", StatusCode = 404 };
                }
                var courseModels = mapper.Map<List<CourseViewModel>>(courses);
                return new ResponseResult<CourseViewModel>() { DataList = courseModels, Message = "Success" };
            }
            catch (Exception ex)
            {
                response.Message = "Error 500 Server";
            }

            return response;
        }
    }
}
