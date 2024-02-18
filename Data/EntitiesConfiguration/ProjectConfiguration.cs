using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TaskSched.Data.Models;

namespace TaskSched.Data.EntitiesConfiguration
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects")
                   .HasKey(p => p.ProjectId);

            builder.Property(u => u.ProjectId)
              .HasColumnType("uniqueidentifier")
              .ValueGeneratedNever();

            builder.HasOne(p => p.User)
                   .WithMany(u => u.Projects)
                   .HasForeignKey(p => p.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.Name)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(p => p.DateCreated)
                   .IsRequired()
                   .HasDefaultValueSql("datetime('now')");

			builder.HasOne(p => p.ProjectDisplayState)
				   .WithOne(pds => pds.Project)
				   .HasForeignKey<ProjectDisplayState>(pds => pds.ProjectId)
				   .OnDelete(DeleteBehavior.Cascade);
		}
    }
}
