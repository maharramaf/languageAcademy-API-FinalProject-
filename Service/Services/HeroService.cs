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
                Eyebrow = hero.Eyebrow,
                Title = hero.Title,
                Lead = hero.Lead,
                PrimaryButtonText = hero.PrimaryButtonText,
                SecondaryButtonText = hero.SecondaryButtonText,
                PointOne = hero.PointOne,
                PointTwo = hero.PointTwo,
                PointThree = hero.PointThree,
                Image = hero.Image,
                ImageAlt = hero.ImageAlt,
                StudentsStat = hero.StudentsStat,
                StudentsLabel = hero.StudentsLabel,
                RatingStat = hero.RatingStat,
                RatingLabel = hero.RatingLabel
            };
        }
    }
}
