using E_Study.Core.Models;
using E_Study.Repository.Infrastructures;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Study.UI.Controllers
{
    public class CourseController : Controller
    {
        private readonly IUnitOfWork uow;
        private readonly IWebHostEnvironment webHostEnvironment;
        public readonly UserManager<User> userManager;
        public CourseController(IUnitOfWork uow, IWebHostEnvironment webHostEnvironment, UserManager<User> userManager)
        {
            this.uow = uow;
            this.webHostEnvironment = webHostEnvironment;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index(string courseId)
        {           
            return View();
        }

        public async Task<IActionResult> CreatePost(string courseId)
        {
            return View();
        }

        public IActionResult Students(string courseId)
        {
            if (courseId == null)
            {
                // Handle the case where CourseId is null
                return RedirectToAction("Index", "Home"); // Redirect to a default page
            }
            else
            {
                var students =  uow.CourseRepository.GetUsersInCourse(courseId);
                ViewBag.CurrentCourseId = courseId;                
                return View(students);
            }
        }

        public async Task<IActionResult> Chat(string courseId)
        {
            if (courseId == null)
            {
                // Handle the case where CourseId is null
                return RedirectToAction("Index", "Home"); // Redirect to a default page
            }
            else
            {
                var currentUser = await userManager.GetUserAsync(User);
                ViewBag.CurrentUserName = currentUser.UserName;
                ViewBag.CurrentCourseId = courseId;
                var messages = await uow.MessageRepository.GetMessagesByCourseIdAsync(courseId);
                TempData["CourseId"] = courseId;
                return View(messages);
            }
           
        }
        [HttpPost]
        public async Task<IActionResult> SendMessage(Message message, string courseId)
        {
            if (ModelState.IsValid)
            {
                message.Username = User.Identity.Name;
                var sender = await userManager.GetUserAsync(User);
                message.UserId = sender.Id;
                message.CourseId = courseId; // Use the CourseId passed from the form
                await uow.MessageRepository.CreateAsync(message);
                await uow.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }
    }
}
