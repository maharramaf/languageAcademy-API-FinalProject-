using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Domain.Configurations
{
    public class StatItemConfiguration : IEntityTypeConfiguration<StatItem>
    {
        public void Configure(EntityTypeBuilder<StatItem> builder)
        {
            builder.Property(m => m.SourceKey).IsRequired().HasMaxLength(40);
            builder.Property(m => m.Icon).IsRequired().HasMaxLength(80);
            builder.Property(m => m.Label).IsRequired().HasMaxLength(80);
            builder.Property(m => m.Suffix).IsRequired().HasMaxLength(8);
            builder.Property(m => m.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.HasIndex(m => new { m.StatsId, m.SourceKey }).IsUnique();
            builder.HasOne(m => m.Stats)
                .WithMany(m => m.Items)
                .HasForeignKey(m => m.StatsId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
