﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TrainingProject.Models;
using System.Data;
using System.Data.SqlClient;
using TrainingProject.Helper;


namespace TrainingProject.Controllers
{
    [AuthorizationFilter]
    public class ProductController : Controller
    {
        string strconnect = string.Empty;
        ConnectionHelper sqlconnect = new ConnectionHelper();

        public ActionResult Listing(FormCollection collection)
        {
            string searchName = collection["txtSearch"];
            List<ProductModel> ListOfProducts = new List<ProductModel>();
            ViewBag.search = searchName;
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>();
            param.Add(new KeyValuePair<string, object>("SearchProduct", searchName));
            var search_list = sqlconnect.CreateResult(executeType: ExecuteEnum.ListProduct, query: "Training_SearchProduct", command: CommandType.StoredProcedure, valuePairs: param);
            ListOfProducts = SearchFunction(search_list);
            var count = ListOfProducts.Count;
            if (count == 0)
            {
                TempData["DataNotFound"] = "No records found.";
            }
            return View("ProductListing", ListOfProducts);
        }
        List<ProductModel> SearchFunction(SqlCommand cmd_search)
        {
            List<ProductModel> p_list = new List<ProductModel>();
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>();
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
                    VisibleDate = Convert.ToDateTime(reader["Visible_Till"]),
                    Description = Convert.ToString(reader["Product_Description"]),
                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                    CreatedBy = Convert.ToInt32(reader["CreatedBy"]),
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                    CreatedUser = Convert.ToString(reader["CreatedUser"]),
                    ModifiedUser = reader["ModifiedUser"] != DBNull.Value ? Convert.ToString(reader["ModifiedUser"]) : null,
                    ModifiedBy = reader["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(reader["ModifiedBy"]) : 0,
                    ModifiedDate = reader["ModifiedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ModifiedDate"]) : default(DateTime),
                };
                p_list.Add(prop);
            }
            return p_list;
        }

        [HttpGet]
        public ActionResult InsertProduct()
        {
            ViewBag.Message = "Insert Product";
            GetCategories();
            ViewBag.Message = "Insert Product";

            return View("ProductInsert");
        }

        [HttpPost]
        public ActionResult InsertUpdateProduct(ProductModel prop)
        {
            var userlogin = Session["user"] as LoginModel;
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>();
            param.Add(new KeyValuePair<string, object>("Prod_Name", prop.Product_name));
            param.Add(new KeyValuePair<string, object>("CategoryID", prop.CategoryID));
            param.Add(new KeyValuePair<string, object>("Price", prop.Price));
            param.Add(new KeyValuePair<string, object>("No_Of_Products", prop.NoOfProducts));
            param.Add(new KeyValuePair<string, object>("Visible_Till", prop.VisibleDate));
            param.Add(new KeyValuePair<string, object>("Product_Description", prop.Description));
            param.Add(new KeyValuePair<string, object>("IsActive", prop.IsActive));


            if (prop.Product_ID == 0)
            {
                prop.CreatedUser = userlogin.Username;
                param.Add(new KeyValuePair<string, object>("CreatedBy", userlogin.UserID));
                param.Add(new KeyValuePair<string, object>("CreatedDate", DateTime.Now));

                TempData["DataInsertorUpdateMessage"] = "Product Added.";
            }
            else
            {
                param.Add(new KeyValuePair<string, object>("Product_ID", prop.Product_ID));
                param.Add(new KeyValuePair<string, object>("ModifiedBy", userlogin.UserID));
                param.Add(new KeyValuePair<string, object>("ModifiedDate", DateTime.Now));

                TempData["DataInsertorUpdateMessage"] = "Product Updated.";
            }
            var command_insert = sqlconnect.CreateResult(executeType: ExecuteEnum.Insert, query: "Training_Products_Insert", command: CommandType.StoredProcedure, valuePairs: param);
            int result = command_insert;

            return View("ProductInsert", prop);
        }

        public void GetCategories()
        {
            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>();

            var command_insert = sqlconnect.CreateResult(executeType: ExecuteEnum.ListProduct, query: "Training_GetCategoryName", command: CommandType.StoredProcedure, valuePairs: param);
            var result = command_insert;

            SqlDataAdapter da = new SqlDataAdapter(result);
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
                            Value = Convert.ToString(row["CategoryID"]),
                            Text = Convert.ToString(row["CategoryName"])
                        });
                };
            }
            TempData["Categories"] = Categories;
            TempData.Keep();
        }

        [HttpGet]
        public ActionResult GetProductByID(int? id)
        {
            ViewBag.Message = "Update Product";
            GetCategories();
            ViewBag.Message = "Update Product";

            ProductModel edit = new ProductModel();

            if (id != 0)
            {
                List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>();
                param.Add(new KeyValuePair<string, object>("Product_ID", id));
                var command_select = sqlconnect.CreateResult(executeType: ExecuteEnum.Detail, query: "Select * from Training_Products where Product_ID = @Product_ID", command: CommandType.Text, valuePairs: param);

                command_select.Read();
                {
                    edit.Product_ID = Convert.ToInt32(command_select["Product_ID"]);
                    edit.Product_name = Convert.ToString(command_select["Prod_Name"]);
                    edit.Price = Convert.ToInt32(command_select["Price"]);
                    edit.NoOfProducts = Convert.ToInt32(command_select["No_Of_Products"]);
                    edit.CategoryID = Convert.ToString(command_select["CategoryID"]);
                    edit.VisibleDate = Convert.ToDateTime(command_select["Visible_Till"].ToString());
                    edit.Description = Convert.ToString(command_select["Product_Description"]);
                    edit.IsActive = Convert.ToBoolean(command_select["IsActive"]);
                    edit.CreatedBy = Convert.ToInt32(command_select["CreatedBy"]);
                    edit.CreatedDate = Convert.ToDateTime(command_select["CreatedDate"]);
                    edit.ModifiedBy = command_select["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(command_select["ModifiedBy"]) : 0;
                    edit.ModifiedDate = command_select["ModifiedDate"] != DBNull.Value ? Convert.ToDateTime(command_select["ModifiedDate"]) : default(DateTime);
                }
            }
            return View("ProductInsert", edit);
        }
        public ActionResult DeleteProduct(int ID)
        {
            DataTable dataset = new DataTable();

            List<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>();
            param.Add(new KeyValuePair<string, object>("Product_ID", ID));

            var cmd_delete = sqlconnect.CreateResult(executeType: ExecuteEnum.Delete, query: "Training_Products_Delete", command: CommandType.StoredProcedure, valuePairs: param);
            int del_user = cmd_delete;

            return RedirectToAction("Listing");
        }
    }
}

