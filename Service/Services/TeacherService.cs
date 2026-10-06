using Repository.Repositories.Interfaces;
using Service.Helpers.DTOs.Teachers;
using Service.Services.Interfaces;


namespace Service.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherSectionRepository _teacherSectionRepo;
        public TeacherService(ITeacherSectionRepository teacherSectionRepo)
        {
            _teacherSectionRepo = teacherSectionRepo;
        }

        public async Task<TeacherSectionDto?> GetUIAsync()
        {
            var section = await _teacherSectionRepo.GetWithTeachersAsync();
            if (section is null) return null;

            return new TeacherSectionDto
            {
                Id = section.Id,
                Subtitle = section.Subtitle,
                Title = section.Title,
                Text = section.Text,
                Teachers = section.Teachers.OrderBy(m => m.Order).Select(m => new TeacherDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    Role = m.Role,
                    Summary = m.Summary,
                    Bio = m.Bio,
                    Photo = m.Photo,
                    PhotoAlt = m.PhotoAlt,
                    LinkedIn = m.LinkedIn,
                    SocialIcon = m.SocialIcon,
                    Social = m.Social,
                    Order = m.Order
                }).ToList()
            };
        }
    }
}
