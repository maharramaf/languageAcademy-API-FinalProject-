using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.Helpers.DTOs.Courses;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepo;
        public CourseService(ICourseRepository courseRepo)
        {
            _courseRepo = courseRepo;
        }

        public async Task<IEnumerable<CourseDto>> GetAllUIAsync()
        {
            var result = await _courseRepo.GetAllWithModulesAsync();
            return result.OrderByDescending(m => m.CreatedAt).Select(m => new CourseDto
            {
                Id = m.Id,
                Slug = m.Slug,
                Title = m.Title,
                Type = m.Type.ToString().ToLowerInvariant(),
                Level = m.Level,
                Duration = m.Duration,
                Price = m.Price,
                Image = m.Image,
                Summary = m.Summary,
                LessonCount = m.Modules.Sum(x => x.Lessons.Count),
                PreviewVideoUrl = m.PreviewVideoUrl
            });
        }

        public async Task<CourseDetailDto?> GetUIAsync(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug)) return null;

            var course = await _courseRepo.GetBySlugAsync(slug.Trim());
            if (course is null) return null;

            return new CourseDetailDto
            {
                Id = course.Id,
                Slug = course.Slug,
                Title = course.Title,
                Type = course.Type.ToString().ToLowerInvariant(),
                Level = course.Level,
                Duration = course.Duration,
                Price = course.Price,
                Image = course.Image,
                Summary = course.Summary,
                Overview = course.Overview,
                PreviewVideoUrl = course.PreviewVideoUrl,
                Outcomes = course.Outcomes.OrderBy(m => m.SortOrder).Select(m => m.Text).ToList(),
                Modules = course.Modules.OrderBy(m => m.SortOrder).Select(m => new CourseModuleDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    Meta = m.Meta,
                    SortOrder = m.SortOrder,
                    Lessons = m.Lessons.OrderBy(x => x.SortOrder).Select(x => new LessonDto
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Kind = x.Kind.ToString().ToLowerInvariant(),
                        VideoUrl = x.VideoUrl,
                        DurationSeconds = x.DurationSeconds,
                        SortOrder = x.SortOrder
                    }).ToList()
                }).ToList()
            };
        }
    }
}
