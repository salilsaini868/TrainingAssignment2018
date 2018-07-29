using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrainingProject.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        string strConnect = string.Empty;
        public CategoryController()
        {
            strConnect = @"Data Source=172.20.21.129; MultipleActiveResultSets=True; Initial Catalog=RHPM; User ID=RHPM; Password=evry@123";
        }
        public ActionResult CategoryPage_ListView()
        {
            DataTable dataset = new DataTable();
            using (SqlConnection connect_listview = new SqlConnection(strConnect))
            {
                if (connect_listview.State != ConnectionState.Open)
                {
                    connect_listview.Open();
                }
                SqlCommand list_category = new SqlCommand("select * from Training_ProductCategories", connect_listview);
                SqlDataAdapter adapter = new SqlDataAdapter(list_category);
                adapter.Fill(dataset);
                connect_listview.Close();
            }
            return View(dataset);
        }
    }
}