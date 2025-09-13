using ClubApp;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ClubApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MasterController : Controller
    {
        ClsUtility ClsUtil = new ClsUtility();
        db_Utility util = new db_Utility();
        //string cs = ConfigurationManager.ConnectionStrings["club"].ConnectionString;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string sqlquery = "";
        
        public IActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdminLogin(string username, string password)
        {
            sqlquery = "select * from ADMIN_LOGIN where UserName ='" + username + "' and UserPassword ='" + password + "'";
            ds = util.TableBind(sqlquery);
            if (ds.Tables[0].Rows.Count > 0)
            {
                HttpContext.Session.SetString("UserId", ds.Tables[0].Rows[0]["UserId"].ToString());
                HttpContext.Session.SetString("ProfileId", ds.Tables[0].Rows[0]["ProfileId"].ToString());
                HttpContext.Session.SetString("FullName", ds.Tables[0].Rows[0]["FullName"].ToString());
                HttpContext.Session.SetString("MobileNo", ds.Tables[0].Rows[0]["MobileNo"].ToString());
                HttpContext.Session.SetString("UserName", ds.Tables[0].Rows[0]["UserName"].ToString());
                HttpContext.Session.SetString("UserPassword", ds.Tables[0].Rows[0]["UserPassword"].ToString());
                return RedirectToAction("AdminLogin", "clubmaster", "Admin");
            }
            else
            {
                ViewBag.msg = "Your Username or Password is incorrect !!";
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("AdminLogin");
        }

    }
}
