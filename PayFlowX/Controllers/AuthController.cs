using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PayFlowX.Models;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace PayFlowX.Controllers
{

    [ApiController]
    [Route("api/[Controller]")]
    public class AuthController : Controller
    {
        private readonly IConfiguration _IConfiguration;


        public AuthController(IConfiguration configuration) {

            _IConfiguration = configuration;
        
        }


        [HttpPost("Login")]
        public IActionResult  Login([FromBody] PayFlowX.Models.LoginRequest loginRequest)
        {

            if (loginRequest.Email == "Qalb123@gmail.com" && loginRequest.Password == "123") {



                var Claims = new[] {
                new Claim(ClaimTypes.Email,loginRequest.Email ),
                new Claim(ClaimTypes.Role,"Admin")
                };

                var key = new SymmetricSecurityKey(
                      Encoding.UTF8.GetBytes(_IConfiguration["Jwt:Key"]!)
                  );

                var credentials = new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256
                );

                var token = new JwtSecurityToken(
                    issuer: _IConfiguration["Jwt:Issuer"],
                    audience: _IConfiguration["Jwt:Audience"],
                    claims: Claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: credentials
                );

                var tokenString = new JwtSecurityTokenHandler()
                    .WriteToken(token);

                return Ok(new { token = tokenString });
            }
            else
            {
                return Unauthorized();
            }



        }


    }
}
