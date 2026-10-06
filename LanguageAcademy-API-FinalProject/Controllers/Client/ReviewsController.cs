using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace LanguageAcademy_API_FinalProject.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<IActionResult> GetReviewsAsync()
        {
            var section = await _reviewService.GetUIAsync();
            if (section is null) return NotFound();
            return Ok(section);
        }
    }
}
