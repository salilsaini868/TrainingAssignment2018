using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using WebAPIs.Data;
using WebAPIs.Models;

namespace WebAPIs.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly WebApisContext context;
        private IConfiguration config;

        public LoginController(WebApisContext APIcontext, IConfiguration _config)
        {
            context = APIcontext;
            config = _config;
        }

        /// <summary>
        /// Authenticates the user.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>
        /// Token string for correct details.
        /// </returns>

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> LoginUser(string username, string password)
        {
            IActionResult response = Unauthorized();
            if (username != null && password != null)
            {
                if (!ModelState.IsValid)
                {
                    return response = BadRequest(ModelState);
                }
                var user = await context.LoginTable.SingleOrDefaultAsync(m => m.Username == username && m.Password == password);
                if (user == null)
                {
                    return response = Ok(new { message = "Username or Password is incorrect" });
                }
                var tokenString = BuildToken(user);
                response = Ok(new { token = tokenString });
                return Ok(response);
            }
            else
            {
                return response = Ok(new { message= "Enter username and password" });
            }
        }

        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>
        /// Returns token generated.
        /// </returns>
        
        private string BuildToken(LoginModel user)
        {

            var claims = new[] {
                new Claim(ClaimTypes.Sid , user.UserID.ToString()),
                new Claim(ClaimTypes.GivenName, user.Username),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.NameIdentifier, user.LastName),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(config["Jwt:Issuer"],
              config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}