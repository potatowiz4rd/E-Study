using E_Study.Core.Models;
using E_Study.Repository.Infrastructures;
using E_Study.Service.exam;
using E_Study.Service.qnas;
using E_Study.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Study.UI.Controllers
{
    public class ExamController : Controller
    {
        private readonly IUnitOfWork uow;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IExamService examService;
        private readonly IQnAsService qnAsService;
        public readonly UserManager<User> userManager;
        public ExamController(IUnitOfWork uow, IWebHostEnvironment webHostEnvironment, UserManager<User> userManager, IExamService examService, IQnAsService qnAsService)
        {
            this.uow = uow;
            this.webHostEnvironment = webHostEnvironment;
            this.userManager = userManager;
            this.examService = examService;
            this.qnAsService = qnAsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Exams()
        {
            var currentUser = userManager.GetUserId(User);
            var result = examService.GetAllExamCreatedByUser(currentUser);
            var exams = result.DataList;
            return View(exams);
        }

        [HttpGet]
        public IActionResult CreateExam()
        {
            ExamViewModel examViewModel = new ExamViewModel { QnAs = new List<QnAsViewModel>() };
            return View(examViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateExam(ExamViewModel examViewModel)
        {
            var currentUser = await userManager.GetUserAsync(User);
            examViewModel.AuthorId = currentUser.Id;
            await examService.AddExamAsync(examViewModel);
            return RedirectToAction("Exams");
        }

        [HttpGet]
        public IActionResult StartExam(string examId)
        {
            var currentUser = userManager.GetUserId(User);
            HttpContext.Session.SetString("ExamId", examId); // Set examId in session
            var model = new StartExamViewModel();
        
            if (currentUser != null && examId != "")
            {
                var exam = examService.GetExamById(examId).Data;
                
                TimeSpan timeLimit = TimeSpan.FromMinutes(exam.Time);

                model.QnAs = new List<QnAsViewModel>();
                var response = qnAsService.GetAllQnAsInExam(examId);
                if (exam != null && response.IsSuccessed)
                {
                    model.ExamId = examId;
                    model.StudentId = currentUser;
                    model.QnAs = response.Data;
                    model.ExamName = exam.Title;
                    model.RemainingTime = timeLimit;
                    model.CurrentPage = 1; // Set the current page
                    model.TotalItems = exam.QnAs.Count(); // Set the total number of items
                    model.ItemsPerPage = 1; // Set the number of items per page
                }
                return View(model);
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult StartExam(StartExamViewModel model)
        {
            if (examService.SetExamResult(model))
            {
                return RedirectToAction("ViewResult");
            }
            return RedirectToAction("Exams");
        }

        public IActionResult ViewResult()
        {
            string examId = HttpContext.Session.GetString("ExamId");
            var currentUser = userManager.GetUserId(User);
            if (currentUser != null && examId != "")
            {
                var model = examService.GetExamResult(currentUser, examId);
                return View(model);
            }
            return RedirectToAction("Login", "Account");
        }

    }
}
