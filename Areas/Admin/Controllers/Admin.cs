using System.Data;
using ClubApp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ClubApp.Models;
using ClubApp.Areas.Admin.Models;

namespace ClubApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class Admin : Controller
    {
        ClsUtility ClsUtil = new ClsUtility();
        db_Utility util = new db_Utility();
        //string cs = ConfigurationManager.ConnectionStrings["club"].ConnectionString;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string sqlquery = "";
        string message = "";
        string status = "";
        string Statement = "";

        public IActionResult Index()
        {
            return View();
        }
        
        #region UserRegistration

        public IActionResult BindProfileName()
        {
            sqlquery = "select isnull(ProfileId,0) as ProfileId,isnull(ProfileName,'') as ProfileName from Adm_Profile";
            ds = util.BindDropDown(sqlquery);
            List<SelectListItem> Categoryobj = new List<SelectListItem>();
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                Categoryobj.Add(new SelectListItem { Text = dr["ProfileName"].ToString(), Value = dr["ProfileId"].ToString() });
            }
            ViewBag.Category = Categoryobj;
            return View(Categoryobj);
        }

        public IActionResult userRegistration()
        {
            HttpContext.Session.GetString("UserId");
            if (HttpContext.Session.GetString("UserId") != null)
            {
                BindProfileName();
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin", "Master", "Admin");
            }
            
        }


        [HttpPost]
        public IActionResult SaveUser(User modelobj)
        {
            String div = "";
            string msg = util.MultipleTransactions("exec Usp_SaveAdminData '" + JsonConvert.SerializeObject(modelobj) + "'");
            if (msg == "Successfull")
            {
                DataSet ds = util.TableBind("exec Usp_ShowUsers ");
                DataTable dt = ds.Tables[0];
                div = util.BindDiv(dt);
            }
            var Data = new { status = msg, Data = div };
            return Json(Data);
        }

        public JsonResult BindUserDiv()
        {
            String div = "";
            string sql = " exec Usp_ShowUsers ";
            DataSet ds = util.TableBind(sql);
            DataTable dt = ds.Tables[0];
            div = util.BindDiv(dt);

            return Json(div);
        }

        #endregion

        #region Adm_Profile
        public IActionResult Adm_Profile()
        {
            HttpContext.Session.GetString("UserId");
            if (HttpContext.Session.GetString("UserId") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin", "Master", "Admin");
            }
        }




        [HttpPost]
        public JsonResult SaveProfile(Adm_Profile modelobj)
        {
            String div = "";
            string msg = util.MultipleTransactions("exec Usp_SaveAdminData '" + JsonConvert.SerializeObject(modelobj) + "'");
            if (msg == "Successfull")
            {
                DataSet ds = util.TableBind("exec Usp_SelectAdm_profile");
                DataTable dt = ds.Tables[0];
                div = util.BindDiv(dt);
            }
            var Data = new { status = msg, Data = div };
            return Json(Data);
        }

        public JsonResult BindProfileDiv()
        {
            String div = "";
            string sql = " exec Usp_SelectAdm_profile";
            DataSet ds = util.TableBind(sql);
            DataTable dt = ds.Tables[0];
            div = util.BindDiv(dt);

            return Json(div);
        }
        #endregion

        #region MenuMaster
        public IActionResult Mst_Menu()
        {
            HttpContext.Session.GetString("UserId");
            if (HttpContext.Session.GetString("UserId") != null)
            {

                string sqlquery = "select isnull((select max(isnull(PrintOrder,1)) as PrintOrder from Adm_Menu   where isnull(MenuParent,0) = 0),0) + 1 as PrintOrder";
                DataSet ds = util.TableBind(sqlquery);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ViewBag.PrintOrderValue = Convert.ToDecimal(dt.Rows[0]["PrintOrder"]);
                }
                //ViewBag.Adminid = Adminid;
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin", "Master", "Admin");
            }
        }

        [HttpPost]
        public JsonResult ParentMenu(MenuMaster MenuParents)
        {

            DataTable dt = new DataTable();
            DataSet ds = util.TableBind("select isnull((select max(isnull(PrintOrder,1)) as PrintOrder from Adm_Menu   where isnull(MenuParent,0) = " + MenuParents.MenuParent + "),0) + 1 as PrintOrder   ");
            dt = ds.Tables[0];
            List<MenuMaster> MenuList = new List<MenuMaster>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                MenuList.Add(new MenuMaster
                {
                    PrintOrder = Convert.ToDecimal(dr["PrintOrder"])

                });
            }
            return Json(MenuList);


        }



        [HttpPost]
        public IActionResult SaveMenuMaster(MenuMaster modelobj)
        {
            String div = "";

            string msg = util.MultipleTransactions("exec Usp_SaveAdminData '" + JsonConvert.SerializeObject(modelobj) + "'");

            msg = util.MultipleTransactions("exec SP_SetMenuOrder @id = 0");
            if (msg == "Successfull")
            {
                string sql = "exec Sp_ShowMenu";
                DataSet ds = util.TableBind(sql);
                DataTable dt = ds.Tables[0];
                div = util.BindDiv(dt);
            }
            var Data = new { status = msg, Data = div };
            return Json(Data);
        }

        public JsonResult BindBlood()
        {

            string sqlquery = "exec USp_dropdown 'BindBlood', ''";
            DataSet ds = util.TableBind(sqlquery);
            List<SelectListItem> Category = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Category.Add(new SelectListItem { Text = dr["MenuName"].ToString(), Value = dr["MenuId"].ToString() });
            }
            ViewBag.Category = Category;
            return Json(Category);
        }

        public JsonResult BindMenuDiv()
        {
            String div = "";
            string sql = " exec Sp_ShowMenu";
            DataSet ds = util.TableBind(sql);
            DataTable dt = ds.Tables[0];
            div = util.BindDiv(dt);

            return Json(div);
        }

        #endregion

        #region MenuRights
        public IActionResult MenuRights()
        {
            HttpContext.Session.GetString("UserId");
            if (HttpContext.Session.GetString("UserId") != null)
            {
                sqlquery = "exec USp_dropdown 'Profile',''";
                ds = util.BindDropDown(sqlquery);
                List<SelectListItem> Categoryobj = new List<SelectListItem>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Categoryobj.Add(new SelectListItem { Text = dr["ProfileName"].ToString(), Value = dr["ProfileId"].ToString() });
                }
                ViewBag.Websitelist = Categoryobj;
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin", "Master", "Admin");
            }
        }

        [HttpPost]
        public IActionResult SaveMenuRights([FromBody] List<Adm_MenuList> MenuRight)
        {
            string msg = "";
            string querry = "";
            msg = util.MultipleTransactions("exec Usp_SaveAdminData '" + JsonConvert.SerializeObject(MenuRight) + "'");
            //querry = "exec Sp_SaveProfile '" + JsonConvert.SerializeObject(MenuRight) + "'";
            //msg = Util.execQuery(querry, Util.strElect);
            var Data = new { status = msg, Data = "" };
            return Json(Data);
        }

        public JsonResult BindMenuRightsDiv(int ProfileId = 1)
        {
            String div = "";
            string sql = " exec Sp_GetMenuRights " + ProfileId + "";
            DataSet ds = util.TableBind(sql);
            DataTable dt = ds.Tables[0];
            div = util.BindDiv(dt);

            return Json(div);
        }

        #endregion

        //[HttpPost]
        //public JsonResult GetButtonRights(string menuname)
        //{
        //    string SchoolID = HttpContext.Session.GetString("SchoolID");
        //    string ProfileId = HttpContext.Session.GetString("ProfileId");
        //    DataTable dt = util.SelectParticular("exec Sp_GetMenuBtnRights " + ProfileId + ",'" + menuname + "'", 20);
        //    var retdata = "";
        //    if (dt.Rows.Count > 0)
        //    {
        //        retdata = JsonConvert.SerializeObject(dt);
        //    }
        //    //List<ButtonRights> br = new List<ButtonRights>();
        //    //sqlquery = "exec Sp_GetMenuBtnRights @Schoolid=" + SchoolID + ", @ProfileId=" + ProfileId + ",@Menuname='" + menuname + "'";
        //    //dt = util.execQuery(sqlquery);
        //    //if (dt.Rows.Count > 0)
        //    //{
        //    //    for (int i = 0; i < dt.Rows.Count; i++)
        //    //    {
        //    //        br.Add(new ButtonRights
        //    //        {
        //    //            MenuRightsId = Convert.ToInt32(dt.Rows[i]["MenuRightsId"]),
        //    //            menuid = Convert.ToInt32(dt.Rows[i]["MenuId"]),
        //    //            Menuname = dt.Rows[i]["Menuname"].ToString(),
        //    //            FlagView = Convert.ToInt32(dt.Rows[i]["FlagView"]),
        //    //            FlagAdd = Convert.ToInt32(dt.Rows[i]["FlagAdd"]),
        //    //            FlagModify = Convert.ToInt32(dt.Rows[i]["FlagModify"]),
        //    //            FlagDelete = Convert.ToInt32(dt.Rows[i]["FlagDelete"]),
        //    //        });
        //    //    }
        //    //}
        //    return Json(retdata);
        //}

    }
}
