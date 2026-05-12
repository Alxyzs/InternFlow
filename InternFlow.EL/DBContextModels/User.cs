using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternFlow.EL.DBContextModels
{
    public class User
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<TaskItem>? AssignedTasks { get; set; }
        public ICollection<ProjectMember>? ProjectMembers { get; set; }
    }
}
