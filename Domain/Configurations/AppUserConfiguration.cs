using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Domain.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(m => m.Name).IsRequired().HasMaxLength(80);
            builder.Property(m => m.Surname).IsRequired().HasMaxLength(80);
            builder.Property(m => m.Plan).HasDefaultValue(CourseType.Demo);
            builder.Property(m => m.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
