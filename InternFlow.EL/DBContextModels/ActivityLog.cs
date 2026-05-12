using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternFlow.EL.DBContextModels
{
    public class ActivityLog
    {
        public int Id { get; set; }
        public string? Action { get; set; }
        public string? Detail { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int TaskItemId { get; set; }
        public TaskItem? TaskItem { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
