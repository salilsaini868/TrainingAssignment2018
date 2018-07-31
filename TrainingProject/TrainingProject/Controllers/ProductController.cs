using System;
using System.Collections.Generic;
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
        [HttpGet]
        public ActionResult Product_Listing()
        {
            List<ProductModel> prod_list = new List<ProductModel>();

            // SELECT USER      

            using (SqlConnection connect = new SqlConnection(strconnect))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[Training_Products_Select]", connect);
                cmd.CommandType = CommandType.StoredProcedure;
                if (connect.State != ConnectionState.Open)
                {
                    connect.Open();
                }

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ProductModel prop = new ProductModel
                    {
                        Product_name = Convert.ToString(reader["Prod_Name"]),
                        Price = Convert.ToInt32(reader["Price"]),
                        NoOfProducts = Convert.ToInt32(reader["No_Of_Products"]),
                        Date = Convert.ToDateTime(reader["Visible_Till"]),
                        Description = Convert.ToString(reader["Product_Description"]),
                        IsActive = Convert.ToBoolean(reader["IsActive"])
                    };
                    prod_list.Add(prop);
                }
                connect.Close();
            }
            return View(prod_list);
        }

        [HttpGet]
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
            return RedirectToAction("Product_Listing");
        }

    }
}
