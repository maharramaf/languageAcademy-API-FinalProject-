using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Domain.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.Property(m => m.Name).IsRequired().HasMaxLength(120);
            builder.Property(m => m.Text).IsRequired().HasMaxLength(1000);
            builder.Property(m => m.Rating).IsRequired();
            builder.ToTable(t => t.HasCheckConstraint("CK_Reviews_Rating", "[Rating] >= 1 AND [Rating] <= 5"));
            builder.Property(m => m.Photo).IsRequired().HasMaxLength(260);
            builder.Property(m => m.PhotoAlt).IsRequired().HasMaxLength(200);
            builder.Property(m => m.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.HasIndex(m => new { m.Approved, m.Home, m.Order });
            builder.HasOne(m => m.Course)
                .WithMany(m => m.Reviews)
                .HasForeignKey(m => m.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
