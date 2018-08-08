﻿using System;
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

            string searchName = collection["txtSearch"];

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
                var userlogin = Session["user"] as LoginModel;
                ProductModel prop = new ProductModel
                {
                    Product_ID = Convert.ToInt32(reader["Product_ID"]),
                    Product_name = Convert.ToString(reader["Prod_Name"]),
                    Category = Convert.ToString(reader["Category"]),
                    Price = Convert.ToInt32(reader["Price"]),
                    NoOfProducts = Convert.ToInt32(reader["No_Of_Products"]),
                    Date = Convert.ToDateTime(reader["Visible_Till"]),
                    Description = Convert.ToString(reader["Product_Description"]),
                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                    CreatedBy = Convert.ToInt32(reader["CreatedBy"]),
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                    CreatedUser = Convert.ToString(reader["CreatedUser"]),
                    ModifiedUser = reader["ModifiedUser"] != DBNull.Value ? Convert.ToString(reader["ModifiedUser"]) : null,
                    ModifiedBy = reader["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(reader["ModifiedBy"]) : 0,
                    ModifiedDate = reader["ModifiedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ModifiedDate"]) : default(DateTime)
            };
                p_list.Add(prop);
            }
            return p_list;
        }

        [HttpGet]
        public ActionResult InsertProduct()
        {
            GetCategories();
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
        
        public void GetCategories()
        {

            using (SqlConnection con = new SqlConnection(strconnect))
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                
                SqlCommand cmd = new SqlCommand("Training_GetCategoryName", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                List<SelectListItem> Categories = new List<SelectListItem>();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Categories.Add(
                       new SelectListItem
                       {
                           Text = Convert.ToString(row["CategoryName"]),
                           Value = Convert.ToString(row["CategoryID"])
                       });
                        };
                }
                TempData["Categories"] = Categories;
            }
        }


        int InsertUpdateFunction(SqlCommand command_InsertUpdate, ProductModel prop)
        {
            var userlogin = Session["user"] as LoginModel;
            command_InsertUpdate.Parameters.AddWithValue("@Prod_Name", prop.Product_name);
            command_InsertUpdate.Parameters.AddWithValue("@CategoryID", prop.CategoryID);
            command_InsertUpdate.Parameters.AddWithValue("@Price", prop.Price);
            command_InsertUpdate.Parameters.AddWithValue("@No_Of_Products", prop.NoOfProducts);
            command_InsertUpdate.Parameters.AddWithValue("@CategoryId", prop.CategoryID);
            command_InsertUpdate.Parameters.AddWithValue("@Visible_Till", prop.Date);
            command_InsertUpdate.Parameters.AddWithValue("@Product_Description", prop.Description);
            command_InsertUpdate.Parameters.AddWithValue("@IsActive ", prop.IsActive);
            if (prop.Product_ID == 0)
            {
                prop.CreatedUser = userlogin.Username;
                command_InsertUpdate.Parameters.AddWithValue("@CreatedBy", userlogin.UserID);
                command_InsertUpdate.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
            }
            else
            {                
                command_InsertUpdate.Parameters.AddWithValue("@Product_ID", prop.Product_ID);
                command_InsertUpdate.Parameters.AddWithValue("@ModifiedBy", userlogin.UserID);
                command_InsertUpdate.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
            }
            var SuccessMessage = command_InsertUpdate.ExecuteNonQuery();
            return (SuccessMessage);
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
                        edit.Product_name = Convert.ToString(reader["CategoryID"]);
                        edit.Price = Convert.ToInt32(reader["Price"]);
                        edit.NoOfProducts = Convert.ToInt32(reader["No_Of_Products"]);
                        edit.CategoryID = Convert.ToInt32(reader["CategoryId"]);
                        edit.Date = Convert.ToDateTime(reader["Visible_Till"]);
                        edit.Description = Convert.ToString(reader["Product_Description"]);
                        edit.IsActive = Convert.ToBoolean(reader["IsActive"]);
                        edit.CreatedBy = Convert.ToInt32(reader["CreatedBy"]);
                        edit.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);                        
                        edit.ModifiedBy = reader["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(reader["ModifiedBy"]) : 0;
                        edit.ModifiedDate = reader["ModifiedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ModifiedDate"]) : default(DateTime);
                    }
                }
                return View("ProductInsert", edit);
            }
        }

        public ActionResult DeleteProduct(int ID)
        {
            DataTable dataset = new DataTable();
            using (SqlConnection connect = new SqlConnection(strconnect))
            {
                if (connect.State != ConnectionState.Open)
                {
                    connect.Open();
                }
                SqlCommand DeleteCommand = new SqlCommand("[dbo].[Training_Products_Delete]", connect);
                DeleteCommand.CommandType = CommandType.StoredProcedure;
                DeleteCommand.Parameters.AddWithValue("@Product_ID", ID);
                DeleteCommand.ExecuteNonQuery();
                connect.Close();
            }
            return RedirectToAction("Listing");
        }

    }
}

