using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrainingProject.Controllers
{
    [RedirectToLogin]
    public class DashboardController : Controller
    {
        // GET: Dashboard
        string strConnect = string.Empty;
        public DashboardController()
        {
            strConnect = @"Data Source=172.20.21.129; MultipleActiveResultSets=True; Initial Catalog=RHPM; User ID=RHPM; Password=evry@123";
        }
        public ActionResult Index()
        {
            using (SqlConnection connect_listview = new SqlConnection(strConnect))
            {
                if (connect_listview.State != ConnectionState.Open)
                {
                    connect_listview.Open();
                }
                DataTable searchResult = new DataTable();
                SqlCommand cmd_search = new SqlCommand("Training_searchCategory", connect_listview);
                cmd_search.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter_search = new SqlDataAdapter(cmd_search);
                adapter_search.Fill(searchResult);
                var category_count = searchResult.Rows.Count;
                TempData["Message_CategoryCount"] = category_count;

                DataTable searchProduct  = new DataTable();
                SqlCommand cmd_search_product = new SqlCommand("Training_SearchProduct", connect_listview);
                cmd_search_product.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter_search_product = new SqlDataAdapter(cmd_search_product);
                adapter_search_product.Fill(searchProduct);
                var product_count = searchProduct.Rows.Count;
                TempData["Message_ProductCount"] = product_count;

                connect_listview.Close();
                return View();
            }            
        }
    }
}