using IdentityService.Business.Abstract;
using IdentityService.Entities.Dtos;
using IdentityService.Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MusicWebApp.IdentityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IAccountService _identityService;
        private readonly UserManager<User> _userManager;

        public IdentityController(IAccountService identityService, UserManager<User> userManager)
        {
            _identityService = identityService;
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromForm] RegisterDto dto)
        {
            var result = await _identityService.RegisterAsync(dto);
            if (result)
            {
                return Ok("Registered successfully.");
            }
            return BadRequest("Can not register the user.");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var token = await _identityService.LoginAsync(dto);

            if (token != string.Empty)
            {
                var loggedUser = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == dto.Username);
                return Ok(new { Token = token, LoggedUser = loggedUser });
            }
            return BadRequest("Can not login the user .");
        }
    }
}
