using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternFlow.EL.DBContextModels
{
    public class TaskComment
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int TaskItemId { get; set; }
        public TaskItem? TaskItem { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
