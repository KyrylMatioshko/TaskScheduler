using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TaskSched.Data.Models;

namespace TaskSched.Data.EntitiesConfiguration
{
	public class ProjectDisplayStateConfiguration : IEntityTypeConfiguration<ProjectDisplayState>
	{
		public void Configure(EntityTypeBuilder<ProjectDisplayState> builder)
		{
			builder.ToTable("ProjectDisplayStates")
				   .HasKey(pds => pds.Id);

			builder.Property(pds => pds.Id)
				   .HasColumnType("uniqueidentifier")
				   .ValueGeneratedNever();

			builder.HasOne(pds => pds.Project)
				   .WithOne(u => u.ProjectDisplayState)
				   .HasForeignKey<ProjectDisplayState>(pds => pds.ProjectId)
				   .OnDelete(DeleteBehavior.Cascade);

			builder.Property(pds => pds.SortParams)
				   .IsRequired();

			builder.Property(pds => pds.SortDirection)
				   .IsRequired();
		}
	}
}
