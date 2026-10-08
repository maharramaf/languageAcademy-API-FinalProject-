using Microsoft.AspNetCore.Mvc;
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
    }
}
