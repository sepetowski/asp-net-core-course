using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UdemyCourse.API.Models.DTO;
using UdemyCourse.API.Repositories;


namespace UdemyCourse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        public UserManager<IdentityUser> UserManager { get; }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {

            IdentityUser identityUser = new()
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
            };

            var identityResult =await userManager.CreateAsync(identityUser, registerDto.Password);

           if(identityResult.Succeeded)
            {
                if(registerDto.Roles != null && registerDto.Roles.Any())
                {
                    identityResult= await userManager.AddToRolesAsync(identityUser, registerDto.Roles);
                    if (identityResult.Succeeded)
                    {

              
                        return Ok("User was registered");
                    }
                }
            }

            return BadRequest("Something went wrong");

        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
           IdentityUser? user= await userManager.FindByEmailAsync(loginDto.Email);

            if(user == null)
                return BadRequest("User not found");

           bool ckeckedPassword = await userManager.CheckPasswordAsync(user, loginDto.Password);

            if(!ckeckedPassword)
                return BadRequest("Wrong password");

           var roles= await userManager.GetRolesAsync(user);

            if(roles == null)
                return BadRequest("User roles not found");

            var jwtToken= tokenRepository.CreateJwtToken(user, roles.ToList());

            var response = new LoginResponseDto { JwtToken = jwtToken };

            return Ok(response);

        }
    }
}
