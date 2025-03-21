using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleRestAPI.Application.EmployeesApplication;
using SimpleRestAPI.Shared.ViewModels;

namespace Presentation.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IEmployeeApplication _employeeApplication;
        public LoginController(IEmployeeApplication employeeApplication)
        {
            _employeeApplication = employeeApplication;

        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel login)
        {
            try
            {
                var result = await _employeeApplication.Login(login);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
//hash para 123456: $2b$10$UgbfxfnbHRmTggYq6TUB3.HlbsYHQe.4Jgvt0uc5hi60tIw.LJ7ym