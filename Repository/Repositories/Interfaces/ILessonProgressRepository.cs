using Domain.Entities;

namespace Repository.Repositories.Interfaces
{
    public interface ILessonProgressRepository
    {
        Task<bool> ExistsAsync(string studentId, int lessonId);
        Task AddAsync(LessonProgress progress);
        Task<IReadOnlyList<int>> GetLessonIdsByStudentAsync(string studentId);
    }
}
