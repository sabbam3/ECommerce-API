using ECommerce_API.Abstractions;
using ECommerce_API.Auth;
using ECommerce_API.Models.Authentication.LogIn;
using ECommerce_API.Models.Authentication.SignUp;
using ECommerce_API.Models.Email;
using ECommerce_API.Models.Entities;
using ECommerce_API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;


namespace ECommerce_API.Controllers
{
    [ApiController]
    [Route("auth/[controller]")]

    public class AuthController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly TokenGenerator _tokenGenerator;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;
        public AuthController(UserManager<UserEntity> userManager, TokenGenerator tokenGenerator, IEmailService emailService, IUserService userservice)
        {
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
            _emailService = emailService;
            _userService = userservice;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUser user)
        {
            var entity = new UserEntity();
            if (await _userManager.FindByEmailAsync(user.Email) != null)
            {
                return BadRequest("User already exists");
            }
            if (!await _userService.CreateUserAsync(user))
            {
                return BadRequest("Create user failed");
            }
            return Ok(await SendConfirmationLink(user));
        }
        [HttpGet("send-confirmation-link")]
        public async Task<IActionResult> SendConfirmationLink(RegisterUser user)
        {
            var entity = await _userManager.FindByEmailAsync(user.Email);
            
            if (!await _emailService.SendConfirmationLink(entity, user))
            {
                return BadRequest("Failed to send verification link");
            }
            return Ok();
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery]string email, [FromQuery]string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest("User not be found");   
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return BadRequest("Email Not verified");

            }
            return Ok("Your email verified");
        }
        [HttpPost("login")]
        public async Task<IActionResult> LogInUserAsync(LogInUser user)
        {
            var entity = await _userManager.FindByEmailAsync(user.Email);
            var result = await _userManager.CheckPasswordAsync(entity, user.Password);
            if (!result)
            {
                return BadRequest("Email or Password is incorrect");
            }
            return Ok(_tokenGenerator.Generate(entity.Id.ToString()));
        }
    }
}
