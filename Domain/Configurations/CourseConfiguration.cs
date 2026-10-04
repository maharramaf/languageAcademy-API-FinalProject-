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
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(m => m.Slug).IsRequired().HasMaxLength(80);
            builder.HasIndex(m => m.Slug).IsUnique();
            builder.Property(m => m.Title).IsRequired().HasMaxLength(160);
            builder.Property(m => m.Level).IsRequired().HasMaxLength(60);
            builder.Property(m => m.Duration).IsRequired().HasMaxLength(40);
            builder.Property(m => m.Price).HasColumnType("decimal(10,2)");
            builder.Property(m => m.Image).HasMaxLength(260);
            builder.Property(m => m.Summary).HasMaxLength(500);
            builder.Property(m => m.Overview).HasMaxLength(2000);
            builder.Property(m => m.PreviewVideoUrl).HasMaxLength(500);
            builder.Property(m => m.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
