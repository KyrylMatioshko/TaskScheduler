using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TaskSched.Data.Models;

namespace TaskSched.Data.EntitiesConfiguration
{
	public class CompanyDetailsConfiguration : IEntityTypeConfiguration<CompanyDetails>
	{
		public void Configure(EntityTypeBuilder<CompanyDetails> builder)
		{
			builder.ToTable("CompanyDetails")
				   .HasKey(cd => cd.CompanyDetailsId);


			builder.Property(cd => cd.CompanyDetailsId)
				   .HasColumnType("uniqueidentifier")
				   .ValueGeneratedNever();

			builder.HasIndex(cd => cd.Name)
				   .IsUnique();

			builder.Property(cd => cd.Name)
				   .HasMaxLength(30)
				   .IsRequired();

			builder.Property(cd => cd.Country)
				   .HasMaxLength(50)
				   .IsRequired(false);

			builder.Property(cd => cd.City)
				  .HasMaxLength(50)
				  .IsRequired(false);

			builder.Property(cd => cd.Street)
				   .HasMaxLength(100)
				   .IsRequired(false);

			builder.Property(cd => cd.Email)
				   .HasMaxLength(100)
				   .IsRequired(false);

			builder.Property(cd => cd.Phone)
				   .HasMaxLength(30)
				   .IsRequired(false);


		}
	}
}
