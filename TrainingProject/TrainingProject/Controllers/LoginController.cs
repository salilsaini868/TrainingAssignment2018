using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using TrainingProject.Models;

namespace TrainingProject.Controllers
{
    public class LoginController : Controller
    {
        // GET: Default
        string strConnect = string.Empty;
        public LoginController()
        {
            strConnect = @"Data Source=172.20.21.129; MultipleActiveResultSets=True; Initial Catalog=RHPM; User ID=RHPM; Password=evry@123";
        }
        [HttpGet]
        public ActionResult LoginPage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginPage(LoginModel lpage)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connectLogin = new SqlConnection(strConnect))
                {
                    if (connectLogin.State != ConnectionState.Open)
                    {
                        connectLogin.Open();
                    }
                    SqlCommand cmd_login = new SqlCommand("[dbo].[Training_selectUser]", connectLogin);
                    cmd_login.CommandType = CommandType.StoredProcedure;
                    cmd_login.Parameters.AddWithValue("@username", lpage.Username);
                    cmd_login.Parameters.AddWithValue("@password", lpage.Password);

                    SqlDataReader reader = cmd_login.ExecuteReader();
                    if (reader.Read())
                    {
                        lpage.Username = Convert.ToString(reader["Username"]);
                        lpage.Password = Convert.ToString(reader["Password"]);
                        lpage.UserID = Convert.ToInt32(reader["UserID"]);
                        lpage.FirstName = Convert.ToString(reader["FirstName"]);
                        lpage.LastName = Convert.ToString(reader["LastName"]);
                        Session["user"] = lpage;

                        return RedirectToAction("Listing", "Product");
                    }
                    else
                    {
                        ViewBag.Message_IncorrectLogin = "Authentication Failed";
                    }

                    connectLogin.Close();
                }
            }
            return View();
        }
    }
}
