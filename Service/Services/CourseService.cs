using System.Text;
using Domain.Entities;
using Domain.Enums;
using Repository.Repositories.Interfaces;
using Service.Helpers.DTOs.Courses;
using Service.Helpers.DTOs.Reviews;
using Service.Services.Interfaces;

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
                Video = m.Video
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
                Video = course.Video,
                Outcomes = course.Outcomes.OrderBy(m => m.Order).Select(m => m.Text).ToList(),
                Reviews = course.Reviews.Where(m => m.Approved).OrderBy(m => m.Order).ThenByDescending(m => m.CreatedAt).Select(m => new ReviewDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    Text = m.Text,
                    Rating = m.Rating,
                    Photo = m.Photo,
                    PhotoAlt = m.PhotoAlt,
                    Result = m.Result,
                    Course = course.Title,
                    CourseSlug = course.Slug
                }).ToList(),
                Modules = course.Modules.OrderBy(m => m.Order).Select(m => new CourseModuleDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    Info = m.Info,
                    Order = m.Order,
                    Lessons = m.Lessons.OrderBy(x => x.Order).Select(x => new LessonDto
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Kind = x.Kind.ToString().ToLowerInvariant(),
                        Video = x.Video,
                        Seconds = x.Seconds,
                        Order = x.Order
                    }).ToList()
                }).ToList()
            };
        }

        public async Task<CourseDetailDto?> GetByIdAsync(int id)
        {
            var course = await _courseRepo.GetByIdAsync(id);
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
                Video = course.Video
            };
        }

        public async Task<CourseCreateResultDto> CreateAsync(CourseCreateDto dto)
        {
            var errors = Validate(dto);
            if (errors.Count > 0)
                return Fail(errors);

            if (!Enum.TryParse<CourseType>(dto.Type.Trim(), true, out var type)
                || !Enum.IsDefined(type))
                return Fail("Course type must be Demo, Standard, or Premium.");

            var slug = await UniqueSlugAsync(Slugify(dto.Title));
            await _courseRepo.AddAsync(new Course
            {
                Slug = slug,
                Title = dto.Title.Trim(),
                Type = type,
                Level = dto.Level.Trim(),
                Duration = dto.Duration.Trim(),
                Price = dto.Price,
                Image = (dto.Image ?? string.Empty).Trim(),
                Summary = dto.Summary.Trim(),
                Overview = dto.Overview.Trim(),
                Video = string.IsNullOrWhiteSpace(dto.Video) ? null : dto.Video.Trim()
            });

            return new CourseCreateResultDto
            {
                Succeeded = true,
                Slug = slug,
                Title = dto.Title.Trim()
            };
        }

        public async Task<CourseCreateResultDto> UpdateAsync(int id, CourseUpdateDto dto)
        {
            var errors = Validate(dto.Title, dto.Type, dto.Level, dto.Duration, dto.Price, dto.Image, dto.Summary, dto.Overview);
            if (errors.Count > 0)
                return Fail(errors);

            if (!Enum.TryParse<CourseType>(dto.Type.Trim(), true, out var type)
                || !Enum.IsDefined(type))
                return Fail("Course type must be Demo, Standard, or Premium.");

            var course = await _courseRepo.GetByIdAsync(id);
            if (course is null)
                return Fail("Course was not found.");

            var title = dto.Title.Trim();
            if (!string.Equals(course.Title, title, StringComparison.Ordinal))
                course.Slug = await UniqueSlugAsync(Slugify(title), course.Id);
            course.Title = title;
            course.Type = type;
            course.Level = dto.Level.Trim();
            course.Duration = dto.Duration.Trim();
            course.Price = dto.Price;
            if (!string.IsNullOrWhiteSpace(dto.Image))
                course.Image = dto.Image.Trim();
            course.Summary = dto.Summary.Trim();
            course.Overview = dto.Overview.Trim();

            await _courseRepo.SaveAsync();

            return new CourseCreateResultDto
            {
                Succeeded = true,
                Slug = course.Slug,
                Title = course.Title
            };
        }

        private static List<string> Validate(CourseCreateDto dto)
        {
            return Validate(dto.Title, dto.Type, dto.Level, dto.Duration, dto.Price, dto.Image, dto.Summary, dto.Overview);
        }

        private static List<string> Validate(
            string? title,
            string? type,
            string? level,
            string? duration,
            decimal price,
            string? image,
            string? summary,
            string? overview)
        {
            var errors = new List<string>();
            title = (title ?? string.Empty).Trim();
            type = (type ?? string.Empty).Trim();
            level = (level ?? string.Empty).Trim();
            duration = (duration ?? string.Empty).Trim();
            image = (image ?? string.Empty).Trim();
            summary = (summary ?? string.Empty).Trim();
            overview = (overview ?? string.Empty).Trim();

            if (title.Length is 0 or > 160)
                errors.Add("Title is required.");
            if (type.Length == 0)
                errors.Add("Course type is required.");
            if (level.Length is 0 or > 60)
                errors.Add("Level is required.");
            if (duration.Length is 0 or > 40)
                errors.Add("Duration is required.");
            if (price < 0)
                errors.Add("Price cannot be negative.");
            if (image.Length > 260)
                errors.Add("Image path is too long.");
            if (summary.Length is 0 or > 500)
                errors.Add("Summary is required.");
            if (overview.Length is 0 or > 2000)
                errors.Add("Overview is required.");

            return errors;
        }

        private async Task<string> UniqueSlugAsync(string slug, int? exceptId = null)
        {
            if (!await _courseRepo.SlugExistsAsync(slug, exceptId))
                return slug;

            for (var i = 2; i < 1000; i++)
            {
                var candidate = slug.Length + i.ToString().Length + 1 > 80
                    ? slug[..Math.Max(1, 80 - i.ToString().Length - 1)] + "-" + i
                    : slug + "-" + i;
                if (!await _courseRepo.SlugExistsAsync(candidate, exceptId))
                    return candidate;
            }

            return slug + "-" + Guid.NewGuid().ToString("N")[..8];
        }

        private static string Slugify(string title)
        {
            var source = title.Trim().ToLowerInvariant();
            var builder = new StringBuilder();
            var dash = false;

            foreach (var c in source)
            {
                if (char.IsLetterOrDigit(c))
                {
                    builder.Append(c);
                    dash = false;
                }
                else if (builder.Length > 0 && !dash)
                {
                    builder.Append('-');
                    dash = true;
                }
            }

            var slug = builder.ToString().Trim('-');
            if (slug.Length > 80)
                slug = slug[..80].Trim('-');

            return string.IsNullOrEmpty(slug) ? "course" : slug;
        }

        private static CourseCreateResultDto Fail(string error) => Fail(new[] { error });

        private static CourseCreateResultDto Fail(IEnumerable<string> errors)
        {
            return new CourseCreateResultDto
            {
                Succeeded = false,
                Errors = errors.ToList()
            };
        }
    }
}
