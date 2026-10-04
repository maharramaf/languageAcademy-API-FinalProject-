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
    public class CourseOutcomeConfiguration : IEntityTypeConfiguration<CourseOutcome>
    {
        public void Configure(EntityTypeBuilder<CourseOutcome> builder)
        {
            builder.Property(m => m.Text).IsRequired().HasMaxLength(260);
            builder.Property(m => m.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.HasOne(m => m.Course)
                .WithMany(m => m.Outcomes)
                .HasForeignKey(m => m.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
