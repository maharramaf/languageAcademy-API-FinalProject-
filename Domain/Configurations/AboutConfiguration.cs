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
    public class AboutConfiguration : IEntityTypeConfiguration<About>
    {
        public void Configure(EntityTypeBuilder<About> builder)
        {
            builder.Property(m => m.Subtitle).IsRequired().HasMaxLength(80);
            builder.Property(m => m.Title).IsRequired().HasMaxLength(160);
            builder.Property(m => m.Text1).IsRequired().HasMaxLength(1000);
            builder.Property(m => m.Text2).IsRequired().HasMaxLength(1000);
            builder.Property(m => m.Feature1).IsRequired().HasMaxLength(80);
            builder.Property(m => m.Feature2).IsRequired().HasMaxLength(80);
            builder.Property(m => m.Feature3).IsRequired().HasMaxLength(80);
            builder.Property(m => m.Feature4).IsRequired().HasMaxLength(80);
            builder.Property(m => m.Button).IsRequired().HasMaxLength(80);
            builder.Property(m => m.Image).IsRequired().HasMaxLength(260);
            builder.Property(m => m.ImageAlt).IsRequired().HasMaxLength(200);
            builder.Property(m => m.Years).IsRequired().HasMaxLength(40);
            builder.Property(m => m.YearsText1).IsRequired().HasMaxLength(80);
            builder.Property(m => m.YearsText2).IsRequired().HasMaxLength(80);
            builder.Property(m => m.PageKey).IsRequired().HasMaxLength(40).HasDefaultValue("home");
            builder.HasIndex(m => m.PageKey).IsUnique();
            builder.Property(m => m.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
