using InternFlow.BLL.Interfaces;
using InternFlow.EL.DBContextModels;
using Microsoft.AspNetCore.Mvc;

namespace InternFlow.MVC.Controllers
{
    public class TaskCommentController : Controller
    {
        private readonly ICommentService _commentService;

        public TaskCommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public IActionResult Add(TaskComment comment)
        {
            try
            {
                _commentService.Add(comment);
                return RedirectToAction("Detail", "Task", new { id = comment.TaskItemId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("Detail", "Task", new { id = comment.TaskItemId });
            }
        }

        public IActionResult Delete(int id, int taskItemId)
        {
            _commentService.Delete(id);
            return RedirectToAction("Detail", "Task", new { id = taskItemId });
        }
    }
}