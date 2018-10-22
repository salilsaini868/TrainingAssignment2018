using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIsTrainingProject.Models;
using WebAPIsTrainingProject.Data;

namespace WebAPIsTrainingProject.Controllers
{
    [Produces("application/json")]
    [Route("api/Dashboard")]
    public class DashboardController : Controller
    {
        //private readonly APIsTrainingContext context;

        //[HttpGet]
        //public IActionResult GetCount()
        //{
        //    StatisticsModel statisticsModel = new StatisticsModel();

        //    var categoryQuery = context.CategoryTable.ToList().Count;
        //    var productQuery = context.ProductTable.ToList().Count;
        //    statisticsModel.CategoryCount = (categoryQuery);
        //    statisticsModel.ProductCount = (productQuery);
        //    TempData["Message_CategoryCount"] = statisticsModel.CategoryCount;
        //    TempData["Message_ProductCount"] = statisticsModel.ProductCount;
        //    return Ok();

        //}
    }
}