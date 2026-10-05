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
    public class HowWeWorkCardConfiguration : IEntityTypeConfiguration<HowWeWorkCard>
    {
        public void Configure(EntityTypeBuilder<HowWeWorkCard> builder)
        {
            builder.Property(m => m.Icon).IsRequired().HasMaxLength(80);
            builder.Property(m => m.Title).IsRequired().HasMaxLength(80);
            builder.Property(m => m.Text).IsRequired().HasMaxLength(400);
            builder.Property(m => m.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.HasOne(m => m.HowWeWork)
                .WithMany(m => m.Cards)
                .HasForeignKey(m => m.HowWeWorkId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
