using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Helpers.DTOs.Courses;
using Service.Services.Interfaces;

namespace LanguageAcademy_API_FinalProject.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCoursesAsync()
        {
            return Ok(await _courseService.GetAllUIAsync());
        }

        [HttpGet("{slug}")]
        public async Task<IActionResult> GetCourseAsync(string slug)
        {
            var course = await _courseService.GetUIAsync(slug);
            if (course is null) return NotFound();
            return Ok(course);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin + "," + Roles.SuperAdmin)]
        public async Task<IActionResult> CreateAsync([FromBody] CourseCreateDto? dto)
        {
            if (dto is null) return BadRequest();

            var result = await _courseService.CreateAsync(dto);
            if (!result.Succeeded)
                return BadRequest(new { errors = result.Errors });

            return Ok(new { result.Slug, result.Title });
        }
    }
}
