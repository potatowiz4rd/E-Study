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
        public async Task<IActionResult> Index(string id)
        {
            var currentUser = await userManager.GetUserAsync(User);
            ViewBag.CurrentUserName = currentUser.UserName;
            ViewBag.CurrentCourseId = id;
            var messages = await uow.MessageRepository.GetMessagesByCourseIdAsync(id);
            TempData["CourseId"] = id;
            return View(messages);
        }
        public async Task<IActionResult> Chat(string id)
        {
            var currentUser = await userManager.GetUserAsync(User);
            ViewBag.CurrentUserName = currentUser.UserName;
            ViewBag.CurrentCourseId = id;
            var messages = await uow.MessageRepository.GetMessagesByCourseIdAsync(id);
            TempData["CourseId"] = id;
            return View("Chat", messages);
        }

        public async Task<IActionResult> SendMessage(Message message)
        {
            if (ModelState.IsValid)
            {
                message.Username = User.Identity.Name;
                var sender = await userManager.GetUserAsync(User);
                message.UserId = sender.Id;
                message.CourseId = TempData["CourseId"].ToString();
                await uow.MessageRepository.CreateAsync(message);
                await uow.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }
    }
}
