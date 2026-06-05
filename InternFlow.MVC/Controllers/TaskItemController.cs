using InternFlow.BLL.Interfaces;
using InternFlow.BLL.Services;
using InternFlow.EL.DBContextModels;
using InternFlow.MVC.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace InternFlow.MVC.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly ICommentService _commentService;
        private readonly IProjectMemberService _projectMemberService;
        private readonly IActivityLogService _activityLogService;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly IUserService _userService;

        public TaskController(ITaskService taskService, ICommentService commentService, IProjectMemberService projectMemberService, IActivityLogService activityLogService, IHubContext<NotificationHub> hubContext, IUserService userService)
        {
            _taskService = taskService;
            _commentService = commentService;
            _projectMemberService = projectMemberService;
            _activityLogService = activityLogService;
            _hubContext = hubContext;
            _userService = userService;
        }

        public IActionResult Index()
        {
            var tasks = _taskService.GetAll();
            return View(tasks);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TaskItem task)
        {
            try
            {
                _taskService.Add(task);

                _activityLogService.Add(new ActivityLog
                {
                    TaskItemId = task.Id,
                    UserId = task.AssignedUserId ?? 1,
                    Action = "Task Created",
                    Detail = $"{task.Title} created.",
                    CreatedAt = DateTime.Now
                });

                // Kullanıcı projeye üye değilse ekle
                if (task.AssignedUserId.HasValue)
                {
                    var isMember = _projectMemberService.GetAll()
                        .Any(pm => pm.ProjectId == task.ProjectId
                                && pm.UserId == task.AssignedUserId.Value);

                    if (!isMember)
                    {
                        _projectMemberService.Add(new ProjectMember
                        {
                            ProjectId = task.ProjectId,
                            UserId = task.AssignedUserId.Value
                        });
                    }
                }

                TempData["Success"] = "Task created successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(task);
            }
        }

        public IActionResult Edit(int id)
        {
            var task = _taskService.GetById(id);
            return View(task);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(TaskItem task)
        {
            var existing = _taskService.GetById(task.Id);

            existing.Title = task.Title;
            existing.Description = task.Description;
            existing.Status = task.Status;
            existing.Priority = task.Priority;
            existing.DueDate = task.DueDate;
            existing.AssignedUserId = task.AssignedUserId;
            existing.ProjectId = task.ProjectId;

            _taskService.Update(existing);

            await _hubContext.Clients.All.SendAsync(
                "ReceiveStatusUpdate",
                task.Id,
                task.Status,
                task.Title);

            TempData["Success"] = "Task updated successfully.";

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _taskService.Delete(id);

            TempData["Success"] = "Task deleted successfully.";

            return RedirectToAction("Index");
        }

        public IActionResult Detail(int id)
        {
            var task = _taskService.GetById(id);
            var comments = _commentService.GetAll()
                .Where(c => c.TaskItemId == id)
                .ToList();

            var users = _userService.GetAll();

            ViewBag.Comments = comments;
            ViewBag.Users = users;
            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var task = _taskService.GetById(id);
            _taskService.UpdateStatus(id, status);

            _activityLogService.Add(new ActivityLog
            {
                TaskItemId = id,
                UserId = task.AssignedUserId ?? 1,
                Action = "Status Changed",
                Detail = $"{task.Title} → {status}",
                CreatedAt = DateTime.Now
            });

            // SignalR ile herkese bildir
            await _hubContext.Clients.All.SendAsync(
                "ReceiveStatusUpdate",
                id,
                status,
                task.Title);

            TempData["Success"] = $"{task.Title} completed.";

            return RedirectToAction("Index");
        }

        //Kanban Board için 
        [Authorize(Roles = "Admin")]
        public IActionResult Kanban()
        {
            var tasks = _taskService.GetAll();

            ViewBag.New = tasks.Where(t => t.Status == "Active").ToList();
            ViewBag.InProgress = tasks.Where(t => t.Status == "Passive").ToList();
            ViewBag.Testing = tasks.Where(t => t.Status == "Pending").ToList();
            ViewBag.Completed = tasks.Where(t => t.Status == "Completed").ToList();

            return View();
        }
    }
}