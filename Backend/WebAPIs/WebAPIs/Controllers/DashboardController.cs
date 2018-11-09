using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIs.Data;
using WebAPIs.Models;

namespace WebAPIs.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly WebApisContext context;

        public DashboardController(WebApisContext APIcontext)
        {
            context = APIcontext;
        }        

        [HttpGet]
        public async Task<IActionResult> GetCount()
        {
            StatisticsModel statisticsModel = new StatisticsModel();

            return Ok(statisticsModel);

        }
    }
}