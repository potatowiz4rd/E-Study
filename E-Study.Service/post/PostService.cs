using AutoMapper;
using E_Study.Repository.Infrastructures;
using E_Study.ViewModel.Responses;
using E_Study.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Study.Service.post
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public PostService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public ResponseResult<PostViewModel> GetAllPostsInCourse(string courseId)
        {
            ResponseResult<PostViewModel> response = new ResponseResult<PostViewModel>();

            try
            {
                var posts = uow.PostRepository.GetPostsByCourseIdAsync(courseId);              
                var postModels = mapper.Map<List<PostViewModel>>(posts);
                return new ResponseResult<PostViewModel>() { DataList = postModels, Message = "Success" };
            }
            catch (Exception ex)
            {
                response.Message = "Error 500 Server";
            }

            return response;
        }

        public async Task<ResponseResult<PostViewModel>> GetAllPostsInCourseAsync(string courseId)
        {
            var response = new ResponseResult<PostViewModel>();
            try
            {
                var posts = await uow.PostRepository.GetPostsByCourseIdAsync(courseId);
                var postModels = mapper.Map<List<PostViewModel>>(posts);

                foreach (var postModel in postModels)
                {
                    var comments = await uow.CommentRepository.GetCommentsByPostIdAsync(postModel.Id);
                    if (comments != null)
                    {
                        postModel.Comments = mapper.Map<List<CommentViewModel>>(comments);
                    }
                }

                response.DataList = postModels;
                response.Message = "Success";
                response.IsSuccessed = true;
                response.StatusCode = 200; // Assuming success status code is 200
            }
            catch (Exception ex)
            {
                response.Message = "Error 500 Server";
                response.IsSuccessed = false;
                response.StatusCode = 500; // Internal Server Error
                                           // Log the exception for debugging
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return response;
        }
    }
}
