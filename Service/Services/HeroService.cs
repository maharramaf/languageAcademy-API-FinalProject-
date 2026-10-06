using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.Helpers.DTOs.Heroes;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class HeroService : IHeroService
    {
        private readonly IHeroRepository _heroRepo;
        public HeroService(IHeroRepository heroRepo)
        {
            _heroRepo = heroRepo;
        }

        public async Task<HeroDto?> GetUIAsync()
        {
            var hero = await _heroRepo.GetAsync();
            if (hero is null) return null;

            return new HeroDto
            {
                Id = hero.Id,
                Subtitle = hero.Subtitle,
                Title = hero.Title,
                Text = hero.Text,
                Button1 = hero.Button1,
                Button2 = hero.Button2,
                Point1 = hero.Point1,
                Point2 = hero.Point2,
                Point3 = hero.Point3,
                Image = hero.Image,
                ImageAlt = hero.ImageAlt,
                Students = hero.Students,
                StudentsText = hero.StudentsText,
                Rating = hero.Rating,
                RatingText = hero.RatingText
            };
        }
    }
}
