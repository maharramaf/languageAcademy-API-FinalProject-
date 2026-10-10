using Microsoft.Extensions.DependencyInjection;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositoryLayer(this IServiceCollection services)
        {
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseModuleRepository, CourseModuleRepository>();
            services.AddScoped<ILessonRepository, LessonRepository>();
            services.AddScoped<ICourseOutcomeRepository, CourseOutcomeRepository>();
            services.AddScoped<IHeroRepository, HeroRepository>();
            services.AddScoped<IAboutRepository, AboutRepository>();
            services.AddScoped<IWhyChooseRepository, WhyChooseRepository>();
            services.AddScoped<IWhyChooseCardRepository, WhyChooseCardRepository>();
            services.AddScoped<IHowWeWorkRepository, HowWeWorkRepository>();
            services.AddScoped<IHowWeWorkCardRepository, HowWeWorkCardRepository>();
            services.AddScoped<IAboutHeroRepository, AboutHeroRepository>();
            services.AddScoped<ITeacherSectionRepository, TeacherSectionRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<IStatsRepository, StatsRepository>();
            services.AddScoped<IReviewSectionRepository, ReviewSectionRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<INewsletterSectionRepository, NewsletterSectionRepository>();
            services.AddScoped<ISubscriberRepository, SubscriberRepository>();
            services.AddScoped<IContactSectionRepository, ContactSectionRepository>();
            services.AddScoped<IContactMessageRepository, ContactMessageRepository>();
            services.AddScoped<IStudentSectionRepository, StudentSectionRepository>();
            services.AddScoped<ITeacherApplicationRepository, TeacherApplicationRepository>();
            services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<ILessonProgressRepository, LessonProgressRepository>();
            services.AddScoped<ICertificateRepository, CertificateRepository>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            return services;
        }
    }
}
