using Repository.Repositories.Interfaces;
using Service.Helpers.DTOs.Reviews;
using Service.Helpers.DTOs.Students;
using Service.Services.Interfaces;


namespace Service.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentSectionRepository _sectionRepo;
        private readonly IReviewRepository _reviewRepo;

        public StudentService(IStudentSectionRepository sectionRepo, IReviewRepository reviewRepo)
        {
            _sectionRepo = sectionRepo;
            _reviewRepo = reviewRepo;
        }

        public async Task<StudentSectionDto?> GetUIAsync()
        {
            var section = await _sectionRepo.GetLatestAsync();
            if (section is null) return null;

            var reviews = await _reviewRepo.GetApprovedAsync();

            return new StudentSectionDto
            {
                Id = section.Id,
                Subtitle = section.Subtitle,
                Title = section.Title,
                Text = section.Text,
                StoriesSubtitle = section.StoriesSubtitle,
                StoriesTitle = section.StoriesTitle,
                StoriesText = section.StoriesText,
                Reviews = reviews.Select(Map).ToList()
            };
        }

        private static ReviewDto Map(Domain.Entities.Review m)
        {
            return new ReviewDto
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
            };
        }
    }
}
