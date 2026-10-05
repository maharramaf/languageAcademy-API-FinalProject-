using Service.Helpers.DTOs.Heroes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IHeroService
    {
        Task<HeroDto?> GetUIAsync();
    }
}
