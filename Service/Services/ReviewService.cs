using Repository.Repositories.Interfaces;
using Service.Helpers.DTOs.Reviews;
using Service.Services.Interfaces;


namespace Service.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewSectionRepository _sectionRepo;
        private readonly IReviewRepository _reviewRepo;

        public ReviewService(IReviewSectionRepository sectionRepo, IReviewRepository reviewRepo)
        {
            _sectionRepo = sectionRepo;
            _reviewRepo = reviewRepo;
        }

        public async Task<ReviewSectionDto?> GetUIAsync()
        {
            var section = await _sectionRepo.GetLatestAsync();
            if (section is null) return null;

            var reviews = await _reviewRepo.GetHomeAsync();

            return new ReviewSectionDto
            {
                Id = section.Id,
                Subtitle = section.Subtitle,
                Title = section.Title,
                Text = section.Text,
                Reviews = reviews.Select(m => new ReviewDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    Text = m.Text,
                    Rating = m.Rating,
                    Photo = m.Photo,
                    PhotoAlt = m.PhotoAlt,
                    Result = m.Result,
                    Course = m.Course.Title,
                    CourseSlug = m.Course.Slug
                }).ToList()
            };
        }
    }
}
