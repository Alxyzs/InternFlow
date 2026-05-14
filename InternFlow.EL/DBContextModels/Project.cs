namespace InternFlow.EL.DBContextModels
{
    public class Project
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public string Status { get; set; } = "Pending";

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<TaskItem>? Tasks { get; set; }
        public ICollection<ProjectMember>? ProjectMembers { get; set; }
    }
}