using ClubApp;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace DarjeelingClubApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReportsController : Controller
    {
        ClsUtility ClsUtil = new ClsUtility();
        db_Utility util = new db_Utility();
        public IActionResult ReminderReports()
        {
            HttpContext.Session.GetString("UserId");
            if (HttpContext.Session.GetString("UserId") != null)
            {
                ViewBag.bindcat = util.PopulateDropDown("select CATEGORYCODE,CATEGORYNAME from TM_CATEGORYMASTER_Cre_Cou", util.Clubstr);
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin", "Master", "Admin");
            }
            return View();
        }


        public JsonResult getreminderreport(int mon, int type, int cat, string mem, int amount)
        {
           string sqlquery = $"exec getreminderreport {mon},{type},{cat},'{mem}'";
            DataSet ds = util.TableBind(sqlquery);
            DataTable dt = new DataTable();
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }

            return Json(JsonConvert.SerializeObject(dt));
        }
    }
}
