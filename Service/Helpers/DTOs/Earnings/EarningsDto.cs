namespace Service.Helpers.DTOs.Earnings
{
    public class EarningsDto
    {
        public decimal TotalSales { get; set; }
        public decimal TeacherShare { get; set; }
        public decimal PlatformShare { get; set; }
        public int CoursesSold { get; set; }
        public decimal ThisMonth { get; set; }
        public int CommissionPercent { get; set; }
    }
}
