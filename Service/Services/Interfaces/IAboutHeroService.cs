using Service.Helpers.DTOs.AboutHeroes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IAboutHeroService
    {
        Task<AboutHeroDto?> GetUIAsync();
    }
}
