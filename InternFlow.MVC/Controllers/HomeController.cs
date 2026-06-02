using InternFlow.BLL.Interfaces;
using InternFlow.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace InternFlow.MVC.Controllers
{
    [Authorize]//Burası login olduktan sonra erisebilinme sayfası yapmak  icin yanı ilerde admin olanlar girebilir mesala bu kontrollu koymak ıcın .
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IProjectService _projectService;
        private readonly ITaskService _taskService;

        public HomeController(IUserService userService, IProjectService projectService, ITaskService taskService)
        {
            _userService = userService;
            _projectService = projectService;
            _taskService = taskService;
        }

        public IActionResult Index()
        {
            var users = _userService.GetAll();
            var projects = _projectService.GetAll();
            var tasks = _taskService.GetAll();

            ViewBag.TotalUsers = users.Count;
            ViewBag.TotalProjects = projects.Count;

            ViewBag.ActiveTasks = tasks.Count(t => t.Status == "Active");

            ViewBag.CompletedTasks = tasks.Count(t => t.Status == "Done");

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}