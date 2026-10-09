using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface ILessonRepository : IBaseRepository<Lesson>
    {
        Task<Lesson?> GetByIdAsync(int id);
        Task<int> NextOrderAsync(int moduleId);
        Task AddAsync(Lesson lesson);
        Task SaveAsync();
        Task DeleteAsync(Lesson lesson);
    }
}
