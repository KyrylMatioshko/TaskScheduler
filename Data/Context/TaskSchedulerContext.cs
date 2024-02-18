using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskSched.Data.EntitiesConfiguration;
using TaskSched.Data.Models;
using Task = TaskSched.Data.Models.Task;


namespace TaskSched.Data.Context
{
    public class TaskSchedulerContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public TaskSchedulerContext(DbContextOptions<TaskSchedulerContext> options) : base(options) { }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<CompanyDetails> CompanyDetails { get; set; }

        public DbSet<UserComplaints> UserComplaints { get; set; }

        public DbSet<ProjectDisplayState> ProjectDisplayStates { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new ProjectConfiguration());
            builder.ApplyConfiguration(new TaskConfiguration());
            builder.ApplyConfiguration(new CompanyDetailsConfiguration());
            builder.ApplyConfiguration(new UserComplaintsConfiguration());
			builder.ApplyConfiguration(new ProjectDisplayStateConfiguration());

			base.OnModelCreating(builder);
        }
    }
}
