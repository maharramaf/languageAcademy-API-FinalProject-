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
    }
}
