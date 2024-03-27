using E_Study.Core.Models;
using E_Study.Repository.Infrastructures;
using E_Study.Service.course;
using E_Study.Service.post;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Study.UI.Controllers
{
    public class CourseController : Controller
    {
        private readonly IUnitOfWork uow;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IPostService postService;
        public readonly UserManager<User> userManager;
        public CourseController(IUnitOfWork uow, IWebHostEnvironment webHostEnvironment, UserManager<User> userManager, IPostService postService)
        {
            this.uow = uow;
            this.webHostEnvironment = webHostEnvironment;
            this.userManager = userManager;
            this.postService = postService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> NewFeed(string courseId)
        {
            ViewData["CurrentCourseId"] = courseId;
            var currentUser = await userManager.GetUserAsync(User);
            ViewBag.CurrentUserName = currentUser.UserName;
            ViewBag.CurrentCourseId = courseId;
            //var posts = await uow.PostRepository.GetPostsByCourseIdAsync(courseId);
            var response = await postService.GetAllPostsInCourseAsync(courseId);

            // Check if the operation was successful before passing data to the view
            if (response.IsSuccessed)
            {
                // Pass the list of posts to the view
                return View(response.DataList);
            }
            else
            {
                // Handle the error condition appropriately
                // For now, just return a generic error view
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult CreatePost(string courseId)
        {
            ViewBag.CurrentCourseId = courseId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(Post post, string courseId)
        {
            
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                post.UserId = user.Id;
                post.CourseId = courseId; // Use the CourseId passed from the form
                await uow.PostRepository.CreateAsync(post);
                await uow.SaveChangesAsync();
                return RedirectToAction("Index", new { courseId });
            }
            return BadRequest();
        }

        public async Task<IActionResult> CreateComment(Comment comment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Retrieve the post associated with the comment
                    var post = await uow.PostRepository.GetByIdAsync(comment.PostId);                  
                    if (post != null)
                    {
                        // Assuming you have the current user available (e.g., through UserManager)
                        var currentUser = await userManager.GetUserAsync(User);

                        // Assign the current user to the comment
                        comment.UserId = currentUser.Id;

                        // Add the comment to the post
                        post.Comments.Add(comment);

                        // Save changes to the database
                        await uow.SaveChangesAsync();

                        // Prepare the comment data to send back to the client
                        var commentData = new
                        {
                            Text = comment.Text,
                            UserName = currentUser.UserName,
                            CreatedAt = comment.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss")
                        };

                        // Return the newly created comment as JSON
                        return Json(commentData);
                    }
                    else
                    {
                        // Handle the case where the post does not exist
                        return NotFound();
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception or handle it appropriately
                    Console.WriteLine($"An error occurred while adding the comment: {ex.Message}");
                    return StatusCode(500, "An error occurred while processing your request.");
                }
            }

            // If model validation fails, return a BadRequest response with validation errors
            return BadRequest(ModelState);
        }

        public IActionResult Students(string courseId)
        {
            ViewData["CurrentCourseId"] = courseId;
            if (courseId == null)
            {
                // Handle the case where CourseId is null
                return RedirectToAction("Index", "Home"); // Redirect to a default page
            }
            else
            {
                var students =  uow.CourseRepository.GetUsersInCourse(courseId);
                return View(students);
            }
        }

        public IActionResult Exams(string courseId)
        {
            ViewData["CurrentCourseId"] = courseId;
            if (courseId == null)
            {
                // Handle the case where CourseId is null
                return RedirectToAction("Index", "Home"); // Redirect to a default page
            }
            else
            {
                var exams = uow.ExamRepository.GetExamsInCourse(courseId);
                return View(exams);
            }
        }

        public async Task<IActionResult> Chat(string courseId)
        {
            ViewData["CurrentCourseId"] = courseId;

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
