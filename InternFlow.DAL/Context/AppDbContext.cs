using InternFlow.EL.DBContextModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternFlow.DAL.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User>? Users { get; set; }
        public DbSet<Project>? Projects { get; set; }
        public DbSet<ProjectMember>? ProjectMembers { get; set; }
        public DbSet<TaskItem>? TaskItems { get; set; }
        public DbSet<TaskComment>? TaskComments { get; set; }
        public DbSet<ActivityLog>? ActivityLogs { get; set; }

    }

}
