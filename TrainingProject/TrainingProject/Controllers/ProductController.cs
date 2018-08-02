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

        public ActionResult Listing(FormCollection collection)
        {
            SqlCommand cmd_search;
            string searchName = collection["name"];

            List<ProductModel> ListOfProducts = new List<ProductModel>();
            using (SqlConnection connect_search = new SqlConnection(strconnect))
            {
                if (connect_search.State != ConnectionState.Open)
                {
                    connect_search.Open();
                }

                ViewBag.search = searchName;
                cmd_search = new SqlCommand("[dbo].[Training_SearchProduct]", connect_search);
                cmd_search.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(searchName))
                {
                    cmd_search.Parameters.AddWithValue("@searchProduct", searchName);
                }

                ListOfProducts = SearchFunction(cmd_search);
                connect_search.Close();
                return View("ProductListing", ListOfProducts);
            }
        }

        List<ProductModel> SearchFunction(SqlCommand cmd_search)
        {
            List<ProductModel> p_list = new List<ProductModel>();
            SqlDataReader reader = cmd_search.ExecuteReader();
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
                p_list.Add(prop);
            }
            return p_list;
        }

        [HttpGet]
        public ActionResult InsertProduct()
        {
            return View("ProductInsert");
        }

        [HttpPost]
        public ActionResult InsertUpdateProduct(ProductModel prop)
        {
            SqlCommand command_InsertUpdate;

            using (SqlConnection connect = new SqlConnection(strconnect))
            {
                if (connect.State != ConnectionState.Open)
                {
                    connect.Open();
                }

                command_InsertUpdate = new SqlCommand("[dbo].[Training_Products_Insert]", connect);
                command_InsertUpdate.CommandType = CommandType.StoredProcedure;
                int InsertUpdate = InsertUpdateFunction(command_InsertUpdate, prop);
               
                if (InsertUpdate > 0)
                {
                    TempData["DataInsertMessage"] = prop.Product_ID > 0 ? "Data Updated" : "Data Inserted";
                }
 
            }
            return RedirectToAction("InsertProduct");
        }

        int InsertUpdateFunction(SqlCommand command_InsertUpdate, ProductModel prop)
        {
            command_InsertUpdate.Parameters.AddWithValue("@Prod_Name", prop.Product_name);
            command_InsertUpdate.Parameters.AddWithValue("@Price", prop.Price);
            command_InsertUpdate.Parameters.AddWithValue("@No_Of_Products", prop.NoOfProducts);
            command_InsertUpdate.Parameters.AddWithValue("@Visible_Till", prop.Date);
            command_InsertUpdate.Parameters.AddWithValue("@Product_Description", prop.Description);
            command_InsertUpdate.Parameters.AddWithValue("@IsActive ", prop.IsActive);
            var SucessMessage= command_InsertUpdate.ExecuteNonQuery();

            return (SucessMessage);
        }

        
        [HttpGet]
        public ActionResult GetProductByID(int? id)
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
                        edit.Product_ID = Convert.ToInt32(reader["Product_ID"]);
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
    }
}

