using Service.Helpers.DTOs.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDto>> GetAllUIAsync();
        Task<CourseDetailDto?> GetUIAsync(string slug);
        Task<CourseDetailDto?> GetByIdAsync(int id);
        Task<CourseCreateResultDto> CreateAsync(CourseCreateDto dto);
        Task<CourseCreateResultDto> UpdateAsync(int id, CourseUpdateDto dto);
    }
}
