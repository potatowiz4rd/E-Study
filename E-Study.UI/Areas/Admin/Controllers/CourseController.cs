using Microsoft.AspNetCore.Mvc;
using E_Study.Repository.Infrastructures;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using E_Study.Core.Models;
using E_Study.Repository.Repositories;

namespace E_Study.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CourseController : Controller
    {
        private readonly IUnitOfWork uow;
        private readonly IWebHostEnvironment webHostEnvironment;
        public CourseController(IUnitOfWork uow, IWebHostEnvironment webHostEnvironment)
        {
            this.uow = uow;
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Courses()
        {
            var result = uow.CourseRepository.GetAll();
            return View(result.ToList());
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Course course)
        {
            uow.CourseRepository.Create(course);
            uow.SaveChanges();
            return RedirectToAction("Courses");
        }

        [HttpGet]
        public IActionResult Detail(string id)
        {
            var result = uow.CourseRepository.GetById(id);
            return View(result);
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
