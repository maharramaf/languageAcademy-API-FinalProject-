using System.ComponentModel.DataAnnotations;
using Domain.Common;
using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.Helpers.DTOs.Contacts;
using Service.Services.Interfaces;


namespace Service.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactSectionRepository _sectionRepo;
        private readonly IContactMessageRepository _messageRepo;

        public ContactService(IContactSectionRepository sectionRepo, IContactMessageRepository messageRepo)
        {
            _sectionRepo = sectionRepo;
            _messageRepo = messageRepo;
        }

        public async Task<ContactSectionDto?> GetUIAsync()
        {
            var section = await _sectionRepo.GetLatestAsync();
            if (section is null) return null;

            return new ContactSectionDto
            {
                Id = section.Id,
                Subtitle = section.Subtitle,
                Title = section.Title,
                Text = section.Text,
                Address = section.Address,
                City = section.City,
                Phone = section.Phone,
                Email = section.Email,
                Hours = section.Hours,
                Saturday = section.Saturday,
                Map = MapEmbed.Normalize(section.Map)
            };
        }

        public async Task<bool> SendAsync(ContactSendDto dto)
        {
            var name = (dto.Name ?? string.Empty).Trim();
            var email = (dto.Email ?? string.Empty).Trim().ToLowerInvariant();
            var phone = (dto.Phone ?? string.Empty).Trim();
            var subject = (dto.Subject ?? string.Empty).Trim();
            var text = (dto.Text ?? string.Empty).Trim();

            if (name.Length is 0 or > 120) return false;
            if (email.Length is 0 or > 254) return false;
            if (!new EmailAddressAttribute().IsValid(email)) return false;
            if (phone.Length is 0 or > 40) return false;
            if (subject.Length is 0 or > 160) return false;
            if (text.Length < 10 || text.Length > 2000) return false;

            await _messageRepo.AddAsync(new ContactMessage
            {
                Name = name,
                Email = email,
                Phone = phone,
                Subject = subject,
                Text = text
            });

            return true;
        }
    }
}
