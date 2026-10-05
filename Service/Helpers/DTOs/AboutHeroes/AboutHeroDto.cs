using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers.DTOs.AboutHeroes
{
    public class AboutHeroDto
    {
        public int Id { get; set; }
        public string Eyebrow { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Lead { get; set; } = string.Empty;
    }
}
