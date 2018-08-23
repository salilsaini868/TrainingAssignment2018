using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using TrainingProject.Models;
using TrainingProject.Helper;

namespace TrainingProject.Controllers
{
    [AuthorizationFilter]
    public class DashboardController : Controller
    {
        // GET: Dashboard
        ConnectionHelper sqlconnect = new ConnectionHelper();

        public ActionResult Index()
        {
            StatisticsModel statisticsModel = new StatisticsModel();
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            var list_count = sqlconnect.CreateResult( ExecuteEnum.Detail, "Training_ListCount", CommandType.StoredProcedure, parameter);
            list_count.Read();

            statisticsModel.CategoryCount = Convert.ToInt32(list_count["CategoryCount"]);
            statisticsModel.ProductCount = Convert.ToInt32(list_count["ProductCount"]);            
            TempData["Message_CategoryCount"] = statisticsModel.CategoryCount;
            TempData["Message_ProductCount"] = statisticsModel.ProductCount;
             
            return View();
        }
    }
}
