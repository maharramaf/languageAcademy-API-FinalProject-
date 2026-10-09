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
        Task<CourseCreateResultDto> DeleteAsync(int id);
        Task<CourseDetailDto?> GetCurriculumAsync(int id);
        Task<IEnumerable<CourseDto>> GetByTeacherAsync(string teacherId);
        Task<bool> TeacherOwnsAsync(int courseId, string teacherId);
        Task<CourseCreateResultDto> CreateModuleAsync(int courseId, CourseModuleCreateDto dto);
        Task<CourseCreateResultDto> CreateLessonAsync(int courseId, int moduleId, LessonCreateDto dto);
        Task<CourseModuleDto?> GetModuleAsync(int courseId, int moduleId);
        Task<CourseCreateResultDto> UpdateModuleAsync(int courseId, int moduleId, CourseModuleCreateDto dto);
        Task<CourseCreateResultDto> DeleteModuleAsync(int courseId, int moduleId);
        Task<LessonDto?> GetLessonAsync(int courseId, int moduleId, int lessonId);
        Task<CourseCreateResultDto> UpdateLessonAsync(int courseId, int moduleId, int lessonId, LessonCreateDto dto);
        Task<CourseCreateResultDto> DeleteLessonAsync(int courseId, int moduleId, int lessonId);
    }
}
