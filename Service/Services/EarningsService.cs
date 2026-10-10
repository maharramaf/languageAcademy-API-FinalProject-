using Repository.Repositories.Interfaces;
using Service.Helpers.DTOs.Earnings;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class EarningsService : IEarningsService
    {
        public const int CommissionPercent = 20;

        private readonly IEnrollmentRepository _enrollmentRepo;

        public EarningsService(IEnrollmentRepository enrollmentRepo)
        {
            _enrollmentRepo = enrollmentRepo;
        }

        public async Task<EarningsDto> GetMineAsync(string teacherId)
        {
            if (string.IsNullOrWhiteSpace(teacherId))
                return Empty();

            var items = await _enrollmentRepo.GetByTeacherAsync(teacherId);
            var total = items.Sum(m => m.Course.Price);
            var now = DateTime.UtcNow;
            var thisMonth = items
                .Where(m => m.CreatedAt.Year == now.Year && m.CreatedAt.Month == now.Month)
                .Sum(m => m.Course.Price);
            var platform = Math.Round(total * CommissionPercent / 100m, 2);

            return new EarningsDto
            {
                TotalSales = total,
                PlatformShare = platform,
                TeacherShare = total - platform,
                CoursesSold = items.Count,
                ThisMonth = thisMonth,
                CommissionPercent = CommissionPercent
            };
        }

        private static EarningsDto Empty()
        {
            return new EarningsDto { CommissionPercent = CommissionPercent };
        }
    }
}
