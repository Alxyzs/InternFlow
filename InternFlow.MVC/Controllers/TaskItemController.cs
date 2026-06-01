using InternFlow.BLL.Interfaces;
using InternFlow.EL.DBContextModels;
using Microsoft.AspNetCore.Mvc;

namespace InternFlow.MVC.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly ICommentService _commentService;

        public TaskController(ITaskService taskService, ICommentService commentService)
        {
            _taskService = taskService;
            _commentService = commentService;
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