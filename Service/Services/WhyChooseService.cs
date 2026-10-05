using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.Helpers.DTOs.WhyChooses;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class WhyChooseService : IWhyChooseService
    {
        private readonly IWhyChooseRepository _whyChooseRepo;
        public WhyChooseService(IWhyChooseRepository whyChooseRepo)
        {
            _whyChooseRepo = whyChooseRepo;
        }

        public async Task<WhyChooseDto?> GetUIAsync()
        {
            var section = await _whyChooseRepo.GetWithCardsAsync();
            if (section is null) return null;

            return new WhyChooseDto
            {
                Id = section.Id,
                Eyebrow = section.Eyebrow,
                Title = section.Title,
                Lead = section.Lead,
                Cards = section.Cards.OrderBy(m => m.SortOrder).Select(m => new WhyChooseCardDto
                {
                    Id = m.Id,
                    Icon = m.Icon,
                    Title = m.Title,
                    Text = m.Text,
                    SortOrder = m.SortOrder
                }).ToList()
            };
        }
    }
}
