using InternFlow.BLL.Interfaces;
using InternFlow.EL.DBContextModels;
using InternFlow.MVC.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace InternFlow.MVC.Controllers
{
    public class TaskCommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;
        private readonly IHubContext<NotificationHub> _hubContext;

        public TaskCommentController(ICommentService commentService, IUserService userService, IHubContext<NotificationHub> hubContext)
        {
            _commentService = commentService;
            _userService = userService;
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> Add(TaskComment comment)
        {
            try
            {
                _commentService.Add(comment);

                var user = _userService.GetById(comment.UserId);

                // SignalR ile canlıMesaj gostermek ıcın tum bılgılerı onun uzerınden gonderıyoruz
                await _hubContext.Clients.All.SendAsync(
                    "ReceiveComment",
                    comment.TaskItemId,
                    comment.Content,
                    user?.FullName ?? "Unknown",
                    comment.Id,
                    comment.UserId,
                    comment.CreatedAt.ToString("dd.MM.yyyy HH:mm")
                );

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