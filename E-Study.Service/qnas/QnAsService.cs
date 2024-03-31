using AutoMapper;
using E_Study.Repository.Infrastructures;
using E_Study.ViewModel;
using E_Study.ViewModel.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Study.Service.qnas
{
    public class QnAsService : IQnAsService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public QnAsService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        //public ResponseResult<QnAsViewModel> GetAllQnAsInExam(string examId)
        //{
        //    ResponseResult<QnAsViewModel> response = new ResponseResult<QnAsViewModel>();

        //    try
        //    {
        //        var qnas = uow.QnAsRepository.GetAllQnAsInExam(examId);
        //        if (qnas == null)
        //        {
        //            response.Message = "No QnAs found";
        //            return response;
        //        }

        //        response.IsSuccessed = true;
        //        response.Data = mapper.Map<QnAsViewModel>(qnas);
        //        response.Message = "QnAs found successfully";
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Message = $"Error: {ex.Message}";
        //    }

        //    return response;
        //}

        public ResponseResult<List<QnAsViewModel>> GetAllQnAsInExam(string examId)
        {
            ResponseResult<List<QnAsViewModel>> response = new ResponseResult<List<QnAsViewModel>>();

            try
            {
                var qnas = uow.QnAsRepository.GetAllQnAsInExam(examId);
                if (qnas == null || !qnas.Any())
                {
                    response.Message = "No QnAs found";
                    return response;
                }

                response.IsSuccessed = true;
                response.Data = qnas.Select(qna => mapper.Map<QnAsViewModel>(qna)).ToList();
                response.Message = "QnAs found successfully";
            }
            catch (Exception ex)
            {
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

    }
}
