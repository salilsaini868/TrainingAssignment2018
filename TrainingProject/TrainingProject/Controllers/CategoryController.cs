using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using TrainingProject.Models;

namespace TrainingProject.Controllers
{
    [RedirectToLogin]
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
                        category.CreatedBy = Convert.ToInt32(reader["CreatedBy"]);
                        category.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                        category.ModifiedBy = reader["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(reader["ModifiedBy"]) : 0;
                        category.ModifiedDate = reader["ModifiedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ModifiedDate"]) : default(DateTime);

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
                SqlCommand command = new SqlCommand();
                command = new SqlCommand("[dbo].[Training_insertCategory]", connect_category);
                command.CommandType = CommandType.StoredProcedure;

                var userlogin = Session["user"] as LoginModel;
                command.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                command.Parameters.AddWithValue("@CategoryDescription", category.CategoryDescription);
                command.Parameters.AddWithValue("@IsActive", category.IsActive);
                if (category.CategoryID == 0)
                {
                    category.CreatedUser = userlogin.Username;
                    command.Parameters.AddWithValue("@CreatedBy", userlogin.UserID);
                    command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                    int result = command.ExecuteNonQuery();
                    TempData["Message_CategoryInsert"] = "category added.";
                }
                else
                {
                    command.Parameters.AddWithValue("@CategoryID", category.CategoryID);
                    command.Parameters.AddWithValue("@ModifiedBy", userlogin.UserID);
                    command.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);

                    int result = command.ExecuteNonQuery();
                    TempData["Message_CategoryUpdate"] = "category updated.";
                }
                connect_category.Close();
            }
            return View("insertCategory", category);
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
                ViewBag.searchQuery = strSearch;
                SqlCommand cmd_search = new SqlCommand("Training_searchCategory", connect_listview);
                cmd_search.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(strSearch))
                {
                    cmd_search.Parameters.AddWithValue("@search", strSearch);
                }
                SqlDataAdapter adapter_search = new SqlDataAdapter(cmd_search);
                adapter_search.Fill(searchResult);
                var count = searchResult.Rows.Count;
                if (count == 0)
                {
                    TempData["nodata"] = "No records found.";
                }
                connect_listview.Close();
                return View("ListCategory", searchResult);
            }
        }

        public ActionResult Delete(int ID)
        {
            using (SqlConnection connect_delete = new SqlConnection(strConnect))
            {
                if (connect_delete.State != ConnectionState.Open)
                {
                    connect_delete.Open();
                }
                SqlCommand cmd_delete = new SqlCommand("Training_deleteCategory", connect_delete);
                cmd_delete.CommandType = CommandType.StoredProcedure;
                cmd_delete.Parameters.AddWithValue("@CategoryID", ID);
                int del_user = cmd_delete.ExecuteNonQuery();
                connect_delete.Close();
            }
            return RedirectToAction("Listing");
        }
    }
}