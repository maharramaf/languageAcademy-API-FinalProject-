using Service.Helpers.DTOs.WhyChooses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IWhyChooseService
    {
        Task<WhyChooseDto?> GetUIAsync();
    }
}
