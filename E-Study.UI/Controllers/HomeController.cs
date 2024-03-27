using E_Study.Core.Models;
using E_Study.Repository.Infrastructures;
using E_Study.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace E_Study.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _uow;


        public HomeController(ILogger<HomeController> logger, IUnitOfWork uow, UserManager<User> userManager)
        {
            _logger = logger;
            _userManager = userManager;
            _uow = uow;
        }

        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var courses = _uow.CourseRepository.GetAllCourseOfUser(userId);
            return View(courses);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}