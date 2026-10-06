using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Configurations
{
    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.Property(m => m.Title).IsRequired().HasMaxLength(160);
            builder.Property(m => m.Video).HasMaxLength(500);
            builder.Property(m => m.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.HasOne(m => m.CourseModule)
                .WithMany(m => m.Lessons)
                .HasForeignKey(m => m.CourseModuleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
