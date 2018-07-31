using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingProject.Models;

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

        [HttpGet]
        public ActionResult EditCategory(int? id)
        {
            CategoryModel category = new CategoryModel();
            if (id != null)
            {
                using (SqlConnection connect_selectcategory = new SqlConnection(strConnect))
                {
                    SqlCommand select_category = new SqlCommand("[dbo].[Training_selectCategory]", connect_selectcategory);
                    select_category.CommandType = CommandType.StoredProcedure;
                    if (connect_selectcategory.State != ConnectionState.Open)
                    {
                        connect_selectcategory.Open();
                    }
                    select_category.Parameters.AddWithValue("@CategoryId", id);
                    SqlDataReader reader = select_category.ExecuteReader();
                    while (reader.Read())
                    {
                        category.CategoryID = Convert.ToInt32(reader["CategoryID"]);
                        category.CategoryName = Convert.ToString(reader["CategoryName"]);
                        category.CategoryDescription = Convert.ToString(reader["CategoryDescription"]);
                        category.IsActive = Convert.ToBoolean(reader["IsActive"]);
                    }
                    connect_selectcategory.Close();
                }
            }
            return View(category);
        }

        [HttpPost]
        public ActionResult EditCategory(CategoryModel category)
        {
            using (SqlConnection connect_edit = new SqlConnection(strConnect))
            {
                if (connect_edit.State != ConnectionState.Open)
                {
                    connect_edit.Open();
                }
                if (category.CategoryID != 0)
                {
                    SqlCommand update_category = new SqlCommand("[dbo].[Training_editCategory]", connect_edit);
                    update_category.CommandType = CommandType.StoredProcedure;
                    update_category.Parameters.AddWithValue("@CategoryID", category.CategoryID);
                    update_category.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                    update_category.Parameters.AddWithValue("@CategoryDescription", category.CategoryDescription);
                    update_category.Parameters.AddWithValue("@IsActive", category.IsActive);
                    int result = update_category.ExecuteNonQuery();
                    if (result > 0)
                    {
                        ViewBag.Message_CategoryUpdate = "category updated.";
                    }
                }
                connect_edit.Close();
            }
            return View(category);
        }
    }
}