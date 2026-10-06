using System.ComponentModel.DataAnnotations;
using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.Helpers.DTOs.Newsletters;
using Service.Services.Interfaces;


namespace Service.Services
{
    public class NewsletterService : INewsletterService
    {
        private readonly INewsletterSectionRepository _sectionRepo;
        private readonly ISubscriberRepository _subscriberRepo;

        public NewsletterService(INewsletterSectionRepository sectionRepo, ISubscriberRepository subscriberRepo)
        {
            _sectionRepo = sectionRepo;
            _subscriberRepo = subscriberRepo;
        }

        public async Task<NewsletterSectionDto?> GetUIAsync()
        {
            var section = await _sectionRepo.GetLatestAsync();
            if (section is null) return null;

            return new NewsletterSectionDto
            {
                Id = section.Id,
                Subtitle = section.Subtitle,
                Title = section.Title,
                Text = section.Text
            };
        }

        public async Task<bool> SubscribeAsync(string email)
        {
            var normalized = (email ?? string.Empty).Trim().ToLowerInvariant();
            if (normalized.Length is 0 or > 254) return false;
            if (!new EmailAddressAttribute().IsValid(normalized)) return false;

            var existing = await _subscriberRepo.GetByEmailAsync(normalized);
            if (existing is null)
            {
                await _subscriberRepo.AddAsync(new Subscriber
                {
                    Email = normalized,
                    Active = true
                });
                return true;
            }

            if (!existing.Active)
            {
                existing.Active = true;
                await _subscriberRepo.SaveAsync();
            }

            return true;
        }
    }
}
