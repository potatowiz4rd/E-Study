﻿using E_Study.Core.Models;
using E_Study.Repository.Infrastructures;
using E_Study.Service.course;
using E_Study.Service.exam;
using E_Study.Service.post;
using E_Study.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OpenAI_API.Chat;
using OpenAI_API;

namespace E_Study.UI.Controllers
{
    public class CourseController : Controller
    {
        private readonly IUnitOfWork uow;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IPostService postService;
        private readonly IExamService examService;
        private readonly ICourseService courseService;
        public readonly UserManager<User> userManager;
        public CourseController(IUnitOfWork uow,
            IWebHostEnvironment webHostEnvironment,
            UserManager<User> userManager,
            IPostService postService,
            IExamService examService,
            ICourseService courseService)
        {
            this.uow = uow;
            this.webHostEnvironment = webHostEnvironment;
            this.userManager = userManager;
            this.postService = postService;
            this.examService = examService;
            this.courseService = courseService;
        }

        public async Task<IActionResult> NewFeed(string courseId)
        {
            ViewBag.Current = "NewFeed";
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
            return PartialView("_CreatePost");
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
                return RedirectToAction("NewFeed", new { courseId });
            }
            return PartialView("_CreatePost", post);
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
            ViewBag.Current = "Students";

            ViewData["CurrentCourseId"] = courseId;
            if (courseId == null)
            {
                // Handle the case where CourseId is null
                return RedirectToAction("Index", "Home"); // Redirect to a default page
            }
            else
            {
                var students = uow.CourseRepository.GetUsersInCourse(courseId);
                return View(students);
            }
        }

        public IActionResult Events(string courseId)
        {
            ViewBag.Current = "Events";
            ViewBag.Exams = uow.ExamRepository.GetExamCoursesInCourse(courseId);
            ViewData["CurrentCourseId"] = courseId;
            if (courseId == null)
            {
                // Handle the case where CourseId is null
                return RedirectToAction("Index", "Home"); // Redirect to a default page
            }
            else
            {
                var events = uow.EventRepository.GetEventsInCourse(courseId);
                return View(events);
            }
        }

        [HttpGet]
        public IActionResult CreateEvent(string courseId)
        {
            ViewBag.CurrentCourseId = courseId;
            ViewData["CurrentCourseId"] = courseId;

            return PartialView("_CreateEvent");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEvent(Event @event, string courseId)
        {
            if (ModelState.IsValid)
            {
                // Handle file upload
                if (@event.File != null && @event.File.Length > 0)
                {
                    // Create the uploads directory if it doesn't exist
                    var uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", courseId);
                    if (!Directory.Exists(uploadsDirectory))
                    {
                        Directory.CreateDirectory(uploadsDirectory);
                    }

                    // Process the uploaded file
                    var fileName = Path.GetFileName(@event.File.FileName);
                    var filePath = Path.Combine(uploadsDirectory, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await @event.File.CopyToAsync(stream);
                    }

                    @event.FileName = fileName;
                    @event.FilePath = filePath;
                    @event.CourseId = courseId;
                }

                await uow.EventRepository.CreateAsync(@event);
                await uow.SaveChangesAsync();
                return RedirectToAction("Events", new { courseId });
            }
            return View(@event);
        }

        public IActionResult Calendar(string courseId)
        {
            ViewBag.Current = "Calendar";

            ViewData["CurrentCourseId"] = courseId;
            if (courseId == null)
            {
                // Handle the case where CourseId is null
                return RedirectToAction("Index", "Home"); // Redirect to a default page
            }
            else
            {
                return View();
            }
        }

        public IActionResult Files(string courseId)
        {
            ViewBag.Current = "Files";
            ViewData["CurrentCourseId"] = courseId;
            if (courseId == null)
            {
                // Handle the case where CourseId is null
                return RedirectToAction("Index", "Home"); // Redirect to a default page
            }
            else
            {
                var events = uow.EventRepository.GetEventsInCourse(courseId);
                return View(events);
            }
        }

        public IActionResult GetFile(string filePath)
        {
            // Combine the base path with the provided file path
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filePath);

            // Check if the file exists
            if (!System.IO.File.Exists(fullPath))
            {
                return NotFound();
            }

            // Return the file using a file result
            return PhysicalFile(fullPath, "application/pdf"); // Adjust the content type as needed
        }

        public IActionResult UploadFile(string courseId)
        {
            ViewBag.CurrentCourseId = courseId;
            ViewData["CurrentCourseId"] = courseId;

            return PartialView("_UploadFile");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadFile(Event @event, string courseId)
        {
            if (ModelState.IsValid)
            {
                // Handle file upload
                if (@event.File != null && @event.File.Length > 0)
                {
                    // Create the uploads directory if it doesn't exist
                    var uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", courseId);
                    if (!Directory.Exists(uploadsDirectory))
                    {
                        Directory.CreateDirectory(uploadsDirectory);
                    }

                    // Process the uploaded file
                    var fileName = Path.GetFileName(@event.File.FileName);
                    var filePath = Path.Combine(uploadsDirectory, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await @event.File.CopyToAsync(stream);
                    }
                    @event.EventType = "File";
                    @event.Text = "New File";
                    @event.FileName = fileName;
                    @event.FilePath = filePath;
                    @event.CourseId = courseId;
                }

                await uow.EventRepository.CreateAsync(@event);
                await uow.SaveChangesAsync();
                return RedirectToAction("Files", new { courseId });
            }
            return View(@event);
        }

        public IActionResult Exams(string courseId)
        {
            ViewBag.Current = "Exams";

            ViewData["CurrentCourseId"] = courseId;
            if (courseId == null)
            {
                // Handle the case where CourseId is null
                return RedirectToAction("Index", "Home"); // Redirect to a default page
            }
            else
            {
                var exams = uow.ExamRepository.GetExamCoursesInCourse(courseId);
                //var exams = uow.ExamRepository.GetExamsInCourse(courseId);
                return View(exams);
            }
        }

        public IActionResult AddExamToCourse(string courseId)
        {
            ViewData["CurrentCourseId"] = courseId;
            var response = examService.GetAllExamCreatedByUser(userManager.GetUserId(User));
            ViewBag.Exams = new SelectList(response.DataList, "Id", "Title");
            return View();
        }

        [HttpPost]
        public IActionResult AddExamToCourse(ExamCourseViewModel model, string courseId)
        {
            ViewData["CurrentCourseId"] = courseId;

            if (ModelState.IsValid)
            {
                var response = examService.AddExamToCourse(model);
                if (response.IsSuccessed)
                {
                    return RedirectToAction("Exams", new { courseId });
                }
                else
                {
                    return View("Error");
                }
            }

            return View("Exams", new { courseId });
        }

        public async Task<IActionResult> Chat(string courseId)
        {
            ViewBag.Current = "Chat";
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

        public IActionResult ExamGrades(string courseId, string examId)
        {
            ViewData["CurrentCourseId"] = courseId;
            if (courseId == null)
            {
                // Handle the case where CourseId is null
                return RedirectToAction("Index", "Home"); // Redirect to a default page
            }
            else
            {
                var grades = uow.GradeRepository.GetGradesOfExamInCourse(courseId, examId);
                return View(grades);
            }
        }

        public IActionResult CourseSettings(string courseId)
        {
            ViewBag.Current = "CourseSettings";

            ViewData["CurrentCourseId"] = courseId;
            if (courseId == null)
            {
                // Handle the case where CourseId is null
                return RedirectToAction("Index", "Home"); // Redirect to a default page
            }
            else
            {
                var course = uow.CourseRepository.GetById(courseId);
                return View(course);
            }
        }

        public IActionResult CreateCoursePartial()
        {
            return PartialView("_CreateCoursePartial");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCoursePartial(Course course)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await userManager.GetUserAsync(User);
                var userId = currentUser.Id;
                uow.CourseRepository.Create(course);
                uow.CourseRepository.AddUserToCourse(userId, course.Id);
                uow.SaveChanges();
                return RedirectToAction("Index", "Home");

            }
            return View();
        }

        [HttpPost]
        public IActionResult DeleteCourse(string courseId)
        {
            uow.CourseRepository.Delete(courseId);
            uow.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult DeleteExamCourse(string examId, string courseId)
        {
            try
            {
                // Log or place a breakpoint here to ensure the method is called
                Console.WriteLine($"Attempting to delete ExamCourse with ExamId: {examId} and CourseId: {courseId}");

                var assignment = uow.ExamCourseRepository.GetById(examId, courseId);

                uow.ExamCourseRepository.Delete(assignment);

                // Log or check if the object was found and deleted
                Console.WriteLine("Deletion method called");

                uow.SaveChanges();

                // Log success
                Console.WriteLine("Changes saved successfully");

                return RedirectToAction("Exams", new { courseId });
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"Error occurred: {ex.Message}");
                return RedirectToAction("Exams", new { courseId, error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddUsersToCourse(string courseId)
        {
            ViewBag.CurrentCourseId = courseId;
            ViewData["CurrentCourseId"] = courseId;

            var users = await uow.UserRepository.GetUsersNotInCourse(courseId);
            return PartialView("_AddUsersToCourse", users);
        }

        [HttpPost]
        public async Task<IActionResult> AddUsersToCourse(string courseId, List<string> userIds)
        {
            var course = await uow.CourseRepository.GetByIdAsync(courseId);
            if (course == null)
            {
                return NotFound();
            }

            foreach (var userId in userIds)
            {
                var userCourse = new UserCourse
                {
                    CourseId = courseId,
                    UserId = userId
                };
                uow.UserCourseRepository.Create(userCourse);
            }

            await uow.SaveChangesAsync();
            return RedirectToAction("Students", new { id = courseId });
        }

        [HttpPost]
        public IActionResult DeleteEvent(string eventId, string courseId)
        {
            uow.EventRepository.Delete(eventId);
            uow.SaveChanges();
            return RedirectToAction("Files", new { courseId });
        }
    }
}


