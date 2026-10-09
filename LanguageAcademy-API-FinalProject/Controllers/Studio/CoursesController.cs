using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Helpers.DTOs.Courses;
using Service.Services.Interfaces;

namespace LanguageAcademy_API_FinalProject.Controllers.Studio
{
    [Route("api/studio/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.Teacher)]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMineAsync()
        {
            var teacherId = TeacherId();
            if (teacherId is null) return Unauthorized();

            return Ok(await _courseService.GetByTeacherAsync(teacherId));
        }

        [HttpGet("{id:int}/modules")]
        public async Task<IActionResult> GetModulesAsync(int id)
        {
            if (!await OwnsAsync(id)) return NotFound();

            var course = await _courseService.GetCurriculumAsync(id);
            if (course is null) return NotFound();
            return Ok(course);
        }

        [HttpPost("{id:int}/modules")]
        public async Task<IActionResult> CreateModuleAsync(int id, [FromBody] CourseModuleCreateDto? dto)
        {
            if (dto is null) return BadRequest();
            if (!await OwnsAsync(id)) return NotFound();
            return WriteResult(await _courseService.CreateModuleAsync(id, dto));
        }

        [HttpPost("{id:int}/modules/{moduleId:int}/lessons")]
        public async Task<IActionResult> CreateLessonAsync(int id, int moduleId, [FromBody] LessonCreateDto? dto)
        {
            if (dto is null) return BadRequest();
            if (!await OwnsAsync(id)) return NotFound();
            return WriteResult(await _courseService.CreateLessonAsync(id, moduleId, dto));
        }

        [HttpGet("{id:int}/modules/{moduleId:int}")]
        public async Task<IActionResult> GetModuleAsync(int id, int moduleId)
        {
            if (!await OwnsAsync(id)) return NotFound();
            var module = await _courseService.GetModuleAsync(id, moduleId);
            if (module is null) return NotFound();
            return Ok(module);
        }

        [HttpPut("{id:int}/modules/{moduleId:int}")]
        public async Task<IActionResult> UpdateModuleAsync(int id, int moduleId, [FromBody] CourseModuleCreateDto? dto)
        {
            if (dto is null) return BadRequest();
            if (!await OwnsAsync(id)) return NotFound();
            return WriteResult(await _courseService.UpdateModuleAsync(id, moduleId, dto));
        }

        [HttpDelete("{id:int}/modules/{moduleId:int}")]
        public async Task<IActionResult> DeleteModuleAsync(int id, int moduleId)
        {
            if (!await OwnsAsync(id)) return NotFound();
            return WriteResult(await _courseService.DeleteModuleAsync(id, moduleId), emptyOk: true);
        }

        [HttpGet("{id:int}/modules/{moduleId:int}/lessons/{lessonId:int}")]
        public async Task<IActionResult> GetLessonAsync(int id, int moduleId, int lessonId)
        {
            if (!await OwnsAsync(id)) return NotFound();
            var lesson = await _courseService.GetLessonAsync(id, moduleId, lessonId);
            if (lesson is null) return NotFound();
            return Ok(lesson);
        }

        [HttpPut("{id:int}/modules/{moduleId:int}/lessons/{lessonId:int}")]
        public async Task<IActionResult> UpdateLessonAsync(int id, int moduleId, int lessonId, [FromBody] LessonCreateDto? dto)
        {
            if (dto is null) return BadRequest();
            if (!await OwnsAsync(id)) return NotFound();
            return WriteResult(await _courseService.UpdateLessonAsync(id, moduleId, lessonId, dto));
        }

        [HttpDelete("{id:int}/modules/{moduleId:int}/lessons/{lessonId:int}")]
        public async Task<IActionResult> DeleteLessonAsync(int id, int moduleId, int lessonId)
        {
            if (!await OwnsAsync(id)) return NotFound();
            return WriteResult(await _courseService.DeleteLessonAsync(id, moduleId, lessonId), emptyOk: true);
        }

        private async Task<bool> OwnsAsync(int courseId)
        {
            var teacherId = TeacherId();
            return teacherId is not null && await _courseService.TeacherOwnsAsync(courseId, teacherId);
        }

        private string? TeacherId()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            return string.IsNullOrWhiteSpace(id) ? null : id;
        }

        private IActionResult WriteResult(CourseCreateResultDto result, bool emptyOk = false)
        {
            if (!result.Succeeded)
            {
                if (result.Errors.Any(e => e.EndsWith("was not found.")))
                    return NotFound();
                return BadRequest(new { errors = result.Errors });
            }

            return emptyOk ? Ok() : Ok(new { result.Title });
        }
    }
}
