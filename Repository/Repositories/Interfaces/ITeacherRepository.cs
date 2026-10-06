using Domain.Entities;


namespace Repository.Repositories.Interfaces
{
    public interface ITeacherRepository : IBaseRepository<Teacher>
    {
        Task<int> CountAsync();
    }
}
