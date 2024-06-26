﻿using AutoMapper;
using E_Study.Core.Data;
using E_Study.Core.Models;
using E_Study.Repository.Infrastructures;
using E_Study.ViewModel;
using E_Study.ViewModel.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
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
                var exam = uow.ExamRepository.GetExamWithQnAsId(examId);
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
                model.Attempt = attempt + 1;
                foreach (var item in model.QnAs)
                {
                    ExamResult examResult = new ExamResult();
                    examResult.UserId = model.StudentId;
                    examResult.QnAsId = item.Id;
                    examResult.ExamId = item.ExamId;
                    examResult.Answer = item.SelectedAnswer;

                    if (string.IsNullOrEmpty(item.SelectedAnswer))
                    {
                        examResult.IsCorrect = false;
                    }
                    else
                    {
                        // Check correctness based on question type
                        if (item.Type == "Multiple Choice")
                        {
                            // For multiple-choice questions, check if all selected answers match the correct answers
                            var correctAnswers = item.Answer.Split(',').ToList(); // Assuming correct answers are comma-separated
                            var selectedAnswers = item.SelectedAnswer.Split(',').ToList(); // Assuming selected answers are comma-separated
                            examResult.IsCorrect = correctAnswers.All(selectedAnswers.Contains) && correctAnswers.Count == selectedAnswers.Count;
                        }
                        else
                        {
                            // For single-choice questions, check if the selected answer matches the correct answer
                            examResult.IsCorrect = item.SelectedAnswer == item.Answer;
                        }
                    }

                    //examResult.IsCorrect = item.SelectedAnswer == item.Answer;
                    examResult.Attempt = model.Attempt;

                    uow.ExamResultRepository.Create(examResult);
                }
                Debug.WriteLine($"ExamId: {model.ExamId}, StudentId: {model.StudentId}, Attempt: {model.Attempt}");
                //Create new grade for the user
                var grade = new Grade()
                {
                    Id = Guid.NewGuid().ToString(),
                    ExamId = model.ExamId,
                    UserId = model.StudentId,
                    Score = model.QnAs.Count(q => {
                        // Check if the answer is correct based on the question type
                        if (string.IsNullOrEmpty(q.SelectedAnswer))
                        {
                            return false;
                        }

                        // Check if the answer is correct based on the question type
                        if (q.Type == "Multiple Choice")
                        {
                            // For multiple-choice questions, split the correct answers and selected answers
                            var correctAnswers = q.Answer.Split(',').ToList(); // Assuming correct answers are comma-separated
                            var selectedAnswers = q.SelectedAnswer.Split(',').ToList(); // Assuming selected answers are comma-separated
                                                                                        // Count if all selected answers are contained in the correct answers and vice versa
                            return correctAnswers.All(selectedAnswers.Contains) && correctAnswers.Count == selectedAnswers.Count;
                        }
                        else
                        {
                            return q.SelectedAnswer == q.Answer;
                        }
                    }),
                    Attempt = model.Attempt,
                    DateTime = DateTime.Now
                };
                uow.GradeRepository.Create(grade);
                uow.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"Error creating grade: {ex.Message}");
                return false;
            }
        }

        public bool SetGrade(StartExamViewModel model)
        {
            try
            {
                var attempt = uow.ExamRepository.GetUserExamAttempts(model.StudentId, model.ExamId).Count();
                //Create new grade for the user
                Grade grade = new Grade
                {
                    Id = Guid.NewGuid().ToString(),
                    ExamId = model.ExamId,
                    UserId = model.StudentId,
                    Score = model.QnAs.Count(q => q.SelectedAnswer == q.Answer),
                    Attempt = attempt + 1,
                    DateTime = DateTime.Now
                };
                uow.GradeRepository.Create(grade);
                uow.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public ResultViewModel GetExamResult(string userId, string examId, int attempt)
        {
            try
            {
                var examResults = uow.ExamResultRepository.GetAll()
                    .Where(er => er.UserId == userId && er.ExamId == examId && er.Attempt == attempt)
                    .ToList(); // Fetch exam results for the specified user and exam

                var exam = uow.ExamRepository.GetById(examId); // Fetch the exam details


                var correctAnswers = examResults.Count(er => er.IsCorrect); // Count correct answers

                var wrongAnswers = examResults.Count(er => !er.IsCorrect); // Count wrong answers

                var resultViewModel = new ResultViewModel
                {
                    StudentId = userId,
                    ExamName = exam.Title,
                    Attempt = attempt,
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
