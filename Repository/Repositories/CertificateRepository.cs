using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class CertificateRepository : ICertificateRepository
    {
        private readonly AppDbContext _db;

        public CertificateRepository(AppDbContext db)
        {
            _db = db;
        }

        public Task<bool> ExistsAsync(string studentId, int courseId)
        {
            return _db.Certificates.AnyAsync(m => m.StudentId == studentId && m.CourseId == courseId);
        }

        public Task<Certificate?> GetByStudentAndCourseAsync(string studentId, int courseId)
        {
            return _db.Certificates.AsNoTracking()
                .Include(m => m.Student)
                .Include(m => m.Course)
                    .ThenInclude(m => m.Teacher)
                .FirstOrDefaultAsync(m => m.StudentId == studentId && m.CourseId == courseId);
        }

        public Task<Certificate?> GetByStudentAndSlugAsync(string studentId, string slug)
        {
            return _db.Certificates.AsNoTracking()
                .Include(m => m.Student)
                .Include(m => m.Course)
                    .ThenInclude(m => m.Teacher)
                .FirstOrDefaultAsync(m => m.StudentId == studentId && m.Course.Slug == slug);
        }

        public async Task<IReadOnlyList<Certificate>> GetByStudentAsync(string studentId)
        {
            return await _db.Certificates.AsNoTracking()
                .Include(m => m.Student)
                .Include(m => m.Course)
                    .ThenInclude(m => m.Teacher)
                .Where(m => m.StudentId == studentId)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();
        }

        public async Task AddAsync(Certificate certificate)
        {
            await _db.Certificates.AddAsync(certificate);
            await _db.SaveChangesAsync();
        }
    }
}
