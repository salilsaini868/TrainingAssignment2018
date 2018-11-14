using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebAPIs.Data;
using WebAPIs.Models;

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly WebApisContext context;
        private IConfiguration config;

        public UserController(WebApisContext APIcontext, IConfiguration _config)
        {
            context = APIcontext;
            config = _config;
        }

        /// <summary>
        /// Creates new user.
        /// </summary>
        /// <param name="login"></param>
        /// <returns>
        /// Login object with new UserID.
        /// </returns>
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(UserModel))]
        public async Task<IActionResult> CreateLogin([FromBody] [Required] UserModel login)
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
