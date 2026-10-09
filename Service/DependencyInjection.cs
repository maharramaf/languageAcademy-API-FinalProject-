using Microsoft.Extensions.DependencyInjection;
using Service.Services;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServiceLayer(this IServiceCollection services)
        {
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IHeroService, HeroService>();
            services.AddScoped<IAboutService, AboutService>();
            services.AddScoped<IWhyChooseService, WhyChooseService>();
            services.AddScoped<IHowWeWorkService, HowWeWorkService>();
            services.AddScoped<IAboutHeroService, AboutHeroService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<IStatsService, StatsService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<INewsletterService, NewsletterService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITeacherApplicationService, TeacherApplicationService>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IPlanService, PlanService>();
            return services;
        }
    }
}
