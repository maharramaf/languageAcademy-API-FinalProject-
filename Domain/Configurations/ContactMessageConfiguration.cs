using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Domain.Configurations
{
    public class ContactMessageConfiguration : IEntityTypeConfiguration<ContactMessage>
    {
        public void Configure(EntityTypeBuilder<ContactMessage> builder)
        {
            builder.Property(m => m.Name).IsRequired().HasMaxLength(120);
            builder.Property(m => m.Email).IsRequired().HasMaxLength(254);
            builder.Property(m => m.Phone).IsRequired().HasMaxLength(40);
            builder.Property(m => m.Subject).IsRequired().HasMaxLength(160);
            builder.Property(m => m.Text).IsRequired().HasMaxLength(2000);
            builder.Property(m => m.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
