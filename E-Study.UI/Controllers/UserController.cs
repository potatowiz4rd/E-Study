using E_Study.Core.Models;
using E_Study.Repository.Infrastructures;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Study.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUnitOfWork uow;
        private readonly IWebHostEnvironment webHostEnvironment;
        public readonly UserManager<User> userManager;
        public UserController(IUnitOfWork uow, IWebHostEnvironment webHostEnvironment, UserManager<User> userManager)
        {
            this.uow = uow;
            this.webHostEnvironment = webHostEnvironment;
            this.userManager = userManager;
        }
        public IActionResult Profile()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddUserToCourse(string courseId)
        {
            ViewBag.CourseId = courseId;
            ViewBag.Users = new SelectList(uow.UserRepository.GetAll(), "Id", "UserName");
            return View();
        }

        [HttpPost]
        public IActionResult AddUserToCourse(UserCourse userCourse)
        {
            if (ModelState.IsValid)
            {
                uow.UserCourseRepository.Create(userCourse);
                uow.SaveChanges();

                return RedirectToAction("Detail", new { id = userCourse.CourseId });
            }

            ViewBag.Users = new SelectList(uow.UserRepository.GetAll(), "Id", "UserName");
            return View(userCourse);
        }


    }
}
