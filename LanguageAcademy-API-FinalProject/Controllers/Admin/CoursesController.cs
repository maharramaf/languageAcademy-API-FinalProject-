using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Helpers.DTOs.Courses;
using Service.Services.Interfaces;

namespace LanguageAcademy_API_FinalProject.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.Admin + "," + Roles.SuperAdmin)]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var course = await _courseService.GetByIdAsync(id);
            if (course is null) return NotFound();
            return Ok(course);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CourseCreateDto? dto)
        {
            if (dto is null) return BadRequest();

            var result = await _courseService.CreateAsync(dto);
            if (!result.Succeeded)
                return BadRequest(new { errors = result.Errors });

            return Ok(new { result.Slug, result.Title });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] CourseUpdateDto? dto)
        {
            if (dto is null) return BadRequest();

            var result = await _courseService.UpdateAsync(id, dto);
            if (!result.Succeeded)
            {
                if (result.Errors.Any(e => e == "Course was not found."))
                    return NotFound();
                return BadRequest(new { errors = result.Errors });
            }

            return Ok(new { result.Slug, result.Title });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _courseService.DeleteAsync(id);
            if (!result.Succeeded)
            {
                if (result.Errors.Any(e => e == "Course was not found."))
                    return NotFound();
                return BadRequest(new { errors = result.Errors });
            }

            return Ok();
        }

        [HttpGet("{id:int}/modules")]
        public async Task<IActionResult> GetModulesAsync(int id)
        {
            var course = await _courseService.GetCurriculumAsync(id);
            if (course is null) return NotFound();
            return Ok(course);
        }

        [HttpPost("{id:int}/modules")]
        public async Task<IActionResult> CreateModuleAsync(int id, [FromBody] CourseModuleCreateDto? dto)
        {
            if (dto is null) return BadRequest();

            var result = await _courseService.CreateModuleAsync(id, dto);
            if (!result.Succeeded)
            {
                if (result.Errors.Any(e => e == "Course was not found."))
                    return NotFound();
                return BadRequest(new { errors = result.Errors });
            }

            return Ok(new { result.Title });
        }

        [HttpPost("{id:int}/modules/{moduleId:int}/lessons")]
        public async Task<IActionResult> CreateLessonAsync(int id, int moduleId, [FromBody] LessonCreateDto? dto)
        {
            if (dto is null) return BadRequest();

            var result = await _courseService.CreateLessonAsync(id, moduleId, dto);
            if (!result.Succeeded)
            {
                if (result.Errors.Any(e => e == "Course was not found." || e == "Module was not found."))
                    return NotFound();
                return BadRequest(new { errors = result.Errors });
            }

            return Ok(new { result.Title });
        }

        [HttpGet("{id:int}/modules/{moduleId:int}")]
        public async Task<IActionResult> GetModuleAsync(int id, int moduleId)
        {
            var module = await _courseService.GetModuleAsync(id, moduleId);
            if (module is null) return NotFound();
            return Ok(module);
        }

        [HttpPut("{id:int}/modules/{moduleId:int}")]
        public async Task<IActionResult> UpdateModuleAsync(int id, int moduleId, [FromBody] CourseModuleCreateDto? dto)
        {
            if (dto is null) return BadRequest();
            return WriteResult(await _courseService.UpdateModuleAsync(id, moduleId, dto));
        }

        [HttpDelete("{id:int}/modules/{moduleId:int}")]
        public async Task<IActionResult> DeleteModuleAsync(int id, int moduleId)
        {
            return WriteResult(await _courseService.DeleteModuleAsync(id, moduleId), emptyOk: true);
        }

        [HttpGet("{id:int}/modules/{moduleId:int}/lessons/{lessonId:int}")]
        public async Task<IActionResult> GetLessonAsync(int id, int moduleId, int lessonId)
        {
            var lesson = await _courseService.GetLessonAsync(id, moduleId, lessonId);
            if (lesson is null) return NotFound();
            return Ok(lesson);
        }

        [HttpPut("{id:int}/modules/{moduleId:int}/lessons/{lessonId:int}")]
        public async Task<IActionResult> UpdateLessonAsync(int id, int moduleId, int lessonId, [FromBody] LessonCreateDto? dto)
        {
            if (dto is null) return BadRequest();
            return WriteResult(await _courseService.UpdateLessonAsync(id, moduleId, lessonId, dto));
        }

        [HttpDelete("{id:int}/modules/{moduleId:int}/lessons/{lessonId:int}")]
        public async Task<IActionResult> DeleteLessonAsync(int id, int moduleId, int lessonId)
        {
            return WriteResult(await _courseService.DeleteLessonAsync(id, moduleId, lessonId), emptyOk: true);
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
