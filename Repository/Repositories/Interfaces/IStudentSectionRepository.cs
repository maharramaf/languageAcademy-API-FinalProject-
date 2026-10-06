using Domain.Entities;


namespace Repository.Repositories.Interfaces
{
    public interface IStudentSectionRepository : IBaseRepository<StudentSection>
    {
        Task<StudentSection?> GetLatestAsync();
    }
}
