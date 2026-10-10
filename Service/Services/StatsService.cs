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
        private readonly IAccountService _accountService;

        public StatsService(
            IStatsRepository statsRepo,
            ICourseRepository courseRepo,
            ITeacherRepository teacherRepo,
            IAccountService accountService)
        {
            _statsRepo = statsRepo;
            _courseRepo = courseRepo;
            _teacherRepo = teacherRepo;
            _accountService = accountService;
        }

        public async Task<StatsDto?> GetUIAsync()
        {
            var section = await _statsRepo.GetLatestAsync();
            if (section is null) return null;

            return new StatsDto
            {
                Id = section.Id,
                Students = await _accountService.CountStudentsAsync(),
                Courses = await _courseRepo.CountAsync(),
                Teachers = await _teacherRepo.CountAsync(),
                Years = section.Years
            };
        }
    }
}
