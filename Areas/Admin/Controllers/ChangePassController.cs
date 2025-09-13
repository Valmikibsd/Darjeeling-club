using ClubApp;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClubApp.Areas.Admin.Models;
using System.Data;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace ClubApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChangePassController : Controller
    {
        ClsUtility ClsUtil = new ClsUtility();
        db_Utility util = new db_Utility();
        //string cs = ConfigurationManager.ConnectionStrings["club"].ConnectionString;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string sqlquery = "";
        string message = "";
        string status = "";

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public JsonResult IUChangePassword(string NewPassword, string CurrentPassword, string ConfirmPassword)
        {
            HttpContext.Session.GetString("UserId");
            var UserPassword = HttpContext.Session.GetString("UserPassword");
            if (UserPassword == CurrentPassword)
            {
                if (ConfirmPassword == NewPassword)
                {
                    sqlquery = "update ADMIN_LOGIN set UserPassword='" + NewPassword + "' where UserId='" + HttpContext.Session.GetString("UserId") + "'";
                    status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        message = "Password Changed";
                    }
                }
                else
                {
                    message = "Confirm Password do not match";
                }
            }
            else
            {
                message = "Current Password not match";
            }
            var data = new { message = message };
            return Json(data);
        }


    }
}
