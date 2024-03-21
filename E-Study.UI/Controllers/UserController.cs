using E_Study.Core.Models;
using E_Study.Repository.Infrastructures;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
    }
}
