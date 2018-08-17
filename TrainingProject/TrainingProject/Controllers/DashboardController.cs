using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using TrainingProject.Helper;

namespace TrainingProject.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        ConnectionHelper sqlconnect = new ConnectionHelper();

        public ActionResult Index()
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            var count_category = sqlconnect.CreateResult(executeType: ExecuteEnum.List, query: "Training_searchCategory", command: CommandType.StoredProcedure, valuePairs: parameter);
            var category_count = count_category.Rows.Count;
            TempData["Message_CategoryCount"] = category_count;

            List<KeyValuePair<string, object>> prod_parameter = new List<KeyValuePair<string, object>>();
            var count_product = sqlconnect.CreateResult(executeType: ExecuteEnum.List, query: "Training_SearchProduct", command: CommandType.StoredProcedure, valuePairs: prod_parameter);
            var product_count = count_product.Rows.Count;
            TempData["Message_ProductCount"] = product_count;

            return View();
        }
    }
}
