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
    public class AboutHeroConfiguration : IEntityTypeConfiguration<AboutHero>
    {
        public void Configure(EntityTypeBuilder<AboutHero> builder)
        {
            builder.Property(m => m.Subtitle).IsRequired().HasMaxLength(80);
            builder.Property(m => m.Title).IsRequired().HasMaxLength(160);
            builder.Property(m => m.Text).IsRequired().HasMaxLength(500);
            builder.Property(m => m.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
