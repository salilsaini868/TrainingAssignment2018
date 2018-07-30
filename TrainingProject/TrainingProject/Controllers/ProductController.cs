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



        [HttpGet]
        public ActionResult Product_Edit(int? id)
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
                return View(edit);
            }
        }

        [HttpPost]
        public ActionResult Product_Edit(int id)
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
            return View(edit);
        }


    }
}