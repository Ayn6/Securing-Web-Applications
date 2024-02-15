using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly JWTSettings _jwtSettings;
        public AccountController(IOptions<JWTSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        [HttpGet("GetToken")]

        public string GetToken()
        {
            List<Claim> claim = new List<Claim>();
            claim.Add(new Claim(ClaimTypes.Name, "Nataly"));
            claim.Add(new Claim("level", "123"));
            claim.Add(new Claim(ClaimTypes.Role, "Admin"));

            var singingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

            var jwt = new JwtSecurityToken(
                        issuer: _jwtSettings.Issuer,
                        audience: _jwtSettings.Audience,
                        claims: claim,
                        expires: DateTime.UtcNow.Add(TimeSpan.FromHours(1)),
                        signingCredentials: new SigningCredentials(singingKey, SecurityAlgorithms.HmacSha256)
                );
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return token;
        }
        [HttpGet("GetTokenF")]

        public string GetTokenF()
        {
            List<Claim> claim = new List<Claim>();
            claim.Add(new Claim(ClaimTypes.Name, "Nataly"));
            claim.Add(new Claim("level", "123"));
            claim.Add(new Claim(ClaimTypes.Role, "Admin"));

            var singingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

            var jwt = new JwtSecurityToken(
                        issuer: _jwtSettings.Issuer,
                        audience: _jwtSettings.Audience,
                        claims: claim,
                        signingCredentials: new SigningCredentials(singingKey, SecurityAlgorithms.HmacSha256)
                );
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return token;
        }

        [HttpGet]
        public IActionResult TestAuthorization(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                var currentToken = GetTokenF();
                // Проверяем валидность переданного токена
                if (token == currentToken)
                {
                    return Ok(new { message = "Авторизация через параметр прошла успешно" });
                }
                else
                {
                    return Unauthorized(new { message = "Неверный токен" });
                }
            }
            else
            {
                return Unauthorized(new { message = "Требуется авторизация" });
            }
        }
    }
}
