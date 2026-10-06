using Domain.Entities;


namespace Repository.Repositories.Interfaces
{
    public interface ITeacherSectionRepository : IBaseRepository<TeacherSection>
    {
        Task<TeacherSection?> GetWithTeachersAsync();
    }
}
