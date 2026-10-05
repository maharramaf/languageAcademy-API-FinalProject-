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
            builder.Property(m => m.Eyebrow).IsRequired().HasMaxLength(80);
            builder.Property(m => m.Title).IsRequired().HasMaxLength(160);
            builder.Property(m => m.Lead).IsRequired().HasMaxLength(500);
            builder.Property(m => m.PrimaryButtonText).IsRequired().HasMaxLength(80);
            builder.Property(m => m.SecondaryButtonText).IsRequired().HasMaxLength(80);
            builder.Property(m => m.PointOne).IsRequired().HasMaxLength(80);
            builder.Property(m => m.PointTwo).IsRequired().HasMaxLength(80);
            builder.Property(m => m.PointThree).IsRequired().HasMaxLength(80);
            builder.Property(m => m.Image).IsRequired().HasMaxLength(260);
            builder.Property(m => m.ImageAlt).IsRequired().HasMaxLength(200);
            builder.Property(m => m.StudentsStat).IsRequired().HasMaxLength(40);
            builder.Property(m => m.StudentsLabel).IsRequired().HasMaxLength(80);
            builder.Property(m => m.RatingStat).IsRequired().HasMaxLength(40);
            builder.Property(m => m.RatingLabel).IsRequired().HasMaxLength(80);
            builder.Property(m => m.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
