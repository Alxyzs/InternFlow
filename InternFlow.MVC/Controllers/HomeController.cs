using InternFlow.BLL.Interfaces;
using InternFlow.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InternFlow.MVC.Controllers
{
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
            ViewBag.TotalUsers = _userService.GetAll().Count;
            ViewBag.TotalProjects = _projectService.GetAll().Count;
            ViewBag.ActiveTasks = _taskService.GetAll().Count(t => t.Status != "Tamamlandı");
            ViewBag.CompletedTasks = _taskService.GetAll().Count(t => t.Status == "Tamamlandı");

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}