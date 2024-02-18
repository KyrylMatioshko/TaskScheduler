using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Task = TaskSched.Data.Models.Task;

namespace TaskSched.Data.EntitiesConfiguration
{
    public class TaskConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder) 
        {
            builder.ToTable("Tasks")
                   .HasKey(t => t.TaskId);

            builder.Property(u => u.TaskId)
              .HasColumnType("uniqueidentifier")
              .ValueGeneratedNever();

            builder.HasOne(t => t.Project)
                   .WithMany(p => p.Tasks)
                   .HasForeignKey(t => t.ProjectId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(t => t.Name)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(t => t.Description)
                   .HasMaxLength(500)
                   .IsRequired();

            builder.Property(t => t.Priority)
                   .IsRequired();

            builder.Property(t => t.DateCreate)
                   .IsRequired()
                   .HasDefaultValueSql("datetime('now')");

            builder.Property(t => t.DateEnd)
                   .IsRequired();

            builder.Property(p => p.TimeEnd)
                   .IsRequired();
        }
    }
}
