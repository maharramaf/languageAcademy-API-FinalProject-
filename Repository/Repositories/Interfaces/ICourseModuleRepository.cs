using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface ICourseModuleRepository : IBaseRepository<CourseModule>
    {
        Task<CourseModule?> GetByIdAsync(int id);
        Task<int> NextOrderAsync(int courseId);
        Task AddAsync(CourseModule module);
    }
}
