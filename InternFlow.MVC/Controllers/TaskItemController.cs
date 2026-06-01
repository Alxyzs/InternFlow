using InternFlow.BLL.Interfaces;
using InternFlow.BLL.Services;
using InternFlow.EL.DBContextModels;
using Microsoft.AspNetCore.Mvc;

namespace InternFlow.MVC.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly ICommentService _commentService;
        private readonly IProjectMemberService _projectMemberService;

        public TaskController(ITaskService taskService, ICommentService commentService, IProjectMemberService projectMemberService)
        {
            _taskService = taskService;
            _commentService = commentService;
            _projectMemberService = projectMemberService;
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

        [HttpPost]
        public IActionResult Edit(TaskItem task)
        {
            _taskService.Update(task);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _taskService.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult Detail(int id)
        {
            var task = _taskService.GetById(id);
            var comments = _commentService.GetAll()
                .Where(c => c.TaskItemId == id)
                .ToList();

            ViewBag.Comments = comments;
            return View(task);
        }

        [HttpPost]
        public IActionResult UpdateStatus(int id, string status)
        {
            _taskService.UpdateStatus(id, status);
            return RedirectToAction("Index");
        }
    }
}