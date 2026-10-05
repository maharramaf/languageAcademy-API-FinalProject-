using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.Helpers.DTOs.AboutHeroes;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AboutHeroService : IAboutHeroService
    {
        private readonly IAboutHeroRepository _aboutHeroRepo;
        public AboutHeroService(IAboutHeroRepository aboutHeroRepo)
        {
            _aboutHeroRepo = aboutHeroRepo;
        }

        public async Task<AboutHeroDto?> GetUIAsync()
        {
            var section = await _aboutHeroRepo.GetAsync();
            if (section is null) return null;

            return new AboutHeroDto
            {
                Id = section.Id,
                Eyebrow = section.Eyebrow,
                Title = section.Title,
                Lead = section.Lead
            };
        }
    }
}
