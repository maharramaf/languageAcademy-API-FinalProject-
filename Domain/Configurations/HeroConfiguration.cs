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
    public class HeroConfiguration : IEntityTypeConfiguration<Hero>
    {
        public void Configure(EntityTypeBuilder<Hero> builder)
        {
            builder.Property(m => m.Subtitle).IsRequired().HasMaxLength(80);
            builder.Property(m => m.Title).IsRequired().HasMaxLength(160);
            builder.Property(m => m.Text).IsRequired().HasMaxLength(500);
            builder.Property(m => m.Button1).IsRequired().HasMaxLength(80);
            builder.Property(m => m.Button2).IsRequired().HasMaxLength(80);
            builder.Property(m => m.Point1).IsRequired().HasMaxLength(80);
            builder.Property(m => m.Point2).IsRequired().HasMaxLength(80);
            builder.Property(m => m.Point3).IsRequired().HasMaxLength(80);
            builder.Property(m => m.Image).IsRequired().HasMaxLength(260);
            builder.Property(m => m.ImageAlt).IsRequired().HasMaxLength(200);
            builder.Property(m => m.Students).IsRequired().HasMaxLength(40);
            builder.Property(m => m.StudentsText).IsRequired().HasMaxLength(80);
            builder.Property(m => m.Rating).IsRequired().HasMaxLength(40);
            builder.Property(m => m.RatingText).IsRequired().HasMaxLength(80);
            builder.Property(m => m.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
