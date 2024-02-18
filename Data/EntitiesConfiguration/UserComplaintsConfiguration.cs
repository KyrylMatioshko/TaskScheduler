using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TaskSched.Data.Models;

namespace TaskSched.Data.EntitiesConfiguration
{
    public class UserComplaintsConfiguration : IEntityTypeConfiguration<UserComplaints>
    {
        public void Configure(EntityTypeBuilder<UserComplaints> builder)
        {
            builder.ToTable("UserComplaints")
                   .HasKey(uc => uc.Id);

            builder.Property(uc => uc.Id)
                   .HasColumnType("uniqueidentifier")
                   .ValueGeneratedNever();

            builder.HasOne(uc => uc.User)
                   .WithMany(u => u.UserComplaints)
                   .HasForeignKey(uc => uc.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(uc => uc.Description)
                   .HasMaxLength(300)
                   .IsRequired();

            builder.Property(uc => uc.Name)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(t => t.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("datetime('now')");

        }
    }
}
