using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Domain.Configurations
{
    public class ContactSectionConfiguration : IEntityTypeConfiguration<ContactSection>
    {
        public void Configure(EntityTypeBuilder<ContactSection> builder)
        {
            builder.Property(m => m.Subtitle).IsRequired().HasMaxLength(80);
            builder.Property(m => m.Title).IsRequired().HasMaxLength(160);
            builder.Property(m => m.Text).IsRequired().HasMaxLength(500);
            builder.Property(m => m.Address).IsRequired().HasMaxLength(160);
            builder.Property(m => m.City).IsRequired().HasMaxLength(120);
            builder.Property(m => m.Phone).IsRequired().HasMaxLength(40);
            builder.Property(m => m.Email).IsRequired().HasMaxLength(254);
            builder.Property(m => m.Hours).IsRequired().HasMaxLength(80);
            builder.Property(m => m.Saturday).IsRequired().HasMaxLength(80);
            builder.Property(m => m.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
