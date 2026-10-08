using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface ICourseRepository : IBaseRepository<Course>
    {
        Task<IEnumerable<Course>> GetAllWithModulesAsync();
        Task<Course?> GetBySlugAsync(string slug);
        Task<int> CountAsync();
        Task<bool> SlugExistsAsync(string slug, int? exceptId = null);
        Task AddAsync(Course course);
        Task<Course?> GetByIdAsync(int id);
        Task SaveAsync();
    }
}
