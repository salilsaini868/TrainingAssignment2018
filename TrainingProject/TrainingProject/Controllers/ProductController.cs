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
            return View("ProductListing", prod_list);
        }

        [HttpGet]
        public ActionResult InsertProduct()
        {
            return View("ProductInsert");
        }

        [HttpPost]
        public ActionResult InsertUpdateProduct(ProductModel prop)
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
                else
                {
                    SqlCommand cmd_update = new SqlCommand("Update Training_Products SET Prod_Name = @Prod_Name, Price = @Price, No_Of_Products = @No_Of_Products, Visible_Till = @Visible_Till, Product_Description = @Product_Description, IsActive = @IsActive where Product_ID = @Product_ID", connect);

                    if (connect.State != ConnectionState.Open)
                    {
                        connect.Open();
                    }
                    cmd_update.Parameters.AddWithValue("@Product_ID", prop.Product_ID);
                    cmd_update.Parameters.AddWithValue("@Prod_Name", prop.Product_name);
                    cmd_update.Parameters.AddWithValue("@Price", prop.Price);
                    cmd_update.Parameters.AddWithValue("@No_Of_Products", prop.NoOfProducts);
                    cmd_update.Parameters.AddWithValue("@Visible_Till", prop.Date);
                    cmd_update.Parameters.AddWithValue("@Product_Description", prop.Description);
                    cmd_update.Parameters.AddWithValue("@IsActive", prop.IsActive);

                    int sucess = cmd_update.ExecuteNonQuery();
                    if (sucess > 0)
                    {
                        TempData["DataInsertMessage"] = "Data Updated";
                    }

                }
            }
            return RedirectToAction("InsertProduct");
        }

        //Edit.....
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

        [HttpPost]
        public ActionResult Listing(FormCollection collection)
        {
            string searchName = collection["name"];
            List<ProductModel> p_list = new List<ProductModel>();
            using (SqlConnection connect_search = new SqlConnection(strconnect))
            {
                if (connect_search.State != ConnectionState.Open)
                {
                    connect_search.Open();
                }
                if (!string.IsNullOrEmpty(searchName))
                {
                    ViewBag.search = searchName;
                    SqlCommand cmd_search = new SqlCommand("Select * from Training_Products where  Prod_Name Like '%' + @Prod_Name + '%' OR Product_Description Like '%' + @Prod_Name + '%'  ", connect_search);
                    cmd_search.Parameters.AddWithValue("@Prod_Name", searchName);

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
                        //return View("ProductListing", p_list);
                    }
                }
                else
                {
                    SqlCommand cmd_search = new SqlCommand("select * from Training_Products order by Product_ID DESC", connect_search);
                    SqlDataReader reader = cmd_search.ExecuteReader();
                    while (reader.Read())
                    {
                        ProductModel prop = new ProductModel
                        {
                            //Product_ID = Convert.ToInt32(reader["Product_ID"]),
                            Product_name = Convert.ToString(reader["Prod_Name"]),
                            Price = Convert.ToInt32(reader["Price"]),
                            NoOfProducts = Convert.ToInt32(reader["No_Of_Products"]),
                            Date = Convert.ToDateTime(reader["Visible_Till"]),
                            Description = Convert.ToString(reader["Product_Description"]),
                            IsActive = Convert.ToBoolean(reader["IsActive"])
                        };
                        p_list.Add(prop);

                    }

                }
                return View("ProductListing", p_list);
            }


        }

    }
}



