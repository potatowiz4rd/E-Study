using AutoMapper;
using E_Study.Core.Models;
using E_Study.Repository.Infrastructures;
using E_Study.ViewModel;
using E_Study.ViewModel.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Study.Service.exam
{
    public class ExamService : IExamService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public ExamService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public async Task<ResponseResult<ExamViewModel>> AddExamAsync(ExamViewModel examViewModel)
        {
            ResponseResult<ExamViewModel> response = new ResponseResult<ExamViewModel>();

            try
            {
                var exam = mapper.Map<Exam>(examViewModel); // Map examViewModel to Exam object
                await uow.ExamRepository.CreateAsync(exam);

                // Associate QnAs with the exam
                foreach (var qnasViewModel in examViewModel.QnAs)
                {
                    var qnas = mapper.Map<QnAs>(qnasViewModel); // Map QnA ViewModel to QnA entity
                    qnas.ExamId = exam.Id; // Assign exam Id to QnA
                   // exam.QnAs.Add(qnas); // Add QnA to the exam
                }

                await uow.SaveChangesAsync();

                response.IsSuccessed = true;
                response.Data = examViewModel;
                response.Message = "Exam created successfully";
            }
            catch (Exception ex)
            {
                response.Message = $"Error: {ex.Message}"; // Include exception message
            }

            return response;
        }



        public ResponseResult<ExamViewModel> GetAllExamCreatedByUser(string userId)
        {
            ResponseResult<ExamViewModel> response = new ResponseResult<ExamViewModel>();

            try
            {
                var exams = uow.ExamRepository.GetAllExamsOfUser(userId);

                var examModels = mapper.Map<List<ExamViewModel>>(exams);
                return new ResponseResult<ExamViewModel>() { DataList = examModels, Message = "Success" };
            }
            catch (Exception ex)
            {
                response.Message = "Error 500 Server";
            }

            return response;
        }

        public ResponseResult<ExamCourseViewModel> AddExamToCourse(ExamCourseViewModel model)
        {
            ResponseResult<ExamCourseViewModel> response = new ResponseResult<ExamCourseViewModel>();
            try
            {
                uow.ExamRepository.AddExamToCourse(model.ExamId, model.CourseId, model.StartDate, model.EndDate);
                response.IsSuccessed = true;
                response.Data = model;
                response.Message = "Exam added to course successfully";
            }
            catch (Exception ex)
            {
                response.Message = $"Error: {ex.Message}"; // Include exception message
            }

            return response;
        }

        public ResponseResult<ExamViewModel> GetExamById(string examId)
        {
            ResponseResult<ExamViewModel> response = new ResponseResult<ExamViewModel>();
            try
            {
                var exam = uow.ExamRepository.GetById(examId);
                var examModel = mapper.Map<ExamViewModel>(exam);
                response.IsSuccessed = true;
                response.Data = examModel;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Message = $"Error: {ex.Message}"; // Include exception message
            }

            return response;
        }

        public bool SetExamResult(StartExamViewModel model)
        {
            try
            {
                //Check if the user has attempted the exam before and get the attempt count
                var attempt = uow.ExamRepository.GetUserExamAttempts(model.StudentId, model.ExamId).Count();
                    
                foreach (var item in model.QnAs)
                {
                    ExamResult examResult = new ExamResult();
                    examResult.UserId = model.StudentId;
                    examResult.QnAsId = item.Id;
                    examResult.ExamId = item.ExamId;
                    examResult.Answer = item.SelectedAnswer;
                    examResult.IsCorrect = item.SelectedAnswer == item.Answer;
                    examResult.Attempt = attempt + 1;

                    uow.ExamResultRepository.Create(examResult);
                }

                //Create new grade for the user
                Grade grade = new Grade
                {
                    ExamId = model.ExamId,
                    UserId = model.StudentId,
                    Score = model.QnAs.Count(q => q.SelectedAnswer == q.Answer),
                    Attempt = attempt + 1,
                    DateAssigned = DateTime.Now
                };
                uow.GradeRepository.Create(grade);
                uow.SaveChanges(); // Save changes asynchronously

                return true;
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }


        public ResultViewModel GetExamResult(string userId, string examId)
        {
            try
            {
                var examResults = uow.ExamResultRepository.GetAll()
                    .Where(er => er.UserId == userId && er.ExamId == examId)
                    .ToList(); // Fetch exam results for the specified user and exam

                var exam = uow.ExamRepository.GetById(examId); // Fetch the exam details


                var correctAnswers = examResults.Count(er => er.IsCorrect); // Count correct answers

                var wrongAnswers = examResults.Count(er => !er.IsCorrect); // Count wrong answers

                var resultViewModel = new ResultViewModel
                {
                    StudentId = userId,
                    ExamName = exam.Title,
                    CorrectAnswer = correctAnswers,
                    WrongAnswer = wrongAnswers,
                    TotalQuestion = correctAnswers + wrongAnswers
                };

                return resultViewModel;
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"Error: {ex.Message}");
            }

            return new ResultViewModel();
        }

    }
}
