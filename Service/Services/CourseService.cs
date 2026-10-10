using System.Text;
using Domain.Constants;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Repository.Repositories.Interfaces;
using Service.Helpers.DTOs.Courses;
using Service.Helpers.DTOs.Reviews;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepo;
        private readonly ICourseModuleRepository _moduleRepo;
        private readonly ILessonRepository _lessonRepo;
        private readonly UserManager<AppUser> _userManager;

        public CourseService(
            ICourseRepository courseRepo,
            ICourseModuleRepository moduleRepo,
            ILessonRepository lessonRepo,
            UserManager<AppUser> userManager)
        {
            _courseRepo = courseRepo;
            _moduleRepo = moduleRepo;
            _lessonRepo = lessonRepo;
            _userManager = userManager;
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
                Modules = MapModules(course.Modules, includeVideo: false)
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
                Video = course.Video,
                TeacherEmail = await TeacherEmailAsync(course.TeacherId)
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

            if (dto.TeacherEmail is not null)
            {
                if (string.IsNullOrWhiteSpace(dto.TeacherEmail))
                {
                    course.TeacherId = null;
                }
                else
                {
                    var teacher = await ResolveTeacherAsync(dto.TeacherEmail);
                    if (teacher is null)
                        return Fail("Teacher was not found.");

                    if (course.TeacherId != teacher.Id)
                    {
                        var blocked = await TeacherPlanBlockAsync(teacher);
                        if (blocked is not null)
                            return Fail(blocked);
                    }

                    course.TeacherId = teacher.Id;
                }
            }

            await _courseRepo.SaveAsync();

            return new CourseCreateResultDto
            {
                Succeeded = true,
                Slug = course.Slug,
                Title = course.Title
            };
        }

        public async Task<CourseCreateResultDto> DeleteAsync(int id)
        {
            if (!await _courseRepo.DeleteAsync(id))
                return Fail("Course was not found.");

            return new CourseCreateResultDto { Succeeded = true };
        }

        public async Task<IEnumerable<CourseDto>> GetByTeacherAsync(string teacherId)
        {
            if (string.IsNullOrWhiteSpace(teacherId))
                return Array.Empty<CourseDto>();

            var result = await _courseRepo.GetByTeacherIdAsync(teacherId);
            return result.Select(m => new CourseDto
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

        public async Task<bool> TeacherOwnsAsync(int courseId, string teacherId)
        {
            if (courseId <= 0 || string.IsNullOrWhiteSpace(teacherId))
                return false;

            var course = await _courseRepo.GetByIdAsync(courseId);
            return course is not null && course.TeacherId == teacherId;
        }

        public async Task<CourseDetailDto?> GetCurriculumAsync(int id)
        {
            var course = await _courseRepo.GetByIdWithLessonsAsync(id);
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
                Modules = MapModules(course.Modules)
            };
        }

        public async Task<CourseCreateResultDto> CreateModuleAsync(int courseId, CourseModuleCreateDto dto)
        {
            var errors = ValidateModule(dto.Title, dto.Info, out var title, out var info);
            if (errors.Count > 0)
                return Fail(errors);

            var course = await _courseRepo.GetByIdAsync(courseId);
            if (course is null)
                return Fail("Course was not found.");

            await _moduleRepo.AddAsync(new CourseModule
            {
                CourseId = courseId,
                Title = title,
                Info = info,
                Order = await _moduleRepo.NextOrderAsync(courseId)
            });

            return OkWrite(title);
        }

        public async Task<CourseCreateResultDto> CreateLessonAsync(int courseId, int moduleId, LessonCreateDto dto)
        {
            var errors = ValidateLesson(dto, out var title, out var kind);
            if (errors.Count > 0)
                return Fail(errors);

            var module = await FindModuleAsync(courseId, moduleId);
            if (module is null)
                return Fail(await _courseRepo.GetByIdAsync(courseId) is null
                    ? "Course was not found."
                    : "Module was not found.");

            await _lessonRepo.AddAsync(new Lesson
            {
                CourseModuleId = moduleId,
                Title = title,
                Kind = kind,
                Seconds = dto.Seconds,
                Video = NormalizeVideo(dto.Video),
                Order = await _lessonRepo.NextOrderAsync(moduleId)
            });

            return OkWrite(title);
        }

        public async Task<CourseModuleDto?> GetModuleAsync(int courseId, int moduleId)
        {
            var module = await FindModuleAsync(courseId, moduleId);
            return module is null ? null : MapModule(module);
        }

        public async Task<CourseCreateResultDto> UpdateModuleAsync(int courseId, int moduleId, CourseModuleCreateDto dto)
        {
            var errors = ValidateModule(dto.Title, dto.Info, out var title, out var info);
            if (errors.Count > 0)
                return Fail(errors);

            var module = await FindModuleAsync(courseId, moduleId);
            if (module is null)
                return Fail(await _courseRepo.GetByIdAsync(courseId) is null
                    ? "Course was not found."
                    : "Module was not found.");

            module.Title = title;
            module.Info = info;
            await _moduleRepo.SaveAsync();
            return OkWrite(title);
        }

        public async Task<CourseCreateResultDto> DeleteModuleAsync(int courseId, int moduleId)
        {
            var module = await FindModuleAsync(courseId, moduleId);
            if (module is null)
                return Fail(await _courseRepo.GetByIdAsync(courseId) is null
                    ? "Course was not found."
                    : "Module was not found.");

            await _moduleRepo.DeleteAsync(module);
            return new CourseCreateResultDto { Succeeded = true };
        }

        public async Task<LessonDto?> GetLessonAsync(int courseId, int moduleId, int lessonId)
        {
            var lesson = await FindLessonAsync(courseId, moduleId, lessonId);
            return lesson is null ? null : MapLesson(lesson);
        }

        public async Task<CourseCreateResultDto> UpdateLessonAsync(int courseId, int moduleId, int lessonId, LessonCreateDto dto)
        {
            var errors = ValidateLesson(dto, out var title, out var kind);
            if (errors.Count > 0)
                return Fail(errors);

            var lesson = await FindLessonAsync(courseId, moduleId, lessonId);
            if (lesson is null)
                return Fail("Lesson was not found.");

            lesson.Title = title;
            lesson.Kind = kind;
            lesson.Seconds = dto.Seconds;
            var video = NormalizeVideo(dto.Video);
            if (video is not null)
                lesson.Video = video;
            await _lessonRepo.SaveAsync();
            return OkWrite(title);
        }

        public async Task<CourseCreateResultDto> DeleteLessonAsync(int courseId, int moduleId, int lessonId)
        {
            var lesson = await FindLessonAsync(courseId, moduleId, lessonId);
            if (lesson is null)
                return Fail("Lesson was not found.");

            await _lessonRepo.DeleteAsync(lesson);
            return new CourseCreateResultDto { Succeeded = true };
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

        private async Task<CourseModule?> FindModuleAsync(int courseId, int moduleId)
        {
            var module = await _moduleRepo.GetByIdAsync(moduleId);
            return module is null || module.CourseId != courseId ? null : module;
        }

        private async Task<Lesson?> FindLessonAsync(int courseId, int moduleId, int lessonId)
        {
            var module = await FindModuleAsync(courseId, moduleId);
            if (module is null) return null;

            var lesson = await _lessonRepo.GetByIdAsync(lessonId);
            return lesson is null || lesson.CourseModuleId != moduleId ? null : lesson;
        }

        private static List<string> ValidateModule(string? title, string? info, out string trimmedTitle, out string trimmedInfo)
        {
            trimmedTitle = (title ?? string.Empty).Trim();
            trimmedInfo = (info ?? string.Empty).Trim();
            var errors = new List<string>();
            if (trimmedTitle.Length is 0 or > 160)
                errors.Add("Module title is required.");
            if (trimmedInfo.Length is 0 or > 80)
                errors.Add("Module info is required.");
            return errors;
        }

        private static List<string> ValidateLesson(LessonCreateDto dto, out string title, out LessonKind kind)
        {
            title = (dto.Title ?? string.Empty).Trim();
            var kindText = (dto.Kind ?? string.Empty).Trim();
            kind = default;
            var errors = new List<string>();
            if (title.Length is 0 or > 160)
                errors.Add("Lesson title is required.");
            if (dto.Seconds < 0)
                errors.Add("Duration cannot be negative.");
            if (kindText.Length == 0
                || !Enum.TryParse(kindText, true, out kind)
                || !Enum.IsDefined(kind))
                errors.Add("Lesson type must be Video, Text, Quiz, Homework, or Download.");
            var video = (dto.Video ?? string.Empty).Trim();
            if (video.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                errors.Add("Upload a video file. Do not paste a URL.");
            if (video.Length > 500)
                errors.Add("Video path is too long.");
            return errors;
        }

        private static string? NormalizeVideo(string? video)
        {
            video = (video ?? string.Empty).Trim().Replace('\\', '/');
            return video.Length == 0 || video.StartsWith("http", StringComparison.OrdinalIgnoreCase)
                ? null
                : video;
        }

        private static CourseCreateResultDto OkWrite(string title)
        {
            return new CourseCreateResultDto
            {
                Succeeded = true,
                Title = title
            };
        }

        private static List<CourseModuleDto> MapModules(IEnumerable<CourseModule> modules, bool includeVideo = true)
        {
            return modules.OrderBy(m => m.Order).Select(m => MapModule(m, includeVideo)).ToList();
        }

        private static CourseModuleDto MapModule(CourseModule module, bool includeVideo = true)
        {
            return new CourseModuleDto
            {
                Id = module.Id,
                Title = module.Title,
                Info = module.Info,
                Order = module.Order,
                Lessons = module.Lessons.OrderBy(x => x.Order).Select(m => MapLesson(m, includeVideo)).ToList()
            };
        }

        private static LessonDto MapLesson(Lesson lesson, bool includeVideo = true)
        {
            return new LessonDto
            {
                Id = lesson.Id,
                Title = lesson.Title,
                Kind = lesson.Kind.ToString().ToLowerInvariant(),
                Video = includeVideo ? lesson.Video : null,
                Seconds = lesson.Seconds,
                Order = lesson.Order
            };
        }

        private async Task<string?> TeacherEmailAsync(string? teacherId)
        {
            if (string.IsNullOrWhiteSpace(teacherId))
                return null;

            var user = await _userManager.FindByIdAsync(teacherId);
            return user?.Email;
        }

        private async Task<string?> TeacherPlanBlockAsync(AppUser teacher)
        {
            var limit = teacher.TeacherPlan switch
            {
                CourseType.Demo => 1,
                CourseType.Standard => 5,
                CourseType.Premium => (int?)null,
                _ => 1
            };
            if (limit is null)
                return null;

            var count = await _courseRepo.CountByTeacherIdAsync(teacher.Id);
            if (count < limit)
                return null;

            var label = teacher.TeacherPlan == CourseType.Demo ? "Free" : teacher.TeacherPlan.ToString();
            return $"This teacher's {label} plan allows {limit} course(s). Choose a higher teacher plan first.";
        }

        private async Task<AppUser?> ResolveTeacherAsync(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            var user = await _userManager.FindByEmailAsync(email.Trim());
            if (user is null)
                return null;

            return await _userManager.IsInRoleAsync(user, Roles.Teacher) ? user : null;
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
