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
        public ActionResult Listing()
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
                        Product_ID = Convert.ToInt32(reader["Product_ID"]),
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
            return View("ProductListing",prod_list);
        }

        [HttpGet]
        public ActionResult InsertProduct()
        {
            return View("ProductInsert");
        }

        [HttpPost]
        public ActionResult InsertProduct(ProductModel prop)
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
                    
                    int sucess = command.ExecuteNonQuery();
                    if (sucess > 0)
                    {
                        TempData["DataInsertMessage"] = "Data Inserted";
                    }

                }
            }
            return RedirectToAction("InsertProduct");
            
        }

        //Edit.....
        [HttpGet]
        public ActionResult UpdateProduct(int? id)
        {
            ProductModel edit = new ProductModel();
            using (SqlConnection connect_edit = new SqlConnection(strconnect))
            {
                if (connect_edit.State != ConnectionState.Open)
                {
                    connect_edit.Open();
                }
                if (id != 0)
                {
                    SqlCommand cmd_update = new SqlCommand("Select * from Training_Products where Product_ID = @Product_ID", connect_edit);

                    cmd_update.Parameters.AddWithValue("@Product_ID", id);

                    SqlDataReader reader = cmd_update.ExecuteReader();
                    while (reader.Read())
                    {
                        edit.Product_name = Convert.ToString(reader["Prod_Name"]);
                        edit.Price = Convert.ToInt32(reader["Price"]);
                        edit.NoOfProducts = Convert.ToInt32(reader["No_Of_Products"]);
                        edit.Date = Convert.ToDateTime(reader["Visible_Till"]);
                        edit.Description = Convert.ToString(reader["Product_Description"]);
                        edit.IsActive = Convert.ToBoolean(reader["IsActive"]);
                    }
                }
                return View("ProductInsert", edit);
            }
        }


        [HttpPost]
        public ActionResult UpdateProduct(int id)
        {
            ProductModel edit = new ProductModel();
            if (id != 0)
            {
                using (SqlConnection connect = new SqlConnection(strconnect))
                {
                    SqlCommand cmd_update = new SqlCommand("Update Training_Products SET Product_name = @Prod_Name, Price = @Price, NoOfProducts = @No_Of_Products, Date = @Visible_Till, Description = @Product_Description, IsActive = @IsActive where Product_ID = @Product_ID", connect);

                    if (connect.State != ConnectionState.Open)
                    {
                        connect.Open();
                    }
                    cmd_update.Parameters.AddWithValue("@Product_ID", id);
                    cmd_update.Parameters.AddWithValue("@Prod_Name", edit.Product_name);
                    cmd_update.Parameters.AddWithValue("@Price", edit.Price);
                    cmd_update.Parameters.AddWithValue("@No_Of_Products", edit.NoOfProducts);
                    cmd_update.Parameters.AddWithValue("@Visible_Till", edit.Date);
                    cmd_update.Parameters.AddWithValue("@Product_Description", edit.Description);
                    cmd_update.Parameters.AddWithValue("@IsActive", edit.IsActive);

                    cmd_update.ExecuteNonQuery();

                }
            }
            return View("ProductListing", edit);
        }
    }
}
