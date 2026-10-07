using System.ComponentModel.DataAnnotations;
using Domain.Entities;
using Domain.Enums;
using Repository.Repositories.Interfaces;
using Service.Helpers.DTOs.TeacherApplications;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class TeacherApplicationService : ITeacherApplicationService
    {
        private readonly ITeacherApplicationRepository _repository;

        public TeacherApplicationService(ITeacherApplicationRepository repository)
        {
            _repository = repository;
        }

        public async Task<TeacherApplicationResultDto> ApplyAsync(TeacherApplicationCreateDto dto)
        {
            var errors = Validate(dto);
            if (errors.Count > 0)
                return Fail(errors);

            var email = dto.Email.Trim().ToLowerInvariant();
            if (await _repository.HasPendingEmailAsync(email))
                return Fail(new List<string> { "An application with this email is already pending." });

            await _repository.AddAsync(new TeacherApplication
            {
                Name = dto.Name.Trim(),
                Surname = dto.Surname.Trim(),
                Email = email,
                Phone = dto.Phone.Trim(),
                Country = dto.Country.Trim(),
                Education = dto.Education.Trim(),
                Institution = dto.Institution.Trim(),
                Experience = dto.Experience.Trim(),
                Years = dto.Years,
                Languages = dto.Languages.Trim(),
                Subject = dto.Subject.Trim(),
                Bio = dto.Bio.Trim(),
                Portfolio = (dto.Portfolio ?? string.Empty).Trim(),
                Status = TeacherApplicationStatus.Pending
            });

            return new TeacherApplicationResultDto { Succeeded = true };
        }

        public async Task<IReadOnlyList<TeacherApplicationDto>> GetAllAsync()
        {
            var items = await _repository.GetAllAsync();
            return items.Select(m => new TeacherApplicationDto
            {
                Id = m.Id,
                Name = m.Name,
                Surname = m.Surname,
                Email = m.Email,
                Phone = m.Phone,
                Country = m.Country,
                Education = m.Education,
                Institution = m.Institution,
                Experience = m.Experience,
                Years = m.Years,
                Languages = m.Languages,
                Subject = m.Subject,
                Bio = m.Bio,
                Portfolio = m.Portfolio,
                Status = m.Status.ToString(),
                CreatedAt = m.CreatedAt
            }).ToList();
        }

        private static TeacherApplicationResultDto Fail(List<string> errors)
        {
            return new TeacherApplicationResultDto { Succeeded = false, Errors = errors };
        }

        private static List<string> Validate(TeacherApplicationCreateDto dto)
        {
            var errors = new List<string>();
            Required(errors, dto.Name, 80, "Name is required.");
            Required(errors, dto.Surname, 80, "Surname is required.");

            var email = (dto.Email ?? string.Empty).Trim();
            if (email.Length is 0 or > 254 || !new EmailAddressAttribute().IsValid(email))
                errors.Add("Enter a valid email.");

            Required(errors, dto.Phone, 40, "Phone is required.");
            Required(errors, dto.Country, 80, "Country is required.");
            Required(errors, dto.Education, 160, "Education is required.");
            Required(errors, dto.Institution, 160, "Institution is required.");
            Required(errors, dto.Experience, 160, "Experience is required.");
            if (dto.Years is < 0 or > 60)
                errors.Add("Years of experience must be between 0 and 60.");
            Required(errors, dto.Languages, 160, "Languages are required.");
            Required(errors, dto.Subject, 120, "Specialization is required.");
            var bio = (dto.Bio ?? string.Empty).Trim();
            if (bio.Length is 0 or > 2000)
                errors.Add("Biography is required.");
            if ((dto.Portfolio ?? string.Empty).Trim().Length > 400)
                errors.Add("Portfolio URL is too long.");

            return errors;
        }

        private static void Required(List<string> errors, string? value, int max, string message)
        {
            var text = (value ?? string.Empty).Trim();
            if (text.Length is 0 || text.Length > max)
                errors.Add(message);
        }
    }
}
