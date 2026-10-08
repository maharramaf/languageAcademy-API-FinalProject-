using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace LanguageAcademy_API_FinalProject.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.Admin + "," + Roles.SuperAdmin)]
    public class StudentsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public StudentsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudentsAsync()
        {
            return Ok(await _accountService.GetStudentsAsync());
        }
    }
}
