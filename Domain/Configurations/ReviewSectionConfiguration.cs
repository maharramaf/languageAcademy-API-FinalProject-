using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Domain.Configurations
{
    public class ReviewSectionConfiguration : IEntityTypeConfiguration<ReviewSection>
    {
        public void Configure(EntityTypeBuilder<ReviewSection> builder)
        {
            builder.Property(m => m.Subtitle).IsRequired().HasMaxLength(80);
            builder.Property(m => m.Title).IsRequired().HasMaxLength(160);
            builder.Property(m => m.Text).IsRequired().HasMaxLength(500);
            builder.Property(m => m.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
