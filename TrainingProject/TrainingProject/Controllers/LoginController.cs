using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using TrainingProject.Models;
using TrainingProject.Helper;
using System.Collections.Generic;

namespace TrainingProject.Controllers
{
    public class LoginController : Controller
    {
        // GET: Default
        ConnectionHelper sqlconnect = new ConnectionHelper();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel lpage)
        {
            List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("username", lpage.Username));
            parameter.Add(new KeyValuePair<string, object>("password", lpage.Password));
            var reader = sqlconnect.CreateResult(executeType: ExecuteEnum.Detail, query: "Training_selectUser", command: CommandType.StoredProcedure, valuePairs: parameter);

            if (reader.Read())
            {
                lpage.Username = Convert.ToString(reader["Username"]);
                lpage.Password = Convert.ToString(reader["Password"]);
                lpage.UserID = Convert.ToInt32(reader["UserID"]);
                lpage.FirstName = Convert.ToString(reader["FirstName"]);
                lpage.LastName = Convert.ToString(reader["LastName"]);
                Session["user"] = lpage;

                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ViewBag.Message_IncorrectLogin = "Authentication Failed";

            }
            return View();
        }
        public ActionResult LogOut()
        {
            Session["user"] = null;
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }
    }
}




