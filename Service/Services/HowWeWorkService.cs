using Repository.Repositories.Interfaces;
using Service.Helpers.DTOs.HowWeWorks;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class HowWeWorkService : IHowWeWorkService
    {
        private readonly IHowWeWorkRepository _howWeWorkRepo;
        public HowWeWorkService(IHowWeWorkRepository howWeWorkRepo)
        {
            _howWeWorkRepo = howWeWorkRepo;
        }

        public async Task<HowWeWorkDto?> GetUIAsync()
        {
            var section = await _howWeWorkRepo.GetWithCardsAsync();
            if (section is null) return null;

            return new HowWeWorkDto
            {
                Id = section.Id,
                Subtitle = section.Subtitle,
                Title = section.Title,
                Cards = section.Cards.OrderBy(m => m.Order).Select(m => new HowWeWorkCardDto
                {
                    Id = m.Id,
                    Icon = m.Icon,
                    Title = m.Title,
                    Text = m.Text,
                    Order = m.Order
                }).ToList()
            };
        }
    }
}
