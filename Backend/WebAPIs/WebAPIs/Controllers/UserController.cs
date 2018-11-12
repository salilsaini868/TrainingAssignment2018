using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPIs.Data;
using WebAPIs.Models;

namespace WebAPIs.Controllers
{
    [Produces("application/json")]
    [Route("api/User/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class UserController : Controller
    {
        private readonly WebApisContext context;
        private IConfiguration config;

        public UserController(WebApisContext APIcontext, IConfiguration _config)
        {
            context = APIcontext;
            config = _config;
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateLogin([FromBody] LoginModel login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.LoginTable.Add(login);
            await context.SaveChangesAsync();
            return CreatedAtAction("CreateLogin", new { id = login.UserID }, login);
        }
    }
}
