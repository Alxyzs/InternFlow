using InternFlow.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternFlow.MVC.Controllers
{
    [Authorize]
    public class ActivityLogController : Controller
    {
        private readonly IActivityLogService _activityLogService;

        public ActivityLogController(IActivityLogService activityLogService)
        {
            _activityLogService = activityLogService;
        }

        public IActionResult Index()
        {
            var logs = _activityLogService.GetAll();
            return View(logs);
        }
    }
}