﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingProject.Models;
using TrainingProject.Helper;

namespace TrainingProject.Controllers
{
    [RedirectToLogin]
    public class CategoryController : Controller
    {
        // GET: Category                
        ConnectionHelper sqlconnect = new ConnectionHelper();

        [HttpGet]
        public ActionResult Detail(int? id)
        {
            CategoryModel category = new CategoryModel();
            if (id != null)
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                parameter.Add(new KeyValuePair<string, object>("CategoryId", id));
                var command_select = sqlconnect.CreateResult(executeType: ExecuteEnum.Detail, query: "Training_selectCategory", command: CommandType.StoredProcedure, valuePairs: parameter);
                command_select.Read();
                {
                    category.CategoryID = Convert.ToInt32(command_select["CategoryID"]);
                    category.CategoryName = Convert.ToString(command_select["CategoryName"]);
                    category.CategoryDescription = Convert.ToString(command_select["CategoryDescription"]);
                    category.IsActive = Convert.ToBoolean(command_select["IsActive"]);
                    category.CreatedBy = Convert.ToInt32(command_select["CreatedBy"]);
                    category.CreatedDate = Convert.ToDateTime(command_select["CreatedDate"]);
                    
                    if (command_select["ModifiedBy"] is DBNull)
                    { category.ModifiedBy = 0; }
                    else
                    { category.ModifiedBy = Convert.ToInt32(command_select["ModifiedBy"]); }

                    if (command_select["ModifiedDate"] is DBNull)
                    { category.ModifiedDate = default(DateTime); }
                    else
                    { category.ModifiedDate = Convert.ToDateTime(command_select["ModifiedDate"]); }
                }
            }
            return View("InsertCategory", category);
        }

        [HttpPost]
        public ActionResult InsertCategory(CategoryModel category)
        {
            var userlogin = Session["user"] as LoginModel;
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("CategoryName", category.CategoryName));
            parameter.Add(new KeyValuePair<string, object>("CategoryDescription", category.CategoryDescription));
            parameter.Add(new KeyValuePair<string, object>("IsActive", category.IsActive));

            if (category.CategoryID == 0)
            {
                category.CreatedUser = userlogin.Username;
                parameter.Add(new KeyValuePair<string, object>("CreatedBy", userlogin.UserID));
                parameter.Add(new KeyValuePair<string, object>("CreatedDate", DateTime.Now));
            }
            else
            {
                parameter.Add(new KeyValuePair<string, object>("CategoryID", category.CategoryID));
                parameter.Add(new KeyValuePair<string, object>("ModifiedBy", userlogin.UserID));
                parameter.Add(new KeyValuePair<string, object>("ModifiedDate", DateTime.Now));
            }
            var command_insert = sqlconnect.CreateResult(executeType: ExecuteEnum.Insert, query: "Training_insertCategory", command: CommandType.StoredProcedure, valuePairs: parameter);
            if (category.CategoryID == 0)
            {
                TempData["Message_CategoryInsert"] = "category added.";
                return RedirectToAction("Detail");
            }
            else
            {
                TempData["Message_CategoryUpdate"] = "category updated.";                
            }
            int result = command_insert;
            return RedirectToAction("Detail",  new { id = category.CategoryID });
        }

        public ActionResult Listing(FormCollection coll)
        {
            string[] strSearch = new string[1];
            strSearch[0] = coll["txtSearch"];
            string searchView = coll["txtSearch"];
            ViewBag.searchQuery = searchView;
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("search", strSearch[0]));
            var cmd_search = sqlconnect.CreateResult(executeType: ExecuteEnum.List, query: "Training_searchCategory", command: CommandType.StoredProcedure, valuePairs: parameter);
            var count = cmd_search.Rows.Count;
            if (count == 0)
            {
                TempData["nodata"] = "No records found.";
            }
            return View("ListCategory", cmd_search);
        }

        public ActionResult Delete(int ID)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("CategoryID", ID));
            var cmd_delete = sqlconnect.CreateResult(executeType: ExecuteEnum.Delete, query: "Training_deleteCategory", command: CommandType.StoredProcedure, valuePairs: parameter);
            int del_user = cmd_delete;

            return RedirectToAction("Listing");
        }
    }

}