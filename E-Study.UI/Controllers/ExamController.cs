using Microsoft.AspNetCore.Mvc;

namespace E_Study.UI.Controllers
{
    public class ExamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
