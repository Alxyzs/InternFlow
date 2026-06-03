using InternFlow.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternFlow.MVC.Controllers
{
    [Authorize]
    public class ActivityLogController : Controller
    {
        private readonly IActivityLogService _activityLogService;
        private readonly IUserService _userService;
        private readonly ITaskService _taskService;

        public ActivityLogController(IActivityLogService activityLogService, IUserService userService, ITaskService taskService)
        {
            _activityLogService = activityLogService;
            _userService = userService;
            _taskService = taskService;
        }

        public IActionResult Index()
        {
            var logs = _activityLogService.GetAll();
            var users = _userService.GetAll();
            var tasks = _taskService.GetAll();

            ViewBag.Users = users;
            ViewBag.Tasks = tasks;

            return View(logs);
        }
    }
}