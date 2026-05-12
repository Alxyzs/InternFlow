using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternFlow.EL.DBContextModels
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public int ProjectId { get; set; }
        public Project? Project { get; set; }

        public int? AssignedUserId { get; set; }
        public User? AssignedUser { get; set; }

        public string Status { get; set; } = "Yeni";
        public string Priority { get; set; } = "Normal";

        public DateTime? DueDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<TaskComment>? Comments { get; set; }
        public ICollection<ActivityLog>? ActivityLogs { get; set; }
    }
}
