using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskSched.Data.Models;

namespace TaskSched.Data.EntitiesConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.ToTable("Users")
                   .HasKey(p => p.Id);

            builder.Property(u => u.Id)
              .HasColumnType("uniqueidentifier")
              .ValueGeneratedNever();

            builder.Property(u => u.FirstName)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(u => u.LastName)
                   .HasMaxLength(50)
                   .IsRequired();
        }
    }
}
