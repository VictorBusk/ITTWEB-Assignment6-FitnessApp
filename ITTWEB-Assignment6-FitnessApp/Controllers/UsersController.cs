using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ITTWEB_Assignment6_FitnessApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ITTWEB_Assignment6_FitnessApp.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    [Authorize]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UsersController(ApplicationDbContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        [AllowAnonymous]
        [HttpGet]
        public IActionResult HelloWorld() {
            return Ok(new { HelloWorld = "Hello World!" });
        }
        
        [AllowAnonymous]
        [HttpPost("users")]
        //public async Task<IActionResult> Register([FromBody] string email, [FromBody] string password, [FromBody] string name)
        public async Task<IActionResult> Post([FromBody] DtoUser dtoUser)
        {
            var newUser = new User()
            {
                UserName = dtoUser.Email,
                Email = dtoUser.Email,
                Name = dtoUser.Name
            };

            var userCreationResult = await _userManager.CreateAsync(newUser, dtoUser.Password);
            if (userCreationResult.Succeeded)
            {
                return Ok(new { token = "Bearer " + GenerateToken(newUser.Email)});
            }
            foreach (var error in userCreationResult.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
            return BadRequest(ModelState);
        }
                
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody]DtoUser dtoUser)
        {
            var user = await _userManager.FindByEmailAsync(dtoUser.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login");
                return BadRequest(ModelState);
            }
            var passwordSignInResult = await _signInManager.CheckPasswordSignInAsync(user, dtoUser.Password, false);
            if (passwordSignInResult.Succeeded)
            {
                return Ok(new { token = "Bearer " + GenerateToken(dtoUser.Email)});
            }
            return BadRequest("Invalid login");
        }
                
        [HttpGet("me")]
        public JsonResult Me()
        {
            var token = HttpContext.Request?.Headers["Authorization"];
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token.ToString().Replace("Bearer ", ""));
            var jwtClaims = jwtToken.Claims;
            var jwtUsername = jwtClaims.First(c => c.Type == ClaimTypes.Name).Value;
            var userDto = _context.Users.First(u => u.UserName == jwtUsername);
            return Json(userDto);
        }
                
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        private string GenerateToken(string username)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddHours(12)).ToUnixTimeSeconds().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("70061ee6-92a1-4bd2-8ba3-2b38d7050f14"));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "JWT",
                audience: "ittweb6.herokuapp.com",
                claims: claims,
                expires: DateTime.Now.AddHours(12),
                signingCredentials: cred);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}