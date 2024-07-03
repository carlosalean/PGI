using BackEnd_PGI.Interface;
using BackEnd_PGI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_PGI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUsuarioRepository _usuarioRepository;

        public AuthController(IConfiguration configuration, IUsuarioRepository usuarioRepository)
        {
            _configuration = configuration;
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            if (userLogin == null || string.IsNullOrEmpty(userLogin.Username) || string.IsNullOrEmpty(userLogin.Password))
            {
                return BadRequest("Invalid client request");
            }

            var usuario = await _usuarioRepository.GetUsuarioByCredentialsAsync(userLogin.Username, userLogin.Password);

            if (usuario != null)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.NombreUsuario!),
                };

                var tokenOptions = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return Ok(new { Token = tokenString });
            }

            return Unauthorized();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Usuario usuario)
        {
            if (usuario == null || string.IsNullOrEmpty(usuario.NombreUsuario) || string.IsNullOrEmpty(usuario.Password))
            {
                return BadRequest("Invalid user data");
            }

            var createdUsuario = await _usuarioRepository.CreateAsync(usuario);
            return CreatedAtAction(nameof(GetById), new { id = createdUsuario.ID }, createdUsuario);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Usuario usuario)
        {
            if (id != usuario.ID || usuario == null)
            {
                return BadRequest("Invalid user data");
            }

            await _usuarioRepository.UpdateAsync(usuario);
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _usuarioRepository.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("change-password/{id}")]
        public async Task<IActionResult> ChangePassword(int id, [FromBody] ChangePasswordModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.CurrentPassword) || string.IsNullOrEmpty(model.NewPassword))
            {
                return BadRequest("Invalid password data");
            }

            var result = await _usuarioRepository.ChangePasswordAsync(id, model.CurrentPassword, model.NewPassword);
            if (!result)
            {
                return Unauthorized("Current password is incorrect or user not found");
            }

            return NoContent();
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
            {
                return NotFound("User not found");
            }
            return Ok(usuario);
        }
    }

    public class UserLogin
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }

    public class ChangePasswordModel
    {
        public string? CurrentPassword { get; set; }
        public string? NewPassword { get; set; }
    }
}
