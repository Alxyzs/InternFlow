using Microsoft.AspNetCore.SignalR;

namespace InternFlow.MVC.Hubs
{
    public class NotificationHub : Hub
    {
        // Görev durumu değişince herkese bildir
        public async Task TaskStatusChanged(int taskId, string newStatus, string taskTitle)
        {
            await Clients.All.SendAsync("ReceiveStatusUpdate", taskId, newStatus, taskTitle);
        }

        // Yorum eklenince bildirir
        public async Task CommentAdded(int taskId, string content, string userName, int commentId, int userId, string createdAt)
        {
            await Clients.All.SendAsync("ReceiveComment", taskId, content, userName, commentId, userId, createdAt);
        }

        // Proje eklenince bildirir
        public async Task ProjectAdded(string projectName)
        {
            await Clients.All.SendAsync("ReceiveProjectNotification", projectName);
        }
    }
}