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
                TempData["Success"] = "Comment added successfully.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("Detail", "Task", new { id = comment.TaskItemId });
        }

        public IActionResult Delete(int id, int taskItemId)
        {
            try
            {
                _commentService.Delete(id);
                TempData["Success"] = "Comment deleted successfully.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("Detail", "Task", new { id = taskItemId });
        }
    }
}