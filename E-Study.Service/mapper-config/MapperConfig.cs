using AutoMapper;
using E_Study.Core.Models;
using E_Study.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Study.Service

{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Course, CourseViewModel>().ReverseMap();           
        }
    }
}
