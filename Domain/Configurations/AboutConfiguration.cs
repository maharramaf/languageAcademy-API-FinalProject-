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
            builder.Property(m => m.Eyebrow).IsRequired().HasMaxLength(80);
            builder.Property(m => m.Title).IsRequired().HasMaxLength(160);
            builder.Property(m => m.ParagraphOne).IsRequired().HasMaxLength(1000);
            builder.Property(m => m.ParagraphTwo).IsRequired().HasMaxLength(1000);
            builder.Property(m => m.FeatureOne).IsRequired().HasMaxLength(80);
            builder.Property(m => m.FeatureTwo).IsRequired().HasMaxLength(80);
            builder.Property(m => m.FeatureThree).IsRequired().HasMaxLength(80);
            builder.Property(m => m.FeatureFour).IsRequired().HasMaxLength(80);
            builder.Property(m => m.ButtonText).IsRequired().HasMaxLength(80);
            builder.Property(m => m.Image).IsRequired().HasMaxLength(260);
            builder.Property(m => m.ImageAlt).IsRequired().HasMaxLength(200);
            builder.Property(m => m.YearsStat).IsRequired().HasMaxLength(40);
            builder.Property(m => m.YearsLabelLineOne).IsRequired().HasMaxLength(80);
            builder.Property(m => m.YearsLabelLineTwo).IsRequired().HasMaxLength(80);
            builder.Property(m => m.PageKey).IsRequired().HasMaxLength(40).HasDefaultValue("home");
            builder.HasIndex(m => m.PageKey).IsUnique();
            builder.Property(m => m.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
