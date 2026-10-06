using Repository.Repositories.Interfaces;
using Service.Helpers.DTOs.Stats;
using Service.Services.Interfaces;


namespace Service.Services
{
    public class StatsService : IStatsService
    {
        private readonly IStatsRepository _statsRepo;
        private readonly ICourseRepository _courseRepo;
        private readonly ITeacherRepository _teacherRepo;

        public StatsService(
            IStatsRepository statsRepo,
            ICourseRepository courseRepo,
            ITeacherRepository teacherRepo)
        {
            _statsRepo = statsRepo;
            _courseRepo = courseRepo;
            _teacherRepo = teacherRepo;
        }

        public async Task<StatsDto?> GetUIAsync()
        {
            var section = await _statsRepo.GetWithItemsAsync();
            if (section is null) return null;

            var courseCount = await _courseRepo.CountAsync();
            var teacherCount = await _teacherRepo.CountAsync();
            var studentCount = 0;

            return new StatsDto
            {
                Id = section.Id,
                Items = section.Items.OrderBy(m => m.SortOrder).Select(m => new StatItemDto
                {
                    Id = m.Id,
                    Icon = m.Icon,
                    Value = ResolveValue(m.SourceKey, m.StoredValue, studentCount, courseCount, teacherCount),
                    Suffix = m.Suffix,
                    Label = m.Label,
                    SortOrder = m.SortOrder
                }).ToList()
            };
        }

        private static int ResolveValue(string sourceKey, int storedValue, int studentCount, int courseCount, int teacherCount)
        {
            if (string.Equals(sourceKey, "students", StringComparison.OrdinalIgnoreCase))
                return studentCount;
            if (string.Equals(sourceKey, "courses", StringComparison.OrdinalIgnoreCase))
                return courseCount;
            if (string.Equals(sourceKey, "teachers", StringComparison.OrdinalIgnoreCase))
                return teacherCount;
            return storedValue;
        }
    }
}
