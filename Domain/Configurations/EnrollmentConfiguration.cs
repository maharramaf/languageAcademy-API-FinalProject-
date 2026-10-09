using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations
{
    public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.Property(m => m.StudentId).IsRequired().HasMaxLength(450);
            builder.Property(m => m.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.HasIndex(m => new { m.StudentId, m.CourseId }).IsUnique();
            builder.HasOne(m => m.Student)
                .WithMany()
                .HasForeignKey(m => m.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(m => m.Course)
                .WithMany(m => m.Enrollments)
                .HasForeignKey(m => m.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
