using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NET5_JWT.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class JWTController : ControllerBase
    {
        [HttpPost]
        public IActionResult Index()
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "Shailesh"),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var jwtResult = GenerateTokens("Shailesh", claims, DateTime.Now);

            return Ok(new LoginResult 
            { 
                UserName = "Shailesh",
                Role = "Admin",
                AccessToken = jwtResult.AccessToken
                });
        }

        private JwtAuthResult GenerateTokens(string userName, Claim[] claims, DateTime now)
        {
            var jwtToken = new JwtSecurityToken(claims: claims, expires: now.AddMinutes(5),
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes("35_RcbRio_A03Xh29o467IgiQbfXk9S5In0Sz54fbic")), SecurityAlgorithms.HmacSha256));

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return new JwtAuthResult
            {
                AccessToken = accessToken
            };
        }
    }

    public class LoginResult 
    {
        public string UserName { get; set; }
        public string Role { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
    public class JwtAuthResult 
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
