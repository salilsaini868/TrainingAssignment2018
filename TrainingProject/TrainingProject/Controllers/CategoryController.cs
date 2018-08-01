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
        public ActionResult Detail(int? id)
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
            return View("InsertCategory", category);
        }

        [HttpPost]
        public ActionResult InsertCategory(CategoryModel category)
        {

            using (SqlConnection connect_category = new SqlConnection(strConnect))
            {
                if (connect_category.State != ConnectionState.Open)
                {
                    connect_category.Open();
                }
                if (category.CategoryID == 0)
                {
                    SqlCommand add_category = new SqlCommand("[dbo].[Training_addCategory]", connect_category);
                    add_category.CommandType = CommandType.StoredProcedure;
                    add_category.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                    add_category.Parameters.AddWithValue("@CatergoryDescription", category.CategoryDescription);
                    add_category.Parameters.AddWithValue("@IsActive", category.IsActive);
                    int result = add_category.ExecuteNonQuery();
                    if (result > 0)
                    {
                        TempData["Message_CategoryInsert"] = "category added.";
                    }
                }
                else
                {
                    SqlCommand update_category = new SqlCommand("[dbo].[Training_editCategory]", connect_category);
                    update_category.CommandType = CommandType.StoredProcedure;
                    update_category.Parameters.AddWithValue("@CategoryID", category.CategoryID);
                    update_category.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                    update_category.Parameters.AddWithValue("@CategoryDescription", category.CategoryDescription);
                    update_category.Parameters.AddWithValue("@IsActive", category.IsActive);
                    int result_update = update_category.ExecuteNonQuery();
                    if (result_update > 0)
                    {
                        TempData["Message_CategoryUpdate"] = "category updated.";
                    }
                }
                connect_category.Close();
            }
            return RedirectToAction("Detail");
        }

        public ActionResult Listing(FormCollection coll)
        {
            DataTable dataset = new DataTable();
            using (SqlConnection connect_listview = new SqlConnection(strConnect))
            {
                if (connect_listview.State != ConnectionState.Open)
                {
                    connect_listview.Open();
                }
                string strSearch = coll["txtSearch"];
                DataTable searchResult = new DataTable();
                if (!string.IsNullOrEmpty(strSearch))
                {
                    ViewBag.searchQuery = strSearch;
                    SqlCommand cmd_search = new SqlCommand("Training_searchCategory", connect_listview);
                    cmd_search.CommandType = CommandType.StoredProcedure;
                    cmd_search.Parameters.AddWithValue("@search", strSearch);
                    SqlDataAdapter adapter2 = new SqlDataAdapter(cmd_search);
                    adapter2.Fill(searchResult);                    
                    return View("ListCategory", searchResult);
                }
                else
                {
                    SqlCommand list_category = new SqlCommand("select * from Training_ProductCategories", connect_listview);
                    SqlDataAdapter adapter = new SqlDataAdapter(list_category);
                    adapter.Fill(dataset);

                }
                connect_listview.Close();               
            }            
            return View("ListCategory", dataset);
        }
    }    
}
