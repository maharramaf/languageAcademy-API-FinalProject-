using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations
{
    public class TeacherApplicationConfiguration : IEntityTypeConfiguration<TeacherApplication>
    {
        public void Configure(EntityTypeBuilder<TeacherApplication> builder)
        {
            builder.Property(m => m.Name).IsRequired().HasMaxLength(80);
            builder.Property(m => m.Surname).IsRequired().HasMaxLength(80);
            builder.Property(m => m.Email).IsRequired().HasMaxLength(254);
            builder.Property(m => m.Phone).IsRequired().HasMaxLength(40);
            builder.Property(m => m.Country).IsRequired().HasMaxLength(80);
            builder.Property(m => m.Education).IsRequired().HasMaxLength(160);
            builder.Property(m => m.Institution).IsRequired().HasMaxLength(160);
            builder.Property(m => m.Experience).IsRequired().HasMaxLength(160);
            builder.Property(m => m.Years).IsRequired();
            builder.Property(m => m.Languages).IsRequired().HasMaxLength(160);
            builder.Property(m => m.Subject).IsRequired().HasMaxLength(120);
            builder.Property(m => m.Bio).IsRequired().HasMaxLength(2000);
            builder.Property(m => m.Portfolio).HasMaxLength(400);
            builder.Property(m => m.Status).IsRequired();
            builder.Property(m => m.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.HasIndex(m => m.Email);
        }
    }
}
