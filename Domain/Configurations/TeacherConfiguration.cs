using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Domain.Configurations
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.Property(m => m.Name).IsRequired().HasMaxLength(120);
            builder.Property(m => m.Role).IsRequired().HasMaxLength(120);
            builder.Property(m => m.Summary).IsRequired().HasMaxLength(400);
            builder.Property(m => m.Bio).IsRequired().HasMaxLength(1000);
            builder.Property(m => m.Photo).IsRequired().HasMaxLength(260);
            builder.Property(m => m.PhotoAlt).IsRequired().HasMaxLength(200);
            builder.Property(m => m.LinkedIn).IsRequired().HasMaxLength(260);
            builder.Property(m => m.SocialIcon).IsRequired().HasMaxLength(80);
            builder.Property(m => m.Social).IsRequired().HasMaxLength(260);
            builder.Property(m => m.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.HasOne(m => m.TeacherSection)
                .WithMany(m => m.Teachers)
                .HasForeignKey(m => m.TeacherSectionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
