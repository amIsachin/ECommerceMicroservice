using Auth.Application.Interfaces;
using Auth.Core.Entities;
using Auth.Core.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager, IJwtService _jwtService) : ControllerBase // SignInManager<ApplicationUser> _signInManager
    {
        [HttpPost(Name = "Register")]
        public async Task<IActionResult> Register(RegisterEntity registerModel)
        {
            var user = new ApplicationUser { UserName = registerModel.Email, Email = registerModel.Email, Name = "Test" };

            var result = await _userManager.CreateAsync(user, registerModel.Password);

            if (result.Succeeded)
            {
                // await _signInManager.SignInAsync(user, isPersistent: false);

                // 🔹 Ensure the role exists (e.g., "User")
                var roleExists = await _roleManager.RoleExistsAsync(registerModel.Role);

                if (!roleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole(registerModel.Role));
                }

                // 🔹 Assign role to user
                await _userManager.AddToRoleAsync(user, registerModel.Role);

                return Ok(result);
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginEntity loginEntity)
        {
            var user = await _userManager.FindByEmailAsync(loginEntity.Email);

            if (user is null)
            {
                return Unauthorized("Invalid email or password");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginEntity.Password);

            if (!isPasswordValid)
            {
                return Unauthorized("Invalid email or password");
            }

            var roles = await _userManager.GetRolesAsync(user);

            var token = _jwtService.GenerateToken(user, roles);

            return Ok(new { Token = token });
        }
    }
}
