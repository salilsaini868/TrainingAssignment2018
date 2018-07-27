using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingProject.Models;
using System.Data;
using System.Data.SqlClient;

namespace TrainingProject.Controllers
{
    public class ProductController : Controller
    {

        string strconnect = string.Empty;

        public ProductController()
        {
            strconnect = @"Data Source=172.20.21.129;MultipleActiveResultSets=True;Initial Catalog=RHPM;User ID=RHPM;Password=evry@123";
        }

        // GET: Product
        public ActionResult Products_Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Products_Index(ProductModel prop)
        {
           
            using (SqlConnection connect = new SqlConnection(strconnect))
            {
                if (connect.State != ConnectionState.Open)
                {
                    connect.Open();
                }

                if (prop.Product_ID == 0)
                {
                    SqlCommand command = new SqlCommand("[dbo].[Training_Products_Insert]", connect)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    command.Parameters.AddWithValue("@Prod_Name", prop.Product_name);
                    command.Parameters.AddWithValue("@Price", prop.Price);
                    command.Parameters.AddWithValue("@No_Of_Products", prop.NoOfProducts);
                    command.Parameters.AddWithValue("@Visible_Till", prop.Date);
                   
                    command.Parameters.AddWithValue("@Product_Description", prop.Description);
                    command.Parameters.AddWithValue("@IsActive ", prop.IsActive);
                    command.ExecuteNonQuery();
                }
            }
            return View(prop);
        }

    }
}