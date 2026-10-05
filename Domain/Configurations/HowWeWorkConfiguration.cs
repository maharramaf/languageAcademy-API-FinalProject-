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
    public class HowWeWorkConfiguration : IEntityTypeConfiguration<HowWeWork>
    {
        public void Configure(EntityTypeBuilder<HowWeWork> builder)
        {
            builder.Property(m => m.Eyebrow).IsRequired().HasMaxLength(80);
            builder.Property(m => m.Title).IsRequired().HasMaxLength(160);
            builder.Property(m => m.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
