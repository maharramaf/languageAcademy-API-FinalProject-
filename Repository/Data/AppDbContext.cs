using Domain.Configurations;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseModule> CourseModules { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<CourseOutcome> CourseOutcomes { get; set; }
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<WhyChoose> WhyChooses { get; set; }
        public DbSet<WhyChooseCard> WhyChooseCards { get; set; }
        public DbSet<HowWeWork> HowWeWorks { get; set; }
        public DbSet<HowWeWorkCard> HowWeWorkCards { get; set; }
        public DbSet<AboutHero> AboutHeroes { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(CourseConfiguration).Assembly);
        }
    }
}
