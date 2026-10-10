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
            builder.Property(m => m.TeacherPlan).HasDefaultValue(CourseType.Demo);
            builder.Property(m => m.RewardXp).HasDefaultValue(0);
            builder.Property(m => m.RewardPoints).HasDefaultValue(0);
            builder.Property(m => m.RewardLessons).HasDefaultValue(0);
            builder.Property(m => m.RewardHomework).HasDefaultValue(0);
            builder.Property(m => m.RewardQuizzes).HasDefaultValue(0);
            builder.Property(m => m.RewardStreakDays).HasDefaultValue(0);
            builder.Property(m => m.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
