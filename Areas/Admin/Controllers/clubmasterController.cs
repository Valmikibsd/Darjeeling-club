using ClubApp;
using ClubApp.Areas.Admin.Models;
using DarjeelingClubApp.Areas.Admin.Models;
using DarjeelingClubApp.Areas.Admin.Models.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Newtonsoft.Json;
using NuGet.Packaging.Signing;
using Rotativa.AspNetCore;
using System;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClubApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class clubmasterController : Controller
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

        private readonly IConfiguration _config;

        public clubmasterController(IConfiguration config)
        {
            _config = config;
        }


        public IActionResult AdminLogin()
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

        #region ContactForm
        public IActionResult ContactForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IUContactForm(ContactForm cat)
        {


            Statement = "INSERT";
            sqlquery = "exec Sp_ContactForm '" + Statement + "'," + cat.id + ",'" + cat.FullName + "','" + cat.MobileNo + "','" + cat.Email + "','" + cat.Msg + "'";
            status = util.MultipleTransactions(sqlquery);
            if (status == "Successfull")
            {
                message = "Contact Form added.";
            }
            else
            {
                message = "Contact Form not added.";
            }
            var Data = new { message = message, id = cat.id };
            return Json(Data);
        }

        #endregion

        #region CancelReport
        public ActionResult BookingBind()
        {
            sqlquery = "select isnull(ID,0)as ID,isnull(VenueName,'')as VenueName from tblVenueMaster";
            ds = util.BindDropDown(sqlquery);
            List<SelectListItem> Categoryobj = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Categoryobj.Add(new SelectListItem { Text = dr["VenueName"].ToString(), Value = dr["ID"].ToString() });
            }
            ViewBag.Categoryobj = Categoryobj;
            return View(Categoryobj);
        }
        public ActionResult BookingMemberBind()
        {
            sqlquery = "select isnull(Membershipno,'') as Membershipno,isnull(FirstName+' '+MiddleName+' '+LastName,'') as MemberName from TM_MemberDetail";
            ds = util.BindDropDown(sqlquery);
            List<SelectListItem> Categoryobj = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Categoryobj.Add(new SelectListItem { Text = dr["MemberName"].ToString(), Value = dr["Membershipno"].ToString() });
            }
            ViewBag.Categoryvenue = Categoryobj;
            return View(Categoryobj);
        }
        public ActionResult CancelBooking()
        {
            HttpContext.Session.GetString("UserId");
            if (HttpContext.Session.GetString("UserId") != null)
            {
                BookingBind();
                BookingMemberBind();
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin", "Master", "Admin");
            }
        }

        [HttpGet]
        public JsonResult ShowCancelBooking(int venueid, int venuetypeid, string memberid, string VENUE_DATE1, string VENUE_DATE2)
        {
            List<TranVenueCancel> Category = new List<TranVenueCancel>();
            sqlquery = "exec Sp_Booking_Cancel " + venueid + ", " + venuetypeid + ", '" + memberid + "', '" + VENUE_DATE1 + "', '" + VENUE_DATE2 + "'";
            DataSet ds = util.TableBind(sqlquery);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Category.Add(new TranVenueCancel
                {
                    id = Convert.ToInt32(dr["id"]),
                    VenueName = Convert.ToString(dr["VenueName"]),
                    NAME = Convert.ToString(dr["NAME"]),
                    MemberName = Convert.ToString(dr["MemberName"]),
                    VENUE_DATE = Convert.ToDateTime(dr["VENUE_DATE"].ToString()).ToString("dd/MM/yyyy"),
                    cgst = Convert.ToDouble(dr["cgst"]),
                    sgst = Convert.ToDouble(dr["sgst"]),
                    Payname = Convert.ToString(dr["Payname"]),
                    AC_Charge = Convert.ToDouble(dr["AC_Charge"]),
                    Maintenance_CHGS = Convert.ToDouble(dr["Maintenance_CHGS"]),
                    rental = Convert.ToDouble(dr["rental"]),
                    curr_date = Convert.ToDateTime(dr["curr_date"]),
                    caldate = Convert.ToDateTime(dr["caldate"]),
                    partyno = Convert.ToString(dr["partyno"]),
                    SecurityCharge = Convert.ToDouble(dr["SecurityCharge"]),
                    RefrenceNo = Convert.ToString(dr["RefrenceNo"]),
                    CheckDate = Convert.ToDateTime(dr["CheckDate"]),
                    CheckNo = Convert.ToString(dr["CheckNo"]),
                    Totalvenueamt = Convert.ToDouble(dr["Totalvenueamt"]),
                    totalvenuegst = Convert.ToDouble(dr["totalvenuegst"]),
                    orderid = Convert.ToString(dr["orderid"]),
                    flg = Convert.ToInt32(dr["flg"]),
                    CANCELFLAG = Convert.ToInt32(dr["CANCELFLAG"]),
                });
            }
            return Json(Category);
        }

        public JsonResult CancelBookingVenueBind(int id)
        {
            sqlquery = "select isnull(a.NAME,'')as NAME,isnull(a.id,0) as id from TYPE_VENUE as a join tblVenueMaster as b on a.venuid=b.ID  where a.venuid=" + id + "";
            DataSet ds = util.BindDropDown(sqlquery);
            List<SelectListItem> SubCategory = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                SubCategory.Add(new SelectListItem { Text = dr["NAME"].ToString(), Value = dr["id"].ToString() });
            }
            ViewBag.SubCategory = SubCategory;
            return Json(SubCategory);
        }
        #endregion

        #region MemberBillReport
        public ActionResult MemberBillReport()
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

        [HttpGet]
        public JsonResult ShowMemberBillReport(string FromDate, string ToDate)
        {
            List<MemberBillReport> Category = new List<MemberBillReport>();
            sqlquery = "exec Sp_tbl_MemberBill_Main_report '" + FromDate + "', '" + ToDate + "'";
            DataSet ds = util.TableBind(sqlquery);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Category.Add(new MemberBillReport
                {
                    BillDate = Convert.ToDateTime(dr["BillDate"]).ToString("dd/MM/yyyy"),
                    BillNo = Convert.ToString(dr["BillNo"]),
                    MemberID = Convert.ToString(dr["MemberID"]),
                    taxamount = Convert.ToDouble(dr["taxamount"]),
                    netamount = Convert.ToDouble(dr["netamount"]),
                    OpeningBal = Convert.ToDouble(dr["OpeningBal"]),
                    paymentno = Convert.ToString(dr["paymentno"]),
                    IsBilled = Convert.ToInt32(dr["IsBilled"]),
                    p_entrydate = Convert.ToDateTime(dr["p_entrydate"]).ToString("dd/MM/yyyy"),
                });
            }
            return Json(Category);
        }
        #endregion

        #region VenueAvailability

        public ActionResult VenueAvlbleReportBind()
        {
            sqlquery = "select isnull(ID,0)as ID,isnull(VenueName,'')as VenueName from tblVenueMaster";
            ds = util.BindDropDown(sqlquery);
            List<SelectListItem> Categoryobj = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Categoryobj.Add(new SelectListItem { Text = dr["VenueName"].ToString(), Value = dr["ID"].ToString() });
            }
            ViewBag.Categoryobj = Categoryobj;
            return View(Categoryobj);
        }


        public IActionResult VenueAvailability()
        {
            HttpContext.Session.GetString("UserId");
            if (HttpContext.Session.GetString("UserId") != null)
            {
                VenueAvlbleReportBind();
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin", "Master", "Admin");
            }
        }
        [HttpGet]
        public JsonResult ShowVenueAvailability(int venueid, string VENUE_DATE1, string VENUE_DATE2)
        {
            List<AvailableVenueReport> Category = new List<AvailableVenueReport>();
            sqlquery = "exec Sp_Available_venue  " + venueid + ",'" + VENUE_DATE1 + "', '" + VENUE_DATE2 + "'";
            DataSet ds = util.TableBind(sqlquery);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Category.Add(new AvailableVenueReport
                {
                    VenueName = Convert.ToString(dr["VenueName"]),
                    NAME = Convert.ToString(dr["NAME"]),
                });
            }
            return Json(Category);
        }

        //public JsonResult VenueTypeAvlbleReportBind(int id)
        //{
        //    sqlquery = "select isnull(a.NAME,'')as NAME,isnull(a.id,0) as id from TYPE_VENUE as a join tblVenueMaster as b on a.venuid=b.ID  where a.venuid=" + id + "";
        //    DataSet ds = util.BindDropDown(sqlquery);
        //    List<SelectListItem> SubCategory = new List<SelectListItem>();
        //    foreach (DataRow dr in ds.Tables[0].Rows)
        //    {
        //        SubCategory.Add(new SelectListItem { Text = dr["NAME"].ToString(), Value = dr["id"].ToString() });
        //    }
        //    ViewBag.SubCategory = SubCategory;
        //    return Json(SubCategory);
        //}
        #endregion

        #region BookingReport
        public ActionResult VenueReportBind()
        {
            sqlquery = "select isnull(ID,0)as ID,isnull(VenueName,'')as VenueName from tblVenueMaster";
            ds = util.BindDropDown(sqlquery);
            List<SelectListItem> Categoryobj = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Categoryobj.Add(new SelectListItem { Text = dr["VenueName"].ToString(), Value = dr["ID"].ToString() });
            }
            ViewBag.Categoryobj = Categoryobj;
            return View(Categoryobj);
        }
        public ActionResult VenueMemberBind()
        {
            sqlquery = "select isnull(Membershipno,'') as Membershipno,isnull(FirstName+' '+MiddleName+' '+LastName,'') as MemberName from TM_MemberDetail order by FirstName";
            ds = util.BindDropDown(sqlquery);
            List<SelectListItem> Categoryobj = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Categoryobj.Add(new SelectListItem { Text = dr["MemberName"].ToString(), Value = dr["Membershipno"].ToString() });
            }
            ViewBag.Categoryvenue = Categoryobj;
            return View(Categoryobj);
        }
        public ActionResult VenueReport()
        {
            HttpContext.Session.GetString("UserId");
            if (HttpContext.Session.GetString("UserId") != null)
            {
                VenueReportBind();
                VenueMemberBind();
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin", "Master", "Admin");
            }
        }

        [HttpGet]
        public JsonResult ShowVenueReport(int venueid, int venuetypeid, string memberid, string VENUE_DATE1, string VENUE_DATE2)
        {
            List<VenueReport> Category = new List<VenueReport>();
            sqlquery = "exec Sp_tran_venue_report  " + venueid + ", " + venuetypeid + ", '" + memberid + "', '" + VENUE_DATE1 + "', '" + VENUE_DATE2 + "'";
            DataSet ds = util.TableBind(sqlquery);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Category.Add(new VenueReport
                {
                    VenueName = Convert.ToString(dr["VenueName"]),
                    NAME = Convert.ToString(dr["NAME"]),
                    MemberName = Convert.ToString(dr["MemberName"]),
                    VENUE_DATE = Convert.ToDateTime(dr["VENUE_DATE"].ToString()).ToString("dd/MM/yyyy"),
                });
            }
            return Json(Category);
        }

        public JsonResult BindBookingVenueReport(int id)
        {
            sqlquery = "select isnull(a.NAME,'')as NAME,isnull(a.id,0) as id from TYPE_VENUE as a join tblVenueMaster as b on a.venuid=b.ID  where a.venuid=" + id + "";
            DataSet ds = util.BindDropDown(sqlquery);
            List<SelectListItem> SubCategory = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                SubCategory.Add(new SelectListItem { Text = dr["NAME"].ToString(), Value = dr["id"].ToString() });
            }
            ViewBag.SubCategory = SubCategory;
            return Json(SubCategory);
        }
        #endregion

        #region TranVenue
        public ActionResult VenueBind1()
        {
            sqlquery = "select isnull(ID,0)as ID,isnull(VenueName,'')as VenueName from tblVenueMaster";
            ds = util.BindDropDown(sqlquery);
            List<SelectListItem> Categoryobj = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Categoryobj.Add(new SelectListItem { Text = dr["VenueName"].ToString(), Value = dr["ID"].ToString() });
            }
            ViewBag.Categoryobj = Categoryobj;
            return View(Categoryobj);
        }

        public ActionResult TranVenueBind()
        {
            sqlquery = "select isnull(Membershipno,'') as Membershipno,isnull(FirstName+' '+MiddleName+' '+LastName+' ('+Membershipno+')','') as MemberName from TM_MemberDetail order by FirstName";
            ds = util.BindDropDown(sqlquery);
            List<SelectListItem> Categoryobj = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Categoryobj.Add(new SelectListItem { Text = dr["MemberName"].ToString(), Value = dr["Membershipno"].ToString() });
            }
            ViewBag.Categoryvenue = Categoryobj;
            return View(Categoryobj);
        }
        public ActionResult PaymentmodeBind()
        {
            sqlquery = "select isnull(Payid,0) as Payid,isnull(Payname,'') as Payname from tblpaymentmode";
            ds = util.BindDropDown(sqlquery);
            List<SelectListItem> Categoryobj = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Categoryobj.Add(new SelectListItem { Text = dr["Payname"].ToString(), Value = dr["Payid"].ToString() });
            }
            ViewBag.Categorypayment = Categoryobj;
            return View(Categoryobj);
        }
        public ActionResult TranVenue()
        {
            HttpContext.Session.GetString("UserId");
            if (HttpContext.Session.GetString("UserId") != null)
            {
                VenueBind1();
                TranVenueBind();
                PaymentmodeBind();
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin", "Master", "Admin");
            }
        }

        [HttpPost]
        public JsonResult IUTranVenue(TranVenue cat)
        {
            string id = Request.Form["id"].ToString();
            string venueid = Request.Form["venueid"].ToString();
            string venuetypeid = Request.Form["venuetypeid"].ToString();
            string memberid = Request.Form["memberid"].ToString();
            string VENUE_DATE = Request.Form["VENUE_DATE"].ToString();
            string PayMode = Request.Form["PayMode"].ToString();
            string RefrenceNo = Request.Form["RefrenceNo"].ToString();
            string CheckDate = Request.Form["CheckDate"].ToString();
            string CheckNo = Request.Form["CheckNo"].ToString();
            string orderid = DateTime.Now.ToString("ddMMyyyyHHmmss");
            string CANCELFLAG = "";
            string flg = "1";
            string pay_status = "1";
            string curr_date = "";
            string caldate = "";
            string cgst = "";
            string sgst = "";
            string rental = "";
            DateTime t = DateTime.Now;
            DateTime a = DateTime.Now.AddDays(90);
            System.DateTime s = Convert.ToDateTime(VENUE_DATE);
            Guid UniqueID = Guid.NewGuid();
            string partyno = DateTime.Now.ToString("ddMMyyyyHHmmss");
            DataTable dts = util.execQuery("select *  from TYPE_VENUE t, [dbo].[tblVenueMaster] ts where t.venuid=ts.ID and t.id=" + venuetypeid + " and venuid=" + venueid + "");
            rental = dts.Rows[0]["RateForMember"].ToString();
            string AC_Charge = dts.Rows[0]["AC_Charge"].ToString();
            string Maintenance_CHGS = dts.Rows[0]["Maintenance_CHGS"].ToString();
            string SecurityCharge = dts.Rows[0]["SecurityCharge"].ToString();
            cgst = Math.Round((((Convert.ToDouble(string.IsNullOrEmpty(rental) ? "0" : rental) + Convert.ToDouble(string.IsNullOrEmpty(AC_Charge) ? "0" : AC_Charge) + Convert.ToDouble(string.IsNullOrEmpty(Maintenance_CHGS) ? "0" : Maintenance_CHGS)) * 9) / 100), 0).ToString();
            sgst = Math.Round((((Convert.ToDouble(string.IsNullOrEmpty(rental) ? "0" : rental) + Convert.ToDouble(string.IsNullOrEmpty(AC_Charge) ? "0" : AC_Charge) + Convert.ToDouble(string.IsNullOrEmpty(Maintenance_CHGS) ? "0" : Maintenance_CHGS)) * 9) / 100), 0).ToString();
            string totalgsts = (Convert.ToDouble(cgst) + Convert.ToDouble(cgst)).ToString();
            string totalamts = (Convert.ToDouble(cgst) + Convert.ToDouble(cgst) + Convert.ToDouble(rental)).ToString();
            string totalvenuegst = (Convert.ToDouble(sgst) + Convert.ToDouble(sgst)).ToString();
            string Totalvenueamt = (Convert.ToDouble(sgst) + Convert.ToDouble(sgst) + Convert.ToDouble(rental)).ToString();
            if (cat.id == 0)
            {
                sqlquery = "select * from tran_venue where venueid='" + venueid + "' and VENUE_DATE='" + VENUE_DATE + "' and venuetypeid='" + venuetypeid + "' and CANCELFLAG='" + CANCELFLAG + "'";
                DataSet ds = util.TableBind(sqlquery);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    message = "This Booking is already exit";
                }
                else
                {
                    if (s > t && a > s)
                    {
                        Statement = "INSERT";
                        sqlquery = "exec Sp_tran_venue '" + Statement + "'," + id + "," + venueid + "," + venuetypeid + ",'" + memberid + "','" + VENUE_DATE + "','" + cgst + "','" + sgst + "'," + pay_status + ",'" + AC_Charge + "','" + Maintenance_CHGS + "','" + rental + "','" + curr_date + "','" + caldate + "','" + partyno + "','" + SecurityCharge + "','" + RefrenceNo + "','" + CheckDate + "','" + CheckNo + "','" + Totalvenueamt + "','" + totalvenuegst + "','" + orderid + "','" + flg + "','" + CANCELFLAG + "'," + PayMode + "";
                        status = util.MultipleTransactions(sqlquery);
                        if (status == "Successfull")
                        {
                            message = "Booking added";
                        }
                        else
                        {
                            message = "Booking not added";
                        }
                    }
                    else
                    {
                        message = "Please enter valid date for booking";
                    }
                }
            }
            else
            {

                Statement = "UPDATE";
                sqlquery = "exec Sp_tran_venue'" + Statement + "'," + id + "," + venueid + "," + venuetypeid + ",'" + memberid + "','" + VENUE_DATE + "','" + cgst + "','" + sgst + "'," + pay_status + ",'" + AC_Charge + "','" + Maintenance_CHGS + "','" + rental + "','" + curr_date + "','" + caldate + "','" + partyno + "','" + SecurityCharge + "','" + RefrenceNo + "','" + CheckDate + "','" + CheckNo + "','" + Totalvenueamt + "','" + totalvenuegst + "','" + orderid + "','" + flg + "','" + CANCELFLAG + "'," + PayMode + "";
                status = util.MultipleTransactions(sqlquery);
                if (status == "Successfull")
                {
                    message = "Booking update";
                }
                else
                {
                    message = "Booking not update";
                }
            }
            var Data = new { message = message, id = id };
            return Json(Data);
        }

        [HttpPost]
        public JsonResult IUCancelBooking(TranVenue cat)
        {
            string id = Request.Form["id"].ToString();
            string venueid = Request.Form["venueid"].ToString();
            string venuetypeid = Request.Form["venuetypeid"].ToString();
            string memberid = Request.Form["memberid"].ToString();
            string VENUE_DATE = Request.Form["VENUE_DATE"].ToString();
            string PayMode = Request.Form["PayMode"].ToString();
            string RefrenceNo = Request.Form["RefrenceNo"].ToString();
            string CheckDate = Request.Form["CheckDate"].ToString();
            string CheckNo = Request.Form["CheckNo"].ToString();
            string orderid = DateTime.Now.ToString("HHmmss");
            string CANCELFLAG = "";
            string flg = "1";
            string curr_date = "";
            string caldate = "";
            string cgst = "";
            string sgst = "";
            string rental = "";
            Guid UniqueID = Guid.NewGuid();
            string partyno = DateTime.Now.ToString("ddMMyyyyHHmmss");
            DataTable dts = util.execQuery("select *  from TYPE_VENUE t, [dbo].[tblVenueMaster] ts where t.venuid=ts.ID and t.id=" + venuetypeid + " and venuid=" + venueid + "");
            rental = dts.Rows[0]["RateForMember"].ToString();
            string AC_Charge = dts.Rows[0]["AC_Charge"].ToString();
            string Maintenance_CHGS = dts.Rows[0]["Maintenance_CHGS"].ToString();
            string SecurityCharge = dts.Rows[0]["SecurityCharge"].ToString();
            cgst = Math.Round((((Convert.ToDouble(string.IsNullOrEmpty(rental) ? "0" : rental) + Convert.ToDouble(string.IsNullOrEmpty(AC_Charge) ? "0" : AC_Charge) + Convert.ToDouble(string.IsNullOrEmpty(Maintenance_CHGS) ? "0" : Maintenance_CHGS)) * 9) / 100), 0).ToString();
            sgst = Math.Round((((Convert.ToDouble(string.IsNullOrEmpty(rental) ? "0" : rental) + Convert.ToDouble(string.IsNullOrEmpty(AC_Charge) ? "0" : AC_Charge) + Convert.ToDouble(string.IsNullOrEmpty(Maintenance_CHGS) ? "0" : Maintenance_CHGS)) * 9) / 100), 0).ToString();
            string totalgsts = (Convert.ToDouble(cgst) + Convert.ToDouble(cgst)).ToString();
            string totalamts = (Convert.ToDouble(cgst) + Convert.ToDouble(cgst) + Convert.ToDouble(rental)).ToString();
            string totalvenuegst = (Convert.ToDouble(sgst) + Convert.ToDouble(sgst)).ToString();
            string Totalvenueamt = (Convert.ToDouble(sgst) + Convert.ToDouble(sgst) + Convert.ToDouble(rental)).ToString();
            double venvueamount = 0;
            double menuamounts = 0;
            string orinalefund = "0";
            DateTime f = DateTime.Now;
            System.DateTime s = Convert.ToDateTime(VENUE_DATE);
            if (f <= s)
            {
                String diff2 = Math.Round((s - f).TotalDays, 0).ToString();


                DataTable ddy = util.SelectParticular("Cancel_rules", "*", " " + diff2.Replace("-", "") + " between dayfrom and dayto  and ref_flg=0");
                if (ddy.Rows.Count > 0)
                {
                    string dd = ddy.Rows[0]["dedectiontype"].ToString();

                    if (dd == "1")
                    {
                        venvueamount = ((Convert.ToDouble(rental) * Convert.ToDouble(ddy.Rows[0]["Pervenuedec"].ToString())) / 100);
                    }
                    double amsmmd = Math.Round((venvueamount + menuamounts), 0);

                    double cancelsgst = ((amsmmd * 9) / 100);
                    double cancelcgst = ((amsmmd * 9) / 100);

                    string refundamt = Math.Round((amsmmd + cancelsgst + cancelcgst), 0).ToString();
                    orinalefund = (Convert.ToDouble(Totalvenueamt) - Convert.ToDouble(refundamt)).ToString();

                }
                else
                {
                    orinalefund = "0";
                }
            }
            else
            {

                orinalefund = "0";
            }

            if (cat.CANCELFLAG == 0)
            {
                sqlquery = "update tran_venue set calflag=1,CANCELFLAG=1 , calamts=" + orinalefund + ",caluser='" + memberid + "',caldate=GETDATE(),calremark='By Admin',pay_status=3 where id=" + cat.id + "";
                status = util.MultipleTransactions(sqlquery);
                if (status == "Successfull")
                {
                    message = "Booking cancelled";
                }
            }
            var Data = new { message = message, id = id };
            return Json(Data);
        }

        [HttpGet]
        public JsonResult ShowTranVenue()
        {
            List<TranVenue> Category = new List<TranVenue>();
            sqlquery = "exec Sp_tran_venue";
            DataSet ds = util.TableBind(sqlquery);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Category.Add(new TranVenue
                {
                    id = Convert.ToInt32(dr["id"]),
                    VenueName = Convert.ToString(dr["VenueName"]),
                    NAME = Convert.ToString(dr["NAME"]),
                    MemberName = Convert.ToString(dr["MemberName"]),
                    VENUE_DATE = Convert.ToDateTime(dr["VENUE_DATE"].ToString()).ToString("dd/MM/yyyy"),
                    cgst = Convert.ToDouble(dr["cgst"]),
                    sgst = Convert.ToDouble(dr["sgst"]),
                    Payname = Convert.ToString(dr["Payname"]),
                    AC_Charge = Convert.ToDouble(dr["AC_Charge"]),
                    Maintenance_CHGS = Convert.ToDouble(dr["Maintenance_CHGS"]),
                    rental = Convert.ToDouble(dr["rental"]),
                    curr_date = Convert.ToDateTime(dr["curr_date"]),
                    caldate = Convert.ToDateTime(dr["caldate"]),
                    partyno = Convert.ToString(dr["partyno"]),
                    SecurityCharge = Convert.ToDouble(dr["SecurityCharge"]),
                    RefrenceNo = Convert.ToString(dr["RefrenceNo"]),
                    CheckDate = Convert.ToDateTime(dr["CheckDate"]),
                    CheckNo = Convert.ToString(dr["CheckNo"]),
                    Totalvenueamt = Convert.ToDouble(dr["Totalvenueamt"]),
                    totalvenuegst = Convert.ToDouble(dr["totalvenuegst"]),
                    orderid = Convert.ToString(dr["orderid"]),
                    flg = Convert.ToInt32(dr["flg"]),
                    CANCELFLAG = Convert.ToInt32(dr["CANCELFLAG"]),
                    Status = Convert.ToString(dr["Status"]),
                });
            }
            return Json(Category);
        }

        [HttpPost]
        public JsonResult EditTranVenue(int id)
        {
            Statement = "EDIT";
            TranVenue category = new TranVenue();
            sqlquery = "exec Sp_tran_venue '" + Statement + "', " + id + " ";
            DataSet ds = util.TableBind(sqlquery);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                category.venueid = Convert.ToInt32(dr["venueid"]);
                category.venuetypeid = Convert.ToInt32(dr["venuetypeid"]);
                category.memberid = Convert.ToString(dr["memberid"]);
                category.VENUE_DATE = Convert.ToString(dr["VENUE_DATE"]);
                category.id = Convert.ToInt32(dr["id"]);
                category.cgst = Convert.ToDouble(dr["cgst"]);
                category.sgst = Convert.ToDouble(dr["sgst"]);
                category.PayMode = Convert.ToInt32(dr["PayMode"]);
                category.AC_Charge = Convert.ToDouble(dr["AC_Charge"]);
                category.Maintenance_CHGS = Convert.ToDouble(dr["Maintenance_CHGS"]);
                category.rental = Convert.ToDouble(dr["rental"]);
                category.curr_date = Convert.ToDateTime(dr["curr_date"]);
                category.caldate = Convert.ToDateTime(dr["caldate"]);
                category.partyno = Convert.ToString(dr["partyno"]);
                category.SecurityCharge = Convert.ToDouble(dr["SecurityCharge"]);
                category.RefrenceNo = Convert.ToString(dr["RefrenceNo"]);
                category.CheckDate = Convert.ToDateTime(dr["CheckDate"]);
                category.CheckNo = Convert.ToString(dr["CheckNo"]);
                category.Totalvenueamt = Convert.ToDouble(dr["Totalvenueamt"]);
                category.totalvenuegst = Convert.ToDouble(dr["totalvenuegst"]);
                category.orderid = Convert.ToString(dr["orderid"]);
                category.flg = Convert.ToInt32(dr["flg"]);
                category.CANCELFLAG = Convert.ToInt32(dr["CANCELFLAG"]);
            }
            return Json(category);
        }

        [HttpPost]
        public JsonResult DeleteTranVenue(int id)
        {
            Statement = "DELETE";
            sqlquery = "exec Sp_tran_venue '" + Statement + "'," + id + "";
            status = util.MultipleTransactions(sqlquery);
            if (status == "Successfull")
            {
                message = "Delete Successfull!!";
            }
            else
            {
                message = "Failed to Delete";
            }
            var Data = new { msg = message };
            return Json(Data);
        }

        public JsonResult TranVenuetypeBind(int id)
        {
            //  sqlquery = "select isnull(a.NAME,'')as NAME,isnull(a.id,0) as id from TYPE_VENUE as a join tblVenueMaster as b on a.venuid=b.ID  where a.venuid=" + id + "";

            sqlquery = "select isnull(a.NAME,'')as NAME,isnull(a.id,0) as id from TYPE_VENUE";
            DataSet ds = util.BindDropDown(sqlquery);
            List<SelectListItem> SubCategory = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                SubCategory.Add(new SelectListItem { Text = dr["NAME"].ToString(), Value = dr["id"].ToString() });
            }
            ViewBag.SubCategory = SubCategory;
            return Json(SubCategory);
        }

        [HttpGet]
        public JsonResult GetMembers(int id, string term)
        {
            
            string sqlquery = $@"
                    SELECT Membershipno AS id,
                           FirstName + ' ' + LastName + '(' + Membershipno + ')' AS NAME
                    FROM TM_MemberDetail
                    WHERE category = {id}
          AND (FirstName LIKE '%{term}%' OR LastName LIKE '%{term}%' OR Membershipno LIKE '%{term}%')
    ";

            DataSet ds = util.BindDropDown(sqlquery);

            List<SelectListItem> SubCategory = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                SubCategory.Add(new SelectListItem
                {
                    Text = dr["NAME"].ToString(),
                    Value = dr["id"].ToString()
                });
            }

            return Json(SubCategory);
        }

        public JsonResult getMember(int id)
        {
            //  sqlquery = "select isnull(a.NAME,'')as NAME,isnull(a.id,0) as id from TYPE_VENUE as a join tblVenueMaster as b on a.venuid=b.ID  where a.venuid=" + id + "";

            sqlquery = $"select Membershipno id,FirstName+' '+LastName+'('+Membershipno+')' as NAME from TM_MemberDetail where category={id}";
            DataSet ds = util.BindDropDown(sqlquery);
            List<SelectListItem> SubCategory = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                SubCategory.Add(new SelectListItem { Text = dr["NAME"].ToString(), Value = dr["id"].ToString() });
            }
            ViewBag.SubCategory = SubCategory;
            return Json(SubCategory);
        }
        public JsonResult RentBind(int id)
        {
            sqlquery = "select isnull(a.RateForMember,0)as RateForMember,isnull(a.ID,0) as ID from tblVenueMaster as a where a.ID=" + id + "";
            DataSet ds = util.TableBind(sqlquery);
            string rental = ds.Tables[0].Rows[0]["RateForMember"].ToString();
            return Json(rental);
        }

        public JsonResult RefenrenceBind(int id)
        {
            sqlquery = "select isnull(a.payname,'')as payname,isnull(a.payid,0) as payid from tblpaymentmode as a where a.payid=" + id + "  ";
            DataSet ds = util.TableBind(sqlquery);
            string RefrenceNo = ds.Tables[0].Rows[0]["payname"].ToString();
            return Json(RefrenceNo);
        }
        public JsonResult CheckDateBind(int id)
        {
            sqlquery = "select isnull(a.payname,'')as payname,isnull(a.payid,0) as payid from tblpaymentmode as a where a.payid=" + id + "  ";
            DataSet ds = util.TableBind(sqlquery);
            string CkeckDate = ds.Tables[0].Rows[0]["payname"].ToString();
            return Json(CkeckDate);
        }
        public JsonResult CheckNoBind(int id)
        {
            sqlquery = "select isnull(a.payname,'')as payname,isnull(a.payid,0) as payid from tblpaymentmode as a where a.payid=" + id + "  ";
            DataSet ds = util.TableBind(sqlquery);
            string CkeckNo = ds.Tables[0].Rows[0]["payname"].ToString();
            return Json(CkeckNo);
        }

        #endregion

        #region TypeVenue
        public ActionResult TypeVenueBind()
        {
            sqlquery = "select isnull(ID,0)as ID,isnull(VenueName,'')as VenueName from tblVenueMaster";
            ds = util.BindDropDown(sqlquery);
            List<SelectListItem> Categoryobj = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Categoryobj.Add(new SelectListItem { Text = dr["VenueName"].ToString(), Value = dr["ID"].ToString() });
            }
            ViewBag.Categoryobj = Categoryobj;
            return View(Categoryobj);
        }

        public ActionResult TypeVenue()
        {
            HttpContext.Session.GetString("UserId");
            if (HttpContext.Session.GetString("UserId") != null)
            {
                TypeVenueBind();
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin", "Master", "Admin");
            }
        }

        [HttpPost]
        public JsonResult IUTypeVenue()
        {
            string id = Request.Form["id"].ToString();
            string venuid = Request.Form["venuid"].ToString();
            string NAME = Request.Form["NAME"].ToString();

            if (id == "0")
            {
                sqlquery = "select * from TYPE_VENUE where venuid='" + venuid + "' and NAME='" + NAME + "'";
                DataSet ds = util.TableBind(sqlquery);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    message = "This Type Venue is already exit";
                }
                else
                {
                    Statement = "INSERT";
                    sqlquery = "exec Sp_type_venue '" + Statement + "'," + id + ",'" + venuid + "','" + NAME + "'";
                    status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        message = "Type Venue added";
                    }
                    else
                    {
                        message = "Type Venue not added";
                    }
                }
            }
            else
            {
                Statement = "UPDATE";
                sqlquery = "exec Sp_type_venue '" + Statement + "'," + id + ",'" + venuid + "','" + NAME + "'";
                status = util.MultipleTransactions(sqlquery);
                if (status == "Successfull")
                {
                    message = "Type Venue update";
                }
                else
                {
                    message = "Type Venue not update";
                }
            }
            var Data = new { message = message, id = id };
            return Json(Data);
        }
        [HttpGet]
        public JsonResult ShowTypeVenue(int venuid)
        {
            List<TypeVenue> Category = new List<TypeVenue>();
            sqlquery = "exec Sp_type_venue 'SELECT'," + venuid + "";
            DataSet ds = util.TableBind(sqlquery);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Category.Add(new TypeVenue
                {
                    id = Convert.ToInt32(dr["id"]),
                    VenueName = Convert.ToString(dr["VenueName"]),
                    NAME = Convert.ToString(dr["NAME"]),
                });
            }
            return Json(Category);
        }
        [HttpPost]
        public JsonResult EditTypeVenue(int id)
        {
            Statement = "EDIT";
            TypeVenue category = new TypeVenue();
            sqlquery = "exec Sp_type_venue '" + Statement + "', " + id + " ";
            DataSet ds = util.TableBind(sqlquery);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                category.id = Convert.ToInt32(dr["id"]);
                category.venuid = Convert.ToInt32(dr["venuid"]);
                category.NAME = Convert.ToString(dr["NAME"]);
            }
            return Json(category);
        }
        [HttpPost]
        public JsonResult DeleteTypeVenue(int id)
        {
            Statement = "DELETE";
            sqlquery = "exec Sp_type_venue '" + Statement + "'," + id + "";
            status = util.MultipleTransactions(sqlquery);
            if (status == "Successfull")
            {
                message = "Delete Successfull!!";
            }
            else
            {
                message = "Failed to Delete";
            }
            var Data = new { msg = message };
            return Json(Data);
        }

        //public JsonResult TypeVenueName(int id)
        //{
        //    sqlquery = "select isnull(a.VenueTypeName,0)as VenueTypeName,isnull(a.id,0) as id from tblvenuetype as a where a.id=" + id + "";           
        //    DataSet ds = util.TableBind(sqlquery);
        //    string NAME = ds.Tables[0].Rows[0]["VenueTypeName"].ToString();
        //    return Json(NAME);
        //}
        #endregion

        #region VenueMaster
        public ActionResult VenueMaster()
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
        public JsonResult IUIVenueMaster()
        {
            string ID = Request.Form["ID"].ToString();
            string VenueName = Request.Form["VenueName"].ToString();
            string ShortName = Request.Form["ShortName"].ToString();
            string RateForMember = Request.Form["RateForMember"].ToString();
            string Description = Request.Form["Description"].ToString();
            string flg = Request.Form["flg"].ToString();
            string nefilname = Request.Form["filenmes"].ToString();
            string folderName = "wwwroot/VenueMaster/";
            string webRootPath = "";
            string filename = "";
            if (flg == "okg")
            {

                nefilname = "";
                IFormFile file = Request.Form.Files[0];
                string extension = Path.GetExtension(file.FileName);
                filename = Path.GetFileNameWithoutExtension(file.FileName);
                webRootPath = filename + DateTime.Now.ToString("yymmssfff") + extension;
                nefilname = webRootPath;
                string newPath = Path.Combine(folderName, webRootPath);
                using (var fileStream = new FileStream(newPath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            if (ID == "0")
            {
                dt = util.SelectParticular("tblVenueMaster", "*", "VenueName ='" + VenueName + "',ShortName='" + ShortName + "' where ");
                if (dt.Rows.Count > 0)
                {
                    message = "This Venue Master is already exit";
                }
                else
                {
                    Statement = "INSERT";
                    sqlquery = "exec Usp_Venue_Master '" + Statement + "'," + ID + ",'" + VenueName + "','" + ShortName + "','" + RateForMember + "','" + webRootPath + "','" + Description + "'";
                    status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        message = "Venue Master added";
                    }
                    else
                    {
                        message = "Venue Master not added";
                    }
                }
            }
            else
            {
                Statement = "UPDATE";
                sqlquery = "exec Usp_Venue_Master '" + Statement + "'," + ID + ",'" + VenueName + "','" + ShortName + "','" + RateForMember + "','" + nefilname + "','" + Description + "'";
                status = util.MultipleTransactions(sqlquery);
                if (status == "Successfull")
                {
                    message = "Venue Master update";
                }
                else
                {
                    message = "Venue Master not update";
                }
            }
            var Data = new { message = message, ID = ID };
            return Json(Data);
        }
        [HttpGet]
        public JsonResult ShowVenueMaster()
        {
            List<VenueMaster> Category = new List<VenueMaster>();
            sqlquery = "exec Usp_Venue_Master";
            DataSet ds = util.TableBind(sqlquery);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Category.Add(new VenueMaster
                {
                    ID = Convert.ToInt32(dr["ID"]),
                    VenueName = Convert.ToString(dr["VenueName"]),
                    ShortName = Convert.ToString(dr["ShortName"]),
                    RateForMember = Convert.ToDouble(dr["RateForMember"]),
                    Photo = Convert.ToString(dr["Photo"]),
                    Description = Convert.ToString(dr["Description"]),
                });
            }
            return Json(Category);
        }
        [HttpPost]
        public JsonResult EditVenueMaster(int ID)
        {
            Statement = "EDIT";
            VenueMaster category = new VenueMaster();
            sqlquery = "exec Usp_Venue_Master '" + Statement + "', " + ID + " ";
            DataSet ds = util.TableBind(sqlquery);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                category.ID = Convert.ToInt32(dr["ID"]);
                category.VenueName = Convert.ToString(dr["VenueName"]);
                category.ShortName = Convert.ToString(dr["ShortName"]);
                category.RateForMember = Convert.ToDouble(dr["RateForMember"]);
                category.Photo = Convert.ToString(dr["Photo"]);
                category.Description = Convert.ToString(dr["Description"]);
            }
            return Json(category);
        }
        [HttpPost]
        public JsonResult DeleteVenueMaster(int ID)
        {
            Statement = "DELETE";
            sqlquery = "exec Usp_Venue_Master '" + Statement + "'," + ID + "";
            status = util.MultipleTransactions(sqlquery);
            if (status == "Successfull")
            {
                message = "Delete Successfull!!";
            }
            else
            {
                message = "Failed to Delete";
            }
            var Data = new { msg = message };
            return Json(Data);
        }

        #endregion

        #region BlockVenue 
        public ActionResult VenueBind()
        {
            sqlquery = "select isnull(ID,0)as ID,isnull(VenueName,'')as VenueName from tblVenueMaster";
            ds = util.BindDropDown(sqlquery);
            List<SelectListItem> Categoryobj = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Categoryobj.Add(new SelectListItem { Text = dr["VenueName"].ToString(), Value = dr["ID"].ToString() });
            }
            ViewBag.Categoryobj = Categoryobj;
            return View(Categoryobj);
        }
        public ActionResult Venue()
        {
            HttpContext.Session.GetString("UserId");
            if (HttpContext.Session.GetString("UserId") != null)
            {
                VenueBind();
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin", "Master", "Admin");
            }
        }

        [HttpPost]
        public JsonResult IUIVenue()
        {
            string Id = Request.Form["Id"].ToString();
            string venueid = Request.Form["venueid"].ToString();
            string venuetypeid = Request.Form["venuetypeid"].ToString();
            string BlockDate = Request.Form["BlockDate"].ToString();
            if (Id == "0")
            {
                dt = util.SelectParticular("TblDateBlock", "*", "venueid ='" + venueid + "' ");
                if (dt.Rows.Count > 0)
                {
                    message = "This Venue is already exit";
                }
                else
                {
                    Statement = "INSERT";
                    sqlquery = "exec Sp_TblDateBlock '" + Statement + "'," + Id + "," + venueid + "," + venuetypeid + ",'" + BlockDate + "'";
                    status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        message = "Venue added";
                    }
                    else
                    {
                        message = "Venue not added";
                    }
                }
            }
            else
            {
                Statement = "UPDATE";
                sqlquery = "exec Sp_TblDateBlock '" + Statement + "'," + Id + "," + venueid + "," + venuetypeid + ",'" + BlockDate + "'";
                status = util.MultipleTransactions(sqlquery);
                if (status == "Successfull")
                {
                    message = "Venue update";
                }
                else
                {
                    message = "Venue not update";
                }
            }
            var Data = new { message = message, id = Id };
            return Json(Data);
        }

        [HttpGet]
        public JsonResult ShowVenue()
        {
            List<Venue> Category = new List<Venue>();
            sqlquery = "exec Sp_TblDateBlock";
            DataSet ds = util.TableBind(sqlquery);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Category.Add(new Venue
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    VenueName = Convert.ToString(dr["VenueName"]),
                    NAME = Convert.ToString(dr["NAME"]),
                    BlockDate = Convert.ToString(dr["BlockDate"]),
                });
            }
            return Json(Category);
        }
        [HttpPost]
        public JsonResult EditVenue(int Id)
        {
            Statement = "EDIT";
            Venue category = new Venue();
            sqlquery = "exec Sp_TblDateBlock '" + Statement + "', " + Id + " ";
            DataSet ds = util.TableBind(sqlquery);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                category.venueid = Convert.ToInt32(dr["venueid"]);
                category.venuetypeid = Convert.ToInt32(dr["venuetypeid"]);
                category.BlockDate = Convert.ToString(dr["BlockDate"]);
                category.Id = Convert.ToInt32(dr["Id"]);
            }
            return Json(category);
        }
        [HttpPost]
        public JsonResult DeleteVenue(int Id)
        {
            Statement = "DELETE";
            sqlquery = "exec Sp_TblDateBlock '" + Statement + "'," + Id + "";
            status = util.MultipleTransactions(sqlquery);
            if (status == "Successfull")
            {
                message = "Delete Successfull!!";
            }
            else
            {
                message = "Failed to Delete";
            }
            var Data = new { msg = message };
            return Json(Data);
        }

        public ActionResult VenuetypeBind(int id)
        {
            sqlquery = "select isnull(a.NAME,'')as NAME,isnull(a.id,0) as id from TYPE_VENUE a ";
            DataSet ds = util.BindDropDown(sqlquery);
            List<SelectListItem> SubCategory = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                SubCategory.Add(new SelectListItem { Text = dr["NAME"].ToString(), Value = dr["id"].ToString() });
            }
            ViewBag.SubCategory = SubCategory;
            return Json(SubCategory);
        }
        #endregion

        #region ItemMaster 
        public IActionResult ItemMaster()
        {
            HttpContext.Session.GetString("UserId");
            if (HttpContext.Session.GetString("UserId") != null)
            {
                CategoryBind();
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin", "Master", "Admin");
            }
        }

        [HttpPost]
        public JsonResult IUItemMaster()
        {

            string itemId = Request.Form["itemId"].ToString();
            string ItemGroupcode = Request.Form["ItemGroupcode"].ToString();
            string Itemsubgroupcode = Request.Form["Itemsubgroupcode"].ToString();
            string Itemname = Request.Form["Itemname"].ToString();
            string ItemPrice = Request.Form["ItemPrice"].ToString();
            string display = Request.Form["display"].ToString();
            string ImageDisplay = Request.Form["ImageDisplay"].ToString();
            string flg = Request.Form["flg"].ToString();
            string nefilname = Request.Form["filenmes"].ToString();
            string folderName = "wwwroot/Images/";
            string webRootPath = "";
            string filename = "";
            if (flg == "okg")
            {
                nefilname = "";
                IFormFile file = Request.Form.Files[0];
                string extension = Path.GetExtension(file.FileName);
                filename = Path.GetFileNameWithoutExtension(file.FileName);
                webRootPath = filename + DateTime.Now.ToString("yymmssfff") + extension;
                nefilname = webRootPath;
                string newPath = Path.Combine(folderName, webRootPath);
                using (var fileStream = new FileStream(newPath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            if (itemId == "0")
            {
                dt = util.SelectParticular("TM_ItemMaster_Online", "*", "Itemname='" + Itemname + "'");
                if (dt.Rows.Count > 0)
                {
                    message = "This ItemMaster is already exit";
                }
                else
                {
                    Statement = "INSERT";
                    sqlquery = "exec Sp_TM_ItemMaster_Online '" + Statement + "'," + itemId + "," + ItemGroupcode + "," + Itemsubgroupcode + ",'" + Itemname + "','" + ItemPrice + "','" + webRootPath + "'," + display + "," + ImageDisplay + "";
                    status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        message = "ItemMaster added";
                    }
                    else
                    {
                        message = "ItemMaster not added";
                    }
                }
            }
            else
            {
                Statement = "UPDATE";
                sqlquery = "exec Sp_TM_ItemMaster_Online '" + Statement + "'," + itemId + "," + ItemGroupcode + "," + Itemsubgroupcode + ",'" + Itemname + "','" + ItemPrice + "','" + nefilname + "'," + display + "," + ImageDisplay + "";
                status = util.MultipleTransactions(sqlquery);
                if (status == "Successfull")
                {
                    message = "ItemMaster update";
                }
                else
                {
                    message = "ItemMaster not update";
                }
            }
            var Data = new { message = message, id = itemId };
            return Json(Data);
        }

        [HttpGet]
        public JsonResult ShowItemMaster()
        {
            List<ItemMaster> Category = new List<ItemMaster>();
            sqlquery = "exec Sp_TM_ItemMaster_Online";
            DataSet ds = util.TableBind(sqlquery);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Category.Add(new ItemMaster
                {
                    Itemsubgroupcode = Convert.ToInt32(dr["Itemsubgroupcode"]),
                    itemId = Convert.ToInt32(dr["itemId"]),
                    Itemgroup = Convert.ToString(dr["Itemgroup"]),
                    Itemsubgroup = Convert.ToString(dr["Itemsubgroup"]),
                    ItemName = Convert.ToString(dr["ItemName"]),
                    ItemPrice = Convert.ToString(dr["ItemPrice"]),
                    Item_Image = Convert.ToString(dr["Item_Image"]),
                    display = Convert.ToInt32(dr["display"]),
                    ImageDisplay = Convert.ToInt32(dr["ImageDisplay"]),
                });
            }
            return Json(Category);
        }

        [HttpPost]
        public JsonResult EditItemMaster(int itemId)
        {
            Statement = "EDIT";
            ItemMaster category = new ItemMaster();
            sqlquery = "exec Sp_TM_ItemMaster_Online '" + Statement + "', " + itemId + " ";
            DataSet ds = util.TableBind(sqlquery);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                category.ItemGroupcode = Convert.ToInt32(dr["ItemGroupcode"]);
                category.Itemsubgroupcode = Convert.ToInt32(dr["Itemsubgroupcode"]);
                category.ItemName = Convert.ToString(dr["ItemName"]);
                category.ItemPrice = Convert.ToString(dr["ItemPrice"]);
                category.Item_Image = Convert.ToString(dr["Item_Image"]);
                category.itemId = Convert.ToInt32(dr["itemId"]);
                category.display = Convert.ToInt32(dr["display"]);
                category.ImageDisplay = Convert.ToInt32(dr["ImageDisplay"]);
            }
            return Json(category);
        }

        [HttpPost]
        public JsonResult DeleteItemMaster(int itemId)
        {
            Statement = "DELETE";
            sqlquery = "exec Sp_TM_ItemMaster_Online '" + Statement + "'," + itemId + "";
            status = util.MultipleTransactions(sqlquery);
            if (status == "Successfull")
            {
                message = "Delete Successfull!!";
            }
            else
            {
                message = "Failed to Delete";
            }
            var Data = new { msg = message };
            return Json(Data);
        }

        [HttpGet]
        public ActionResult BindItemSubMaster(int ItemGroupcode)
        {
            sqlquery = "select isnull(Itemsubgroupcode,0)as Itemsubgroupcode,isnull(ItemGroupcode,0) as ItemGroupcode,isnull(Itemsubgroup,'')as Itemsubgroup from TM_ITEMSUBGROUPMASTER_Online   where ItemGroupcode=" + ItemGroupcode + "";
            DataSet ds = util.BindDropDown(sqlquery);
            List<SelectListItem> SubCategory = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                SubCategory.Add(new SelectListItem { Text = dr["Itemsubgroup"].ToString(), Value = dr["Itemsubgroupcode"].ToString() });
            }
            ViewBag.SubCategory = SubCategory;
            return Json(SubCategory);

        }
        #endregion
        //#region ItemMaster 
        //public IActionResult ItemMaster()
        //{
        //    HttpContext.Session.GetString("UserId");
        //    if (HttpContext.Session.GetString("UserId") != null)
        //    {
        //        CategoryBind();
        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("AdminLogin", "Master", "Admin");
        //    }
        //}

        //[HttpPost]
        //public JsonResult IUItemMaster()
        //{

        //    string itemId = Request.Form["itemId"].ToString();
        //    string ItemGroupcode = Request.Form["ItemGroupcode"].ToString();
        //    string Itemsubgroupcode = Request.Form["Itemsubgroupcode"].ToString();
        //    string Itemname = Request.Form["Itemname"].ToString();
        //    string ItemPrice = Request.Form["ItemPrice"].ToString();
        //    string display = Request.Form["display"].ToString();
        //    string flg = Request.Form["flg"].ToString();
        //    string nefilname = Request.Form["filenmes"].ToString();
        //    string folderName = "wwwroot/Images/";
        //    string webRootPath = "";
        //    string filename = "";
        //    if (flg == "okg")
        //    {
        //        nefilname = "";
        //        IFormFile file = Request.Form.Files[0];
        //        string extension = Path.GetExtension(file.FileName);
        //        filename = Path.GetFileNameWithoutExtension(file.FileName);
        //        webRootPath = filename + DateTime.Now.ToString("yymmssfff") + extension;
        //        nefilname = webRootPath;
        //        string newPath = Path.Combine(folderName, webRootPath);
        //        using (var fileStream = new FileStream(newPath, FileMode.Create))
        //        {
        //            file.CopyTo(fileStream);
        //        }
        //    }
        //    if (itemId == "0")
        //    {
        //        dt = util.SelectParticular("TM_ItemMaster_Online", "*", "Itemname='" + Itemname + "'");
        //        if (dt.Rows.Count > 0)
        //        {
        //            message = "This ItemMaster is already exit";
        //        }
        //        else
        //        {
        //            Statement = "INSERT";
        //            sqlquery = "exec Sp_TM_ItemMaster_Online '" + Statement + "'," + itemId + "," + ItemGroupcode + "," + Itemsubgroupcode + ",'" + Itemname + "','" + ItemPrice + "','" + webRootPath + "'," + display + "";
        //            status = util.MultipleTransactions(sqlquery);
        //            if (status == "Successfull")
        //            {
        //                message = "ItemMaster added";
        //            }
        //            else
        //            {
        //                message = "ItemMaster not added";
        //            }
        //        }
        //    }
        //    else
        //    {
        //        Statement = "UPDATE";
        //        sqlquery = "exec Sp_TM_ItemMaster_Online '" + Statement + "'," + itemId + "," + ItemGroupcode + "," + Itemsubgroupcode + ",'" + Itemname + "','" + ItemPrice + "','" + nefilname + "'," + display + "";
        //        status = util.MultipleTransactions(sqlquery);
        //        if (status == "Successfull")
        //        {
        //            message = "ItemMaster update";
        //        }
        //        else
        //        {
        //            message = "ItemMaster not update";
        //        }
        //    }
        //    var Data = new { message = message, id = itemId };
        //    return Json(Data);
        //}

        //[HttpGet]
        //public JsonResult ShowItemMaster()
        //{
        //    List<ItemMaster> Category = new List<ItemMaster>();
        //    sqlquery = "exec Sp_TM_ItemMaster_Online";
        //    DataSet ds = util.TableBind(sqlquery);
        //    foreach (DataRow dr in ds.Tables[0].Rows)
        //    {
        //        Category.Add(new ItemMaster
        //        {
        //            Itemsubgroupcode = Convert.ToInt32(dr["Itemsubgroupcode"]),
        //            itemId = Convert.ToInt32(dr["itemId"]),
        //            Itemgroup = Convert.ToString(dr["Itemgroup"]),
        //            Itemsubgroup = Convert.ToString(dr["Itemsubgroup"]),
        //            ItemName = Convert.ToString(dr["ItemName"]),
        //            ItemPrice = Convert.ToString(dr["ItemPrice"]),
        //            Item_Image = Convert.ToString(dr["Item_Image"]),
        //            display = Convert.ToInt32(dr["display"]),
        //        });
        //    }
        //    return Json(Category);
        //}

        //[HttpPost]
        //public JsonResult EditItemMaster(int itemId)
        //{
        //    Statement = "EDIT";
        //    ItemMaster category = new ItemMaster();
        //    sqlquery = "exec Sp_TM_ItemMaster_Online '" + Statement + "', " + itemId + " ";
        //    DataSet ds = util.TableBind(sqlquery);
        //    foreach (DataRow dr in ds.Tables[0].Rows)
        //    {
        //        category.ItemGroupcode = Convert.ToInt32(dr["ItemGroupcode"]);
        //        category.Itemsubgroupcode = Convert.ToInt32(dr["Itemsubgroupcode"]);
        //        category.ItemName = Convert.ToString(dr["ItemName"]);
        //        category.ItemPrice = Convert.ToString(dr["ItemPrice"]);
        //        category.Item_Image = Convert.ToString(dr["Item_Image"]);
        //        category.itemId = Convert.ToInt32(dr["itemId"]);
        //        category.display = Convert.ToInt32(dr["display"]);
        //    }
        //    return Json(category);
        //}

        //[HttpPost]
        //public JsonResult DeleteItemMaster(int itemId)
        //{
        //    Statement = "DELETE";
        //    sqlquery = "exec Sp_TM_ItemMaster_Online '" + Statement + "'," + itemId + "";
        //    status = util.MultipleTransactions(sqlquery);
        //    if (status == "Successfull")
        //    {
        //        message = "Delete Successfull!!";
        //    }
        //    else
        //    {
        //        message = "Failed to Delete";
        //    }
        //    var Data = new { msg = message };
        //    return Json(Data);
        //}

        //[HttpGet]
        //public ActionResult BindItemSubMaster(int ItemGroupcode)
        //{
        //    sqlquery = "select isnull(Itemsubgroupcode,0)as Itemsubgroupcode,isnull(ItemGroupcode,0) as ItemGroupcode,isnull(Itemsubgroup,'')as Itemsubgroup from TM_ITEMSUBGROUPMASTER_Online   where ItemGroupcode=" + ItemGroupcode + "";
        //    DataSet ds = util.BindDropDown(sqlquery);
        //    List<SelectListItem> SubCategory = new List<SelectListItem>();
        //    foreach (DataRow dr in ds.Tables[0].Rows)
        //    {
        //        SubCategory.Add(new SelectListItem { Text = dr["Itemsubgroup"].ToString(), Value = dr["Itemsubgroupcode"].ToString() });
        //    }
        //    ViewBag.SubCategory = SubCategory;
        //    return Json(SubCategory);

        //}
        //#endregion

        #region ItemSubGroup

        public ActionResult CategoryBind()
        {
            sqlquery = "select isnull(ItemGroupCode,0)as ItemGroupCode,isnull(ItemGroup,'')as ItemGroup from TM_ItemGroupMaster_Online";
            ds = util.BindDropDown(sqlquery);
            List<SelectListItem> Categoryobj = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Categoryobj.Add(new SelectListItem { Text = dr["ItemGroup"].ToString(), Value = dr["ItemGroupCode"].ToString() });
            }
            ViewBag.Categoryobj = Categoryobj;
            return View(Categoryobj);
        }

        public ActionResult ItemSubGroup()
        {
            HttpContext.Session.GetString("UserId");
            if (HttpContext.Session.GetString("UserId") != null)
            {
                CategoryBind();
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin", "Master", "Admin");
            }
        }

        [HttpPost]
        public JsonResult IUItemSubGroup(ItemSubGroups cat)
        {
            if (cat.Itemsubgroupcode == 0)
            {
                dt = util.SelectParticular("TM_ITEMSUBGROUPMASTER_Online", "*", "Itemsubgroup='" + cat.Itemsubgroup + "'and ItemGroupcode=" + cat.ItemGroupcode + "");
                if (dt.Rows.Count > 0)
                {
                    message = "This IteamSubGroup is already exit";
                }
                else
                {
                    Statement = "INSERT";
                    sqlquery = "exec Sp_TM_ITEMSUBGROUPMASTER_Online '" + Statement + "'," + cat.Itemsubgroupcode + ",'" + cat.Itemsubgroup + "'," + cat.ItemGroupcode + "," + cat.Status + "";
                    status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        message = "IteamSubGroup added.";
                    }
                    else
                    {
                        message = "IteamSubGroup not added.";
                    }
                }

            }
            else
            {
                Statement = "UPDATE";
                sqlquery = "exec Sp_TM_ITEMSUBGROUPMASTER_Online '" + Statement + "'," + cat.Itemsubgroupcode + ",'" + cat.Itemsubgroup + "'," + cat.ItemGroupcode + "," + cat.Status + "";
                status = util.MultipleTransactions(sqlquery);
                if (status == "Successfull")
                {
                    message = "IteamSubGroup updated.";
                }
                else
                {
                    message = "IteamSubGroup not updated.";
                }
            }
            var Data = new { message = message, id = cat.Itemsubgroupcode };
            return Json(Data);
        }

        [HttpGet]
        public JsonResult ShowItemSubGroup()
        {
            List<ItemSubGroups> Category = new List<ItemSubGroups>();
            sqlquery = "exec Sp_TM_ITEMSUBGROUPMASTER_Online";
            DataSet ds = util.TableBind(sqlquery);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Category.Add(new ItemSubGroups
                {
                    Itemsubgroupcode = Convert.ToInt32(dr["Itemsubgroupcode"]),
                    Itemgroup = Convert.ToString(dr["Itemgroup"]),
                    Itemsubgroup = Convert.ToString(dr["Itemsubgroup"]),
                    Status = Convert.ToInt32(dr["Status"]),
                });
            }
            return Json(Category);
        }

        [HttpPost]
        public JsonResult EditItemSubGroup(int Itemsubgroupcode)
        {
            Statement = "EDIT";
            ItemSubGroups category = new ItemSubGroups();
            sqlquery = "exec Sp_TM_ITEMSUBGROUPMASTER_Online '" + Statement + "', " + Itemsubgroupcode + " ";
            DataSet ds = util.TableBind(sqlquery);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                category.Itemsubgroupcode = Convert.ToInt32(dr["Itemsubgroupcode"]);
                category.ItemGroupcode = Convert.ToInt32(dr["ItemGroupcode"]);
                category.Itemsubgroup = Convert.ToString(dr["Itemsubgroup"]);
                category.Status = Convert.ToInt32(dr["Status"]);


            }
            return Json(category);
        }

        [HttpPost]
        public JsonResult DeleteItemSubGroup(int Itemsubgroupcode)
        {
            Statement = "DELETE";
            sqlquery = "exec Sp_TM_ITEMSUBGROUPMASTER_Online '" + Statement + "'," + Itemsubgroupcode + "";
            status = util.MultipleTransactions(sqlquery);
            if (status == "Successfull")
            {
                message = "Delete Successfull!!";
            }
            else
            {
                message = "Failed to Delete";
            }
            var Data = new { msg = message };
            return Json(Data);
        }


        #endregion

        #region ItemGroup
        public IActionResult ItemGroup()
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
        public ActionResult IUItemGroup(ItemGroups cat)
        {

            if (cat.ItemGroupCode == 0)
            {
                dt = util.SelectParticular("TM_ItemGroupMaster_Online", "*", "ItemGroup='" + cat.ItemGroup + "'");
                if (dt.Rows.Count > 0)
                {
                    message = "This ItemGroup is already exit";
                }
                else
                {
                    Statement = "INSERT";
                    sqlquery = "exec Sp_TM_ItemGroupMaster_Online '" + Statement + "'," + cat.ItemGroupCode + ",'" + cat.ItemGroup + "'," + cat.Status + "";
                    status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        message = "ItemGroup added.";
                    }
                    else
                    {
                        message = "ItemGroup not added.";
                    }
                }

            }

            else
            {
                Statement = "UPDATE";
                sqlquery = "exec Sp_TM_ItemGroupMaster_Online '" + Statement + "'," + cat.ItemGroupCode + ",'" + cat.ItemGroup + "'," + cat.Status + "";
                status = util.MultipleTransactions(sqlquery);
                if (status == "Successfull")
                {
                    message = "ItemGroup updated.";
                }
                else
                {
                    message = "ItemGroup not updted.";
                }
            }
            var Data = new { message = message, ItemGroupCode = cat.ItemGroupCode };
            return Json(Data);
        }

        [HttpGet]
        public JsonResult ShowItemGroup()
        {
            List<ItemGroups> ItemGroups = new List<ItemGroups>();
            sqlquery = "exec Sp_TM_ItemGroupMaster_Online";
            DataSet ds = util.TableBind(sqlquery);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                ItemGroups.Add(new ItemGroups
                {
                    ItemGroupCode = Convert.ToInt32(dr["ItemGroupCode"]),
                    ItemGroup = Convert.ToString(dr["ItemGroup"]),
                    Status = Convert.ToInt32(dr["Status"]),
                });
            }

            return Json(ItemGroups);
        }
        [HttpPost]
        public JsonResult EditItemGroup(int ItemGroupCode)
        {
            Statement = "EDIT";
            ItemGroups category = new ItemGroups();
            sqlquery = "exec Sp_TM_ItemGroupMaster_Online '" + Statement + "', " + ItemGroupCode + " ";
            DataSet ds = util.TableBind(sqlquery);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                category.ItemGroupCode = Convert.ToInt32(dr["ItemGroupCode"]);
                category.ItemGroup = Convert.ToString(dr["ItemGroup"]);
                category.Status = Convert.ToInt32(dr["Status"]);
            }
            return Json(category);
        }
        [HttpPost]
        public JsonResult DeleteItemGroup(int ItemGroupCode)
        {
            Statement = "DELETE";
            sqlquery = "exec Sp_TM_ItemGroupMaster_Online '" + Statement + "'," + ItemGroupCode + "";
            status = util.MultipleTransactions(sqlquery);
            if (status == "Successfull")
            {
                message = "Delete Successfull!!";
            }
            else
            {
                message = "Failed to Delete";
            }
            var Data = new { msg = message };
            return Json(Data);
        }

        #endregion

        #region Affiliate 
        public IActionResult Affiliate()
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
        public JsonResult IUAffiliate(Affiliate cat)
        {
            if (cat.ID == 0)
            {
                dt = util.SelectParticular("APP_tblAffiliatedClub", "*", "ClubName='" + cat.ClubName + "' and ID=" + cat.ID + "");
                if (dt.Rows.Count > 0)
                {
                    message = "This Affiliate is already exit";
                }
                else
                {
                    Statement = "INSERT";
                    sqlquery = "exec Sp_APP_tblAffiliatedClub '" + Statement + "'," + cat.ID + ",'" + cat.ClubName + "','" + cat.Address + "','" + cat.Email + "','" + cat.Contact + "'," + cat.Status + "";
                    status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        message = "Affiliate added.";
                    }
                    else
                    {
                        message = "Affiliate not added.";
                    }
                }

            }
            else
            {
                Statement = "UPDATE";
                sqlquery = "exec Sp_APP_tblAffiliatedClub '" + Statement + "'," + cat.ID + ",'" + cat.ClubName + "','" + cat.Address + "','" + cat.Email + "','" + cat.Contact + "'," + cat.Status + "";
                status = util.MultipleTransactions(sqlquery);
                if (status == "Successfull")
                {
                    message = "Affiliate updated.";
                }
                else
                {
                    message = "Affiliate not updated.";
                }
            }
            var Data = new { message = message, id = cat.ID };
            return Json(Data);
        }

        [HttpGet]
        public JsonResult ShowAffiliate()
        {
            List<Affiliate> Category = new List<Affiliate>();
            sqlquery = "exec Sp_APP_tblAffiliatedClub";
            DataSet ds = util.TableBind(sqlquery);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Category.Add(new Affiliate
                {
                    ID = Convert.ToInt32(dr["ID"]),
                    ClubName = Convert.ToString(dr["ClubName"]),
                    Address = Convert.ToString(dr["Address"]),
                    Email = Convert.ToString(dr["Email"]),
                    Contact = Convert.ToString(dr["Contact"]),
                    Status = Convert.ToInt32(dr["Status"]),
                });
            }
            return Json(Category);
        }

        [HttpPost]
        public JsonResult EditAffiliate(int ID)
        {
            Statement = "EDIT";
            Affiliate category = new Affiliate();
            sqlquery = "exec Sp_APP_tblAffiliatedClub '" + Statement + "', " + ID + " ";
            DataSet ds = util.TableBind(sqlquery);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                category.ID = Convert.ToInt32(dr["ID"]);
                category.ClubName = Convert.ToString(dr["ClubName"]);
                category.Address = Convert.ToString(dr["Address"]);
                category.Email = Convert.ToString(dr["Email"]);
                category.Contact = Convert.ToString(dr["Contact"]);
                category.Status = Convert.ToInt32(dr["Status"]);
            }
            return Json(category);
        }

        [HttpPost]
        public JsonResult DeleteAffiliate(int ID)
        {
            Statement = "DELETE";
            sqlquery = "exec Sp_APP_tblAffiliatedClub '" + Statement + "'," + ID + "";
            status = util.MultipleTransactions(sqlquery);
            if (status == "Successfull")
            {
                message = "Affiliate Delete Successfull!!";
            }
            else
            {
                message = "Failed to Delete";
            }
            var Data = new { msg = message };
            return Json(Data);
        }

        #endregion

        #region Gallery Main
        public IActionResult GalleryMain()
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
        public JsonResult IUGalleryMain()
        {
            string Id = Request.Form["Id"].ToString();
            string name = Request.Form["name"].ToString();
            string display = Request.Form["display"].ToString();
            string flg = Request.Form["flg"].ToString();
            string nefilname = Request.Form["filenmes"].ToString();
            //string AppPath = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/";
            string folderName = "wwwroot/GalleryMain";
            //string folderName1 = "GalleryMain/";
            string webRootPath = "";
            //string webRootPath1 = AppPath + folderName1;
            string NewRoot = "";
            string NewRoot1 = "";
            string filename = "";
            if (flg == "okg")
            {
                nefilname = "";
                IFormFile file = Request.Form.Files[0];
                string extension = Path.GetExtension(file.FileName);
                filename = Path.GetFileNameWithoutExtension(file.FileName);
                webRootPath = filename + DateTime.Now.ToString("yymmssfff") + extension;
                nefilname = webRootPath;
                string newPath = Path.Combine(folderName, webRootPath);
                using (var fileStream = new FileStream(newPath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                    fileStream.Flush();
                }
            }
            //NewRoot = webRootPath1 + webRootPath;
            //NewRoot1 = nefilname;
            if (Id == "0")
            {
                Statement = "INSERT";
                sqlquery = "exec Sp_Gallery_Main '" + Statement + "'," + Id + ",'" + name + "','" + webRootPath + "'," + display + "";
                status = util.MultipleTransactions(sqlquery);
                if (status == "Successfull")
                {
                    message = "Image added";
                }
                else
                {
                    message = "Image not added";
                }

            }
            else
            {
                Statement = "UPDATE";
                sqlquery = "exec Sp_Gallery_Main '" + Statement + "'," + Id + ",'" + name + "','" + nefilname + "'," + display + "";
                status = util.MultipleTransactions(sqlquery);
                if (status == "Successfull")
                {
                    message = "Image update";
                }
                else
                {
                    message = "Image not update";
                }
            }

            var Data = new { message = message, id = Id };
            return Json(Data);
        }

        [HttpGet]
        public JsonResult ShowGalleryMain()
        {
            List<GalleryMain> Category = new List<GalleryMain>();
            sqlquery = "exec Sp_Gallery_Main";
            DataSet ds = util.TableBind(sqlquery);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Category.Add(new GalleryMain
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    name = Convert.ToString(dr["name"]),
                    Image = Convert.ToString(dr["image_path"]),
                    display = Convert.ToInt32(dr["display"]),
                });
            }
            return Json(Category);
        }

        [HttpPost]
        public JsonResult EditGalleryMain(int Id)
        {

            Statement = "EDIT";
            GalleryMain category = new GalleryMain();
            sqlquery = "exec Sp_Gallery_Main '" + Statement + "', " + Id + " ";
            DataSet ds = util.TableBind(sqlquery);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                category.name = Convert.ToString(dr["name"]);
                category.Image = Convert.ToString(dr["image_path"]);
                category.Id = Convert.ToInt32(dr["Id"]);
                category.display = Convert.ToInt32(dr["display"]);
            }
            return Json(category);
        }

        [HttpPost]
        public JsonResult DeleteGalleryMain(int Id)
        {
            Statement = "DELETE";
            sqlquery = "exec Sp_Gallery_Main '" + Statement + "'," + Id + "";
            status = util.MultipleTransactions(sqlquery);
            if (status == "Successfull")
            {
                message = "Image Delete Successfull!!";
            }
            else
            {
                message = "Failed to Delete";
            }
            var Data = new { msg = message };
            return Json(Data);
        }
        #endregion

        #region Gallery
        public ActionResult BindGallery()
        {
            sqlquery = "select isnull(Id,0) as Id,isnull(name,'') as name from Tblgallery_main";
            DataSet ds = util.BindDropDown(sqlquery);
            List<SelectListItem> Categoryobj = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Categoryobj.Add(new SelectListItem { Text = dr["name"].ToString(), Value = dr["Id"].ToString() });
            }
            ViewBag.Category = Categoryobj;
            return View(Categoryobj);
        }

        public IActionResult Gallery()
        {
            HttpContext.Session.GetString("UserId");
            if (HttpContext.Session.GetString("UserId") != null)
            {
                BindGallery();
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin", "Master", "Admin");
            }

        }

        [HttpPost]
        public JsonResult IUGallery()
        {
            string Id = Request.Form["Id"].ToString();
            string mainid = Request.Form["mainid"].ToString();
            string display = Request.Form["display"].ToString();
            string flg = Request.Form["flg"].ToString();
            string nefilname = Request.Form["filenmes"].ToString();
            //string AppPath = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/";
            string folderName = "wwwroot/Gallery";
            //string folderName1 = "Gallery/";
            string webRootPath = "";
            //string webRootPath1 = AppPath + folderName1;
            string NewRoot = "";
            string NewRoot1 = "";
            string filename = "";
            if (flg == "okg")
            {
                nefilname = "";
                IFormFile file = Request.Form.Files[0];
                string extension = Path.GetExtension(file.FileName);
                filename = Path.GetFileNameWithoutExtension(file.FileName);
                webRootPath = filename + DateTime.Now.ToString("yymmssfff") + extension;
                nefilname = webRootPath;
                string newPath = Path.Combine(folderName, webRootPath);
                using (var fileStream = new FileStream(newPath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                    fileStream.Flush();
                }
            }
            //NewRoot = webRootPath1 + webRootPath;
            //NewRoot1 = nefilname;
            if (Id == "0")
            {
                Statement = "INSERT";
                sqlquery = "exec Sp_Gallery '" + Statement + "'," + Id + "," + mainid + ",'" + webRootPath + "'," + display + "";
                status = util.MultipleTransactions(sqlquery);
                if (status == "Successfull")
                {
                    message = "Image added";
                }
                else
                {
                    message = "Image not added";
                }

            }
            else
            {
                Statement = "UPDATE";
                sqlquery = "exec Sp_Gallery '" + Statement + "'," + Id + "," + mainid + ",'" + nefilname + "'," + display + "";
                status = util.MultipleTransactions(sqlquery);
                if (status == "Successfull")
                {
                    message = "Image update";
                }
                else
                {
                    message = "Image not update";
                }
            }
            var Data = new { message = message, id = Id };
            return Json(Data);
        }

        [HttpGet]
        public JsonResult ShowGallery()
        {
            List<Gallery> Category = new List<Gallery>();
            sqlquery = "exec Sp_Gallery";
            DataSet ds = util.TableBind(sqlquery);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Category.Add(new Gallery
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    name = Convert.ToString(dr["name"]),
                    GImage = Convert.ToString(dr["GImage"]),
                    display = Convert.ToInt32(dr["display"]),

                });
            }
            return Json(Category);
        }

        [HttpPost]
        public JsonResult EditGallery(int Id)
        {

            Statement = "EDIT";
            Gallery category = new Gallery();
            sqlquery = "exec Sp_Gallery '" + Statement + "', " + Id + " ";
            DataSet ds = util.TableBind(sqlquery);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                category.GImage = Convert.ToString(dr["GImage"]);
                category.Id = Convert.ToInt32(dr["Id"]);
                category.mainid = Convert.ToInt32(dr["mainid"]);
                category.display = Convert.ToInt32(dr["display"]);
            }
            return Json(category);
        }

        [HttpPost]
        public JsonResult DeleteGallery(int Id)
        {
            Statement = "DELETE";
            sqlquery = "exec Sp_Gallery '" + Statement + "'," + Id + "";
            status = util.MultipleTransactions(sqlquery);
            if (status == "Successfull")
            {
                message = "Image Delete Successfull!!";
            }
            else
            {
                message = "Failed to Delete";
            }
            var Data = new { msg = message };
            return Json(Data);
        }
        #endregion

        #region Event 
        public IActionResult EVent()
        {
            HttpContext.Session.GetString("UserId");
            if (HttpContext.Session.GetString("UserId") != null)
            {
                CategoryBind();
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin", "Master", "Admin");
            }
        }

        [HttpPost]
        public JsonResult IUEvent()
        {
            string EventId = Request.Form["EventId"].ToString();
            string EventName = Request.Form["EventName"].ToString();
            string EventDate = Request.Form["EventDate"].ToString();
            string display = Request.Form["display"].ToString();
            string Description = Request.Form["Description"].ToString();
            string flg = Request.Form["flg"].ToString();
            string nefilname = Request.Form["filenmes"].ToString();
            string folderName = "wwwroot/Event/";
            string webRootPath = "";
            string filename = "";
            if (flg == "okg")
            {
                nefilname = "";
                IFormFile file = Request.Form.Files[0];
                string extension = Path.GetExtension(file.FileName);
                filename = Path.GetFileNameWithoutExtension(file.FileName);
                webRootPath = filename + DateTime.Now.ToString("yymmssfff") + extension;
                nefilname = webRootPath;
                string newPath = Path.Combine(folderName, webRootPath);
                using (var fileStream = new FileStream(newPath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            if (EventId == "0")
            {
                dt = util.SelectParticular("TblEvent", "*", "EventName='" + EventName + "'");
                if (dt.Rows.Count > 0)
                {
                    message = "This Event is already exit";
                }
                else
                {
                    Statement = "INSERT";
                    sqlquery = "exec Sp_TM_Event '" + Statement + "'," + EventId + ",'" + EventName + "','" + EventDate + "','" + webRootPath + "'," + display + ",'" + Description + "'";
                    status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        message = "Event added";
                    }
                    else
                    {
                        message = "Event not added";
                    }
                }
            }
            else
            {
                Statement = "UPDATE";
                sqlquery = "exec Sp_TM_Event '" + Statement + "'," + EventId + ",'" + EventName + "','" + EventDate + "','" + nefilname + "'," + display + ",'" + Description + "'";
                status = util.MultipleTransactions(sqlquery);
                if (status == "Successfull")
                {
                    message = "Event update";
                }
                else
                {
                    message = "Event not update";
                }
            }
            var Data = new { message = message, id = EventId };
            return Json(Data);
        }

        [HttpGet]
        public JsonResult ShowEvent()
        {
            List<Event> Category = new List<Event>();
            sqlquery = "exec Sp_TM_Event";
            DataSet ds = util.TableBind(sqlquery);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Category.Add(new Event
                {
                    EventName = Convert.ToString(dr["EventName"]),
                    EventDate = Convert.ToString(dr["EventDate"]),
                    EventId = Convert.ToInt32(dr["EventId"]),
                    EventImage = Convert.ToString(dr["EventImage"]),
                    display = Convert.ToInt32(dr["display"]),
                    Description = Convert.ToString(dr["Description"]),
                });
            }
            return Json(Category);
        }

        [HttpPost]
        public JsonResult EditEvent(int EventId)
        {
            Statement = "EDIT";
            Event category = new Event();
            sqlquery = "exec Sp_TM_Event '" + Statement + "', " + EventId + " ";
            DataSet ds = util.TableBind(sqlquery);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                category.EventName = Convert.ToString(dr["EventName"]);
                category.EventDate = Convert.ToString(dr["EventDate"]);
                category.EventImage = Convert.ToString(dr["EventImage"]);
                category.EventId = Convert.ToInt32(dr["EventId"]);
                category.display = Convert.ToInt32(dr["display"]);
                category.Description = Convert.ToString(dr["Description"]);
            }
            return Json(category);
        }

        [HttpPost]
        public JsonResult DeleteEvent(int EventId)
        {
            Statement = "DELETE";
            sqlquery = "exec Sp_TM_Event '" + Statement + "'," + EventId + "";
            status = util.MultipleTransactions(sqlquery);
            if (status == "Successfull")
            {
                message = "Event Delete Successfull!!";
            }
            else
            {
                message = "Failed to Delete";
            }
            var Data = new { msg = message };
            return Json(Data);
        }
        #endregion

        #region Reminder
        public IActionResult Reminder()
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
        }

        

        public JsonResult getMemberdata(int mon,int type,int cat,string mem,int amount)
        {
            sqlquery = $"exec Usp_ReminderMember {mon},{type},{cat},'{mem}',{amount}";
            DataSet ds = util.TableBind(sqlquery);
            DataTable dt = new DataTable();
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }   

            return Json(JsonConvert.SerializeObject(dt));
        }

       

         public async Task<IActionResult> Submitreminder(string jdata)
        {
            try
            {
                string refno;
                DataTable dt = new DataTable();
                //dt=JsonConvert.DeserializeObject<DataTable>(jdata.ToString());

                List<ReminderModel> Member = JsonConvert.DeserializeObject<List<ReminderModel>>(jdata);


                // Ensure the folder exists

                if (Member.Any())
                {
                    for (int i = 0; i < Member.Count; i++)
                    {
                        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", Member[i].remindertype);
                        string fileName = $"{Member[i].remindertype}_{Member[i].MemberId}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
                        string filePath = Path.Combine(folderPath, fileName);
                        var d = DateTime.Now;
                        int startYear = d.Month >= 4 ? d.Year : d.Year - 1;          
                        string endYY = (startYear + 1).ToString().Substring(2);      
                        var fy= $"{startYear}-{endYY}";
                        //string smo = string.Format("{0,3:000}",Convert.ToInt32(Member[i].month));
                        refno = string.Format("{0:3:000}/DGC/{1}", string.Format("{0,3:000}", Convert.ToInt32(Member[i].month)), fy);
                        //refno = $"{Member[i].month:D4}/DGC/{fy}";
                        //refno = $"{Member[i].month}"
                        Member[i].refno=refno;
                        Member[i].amountinwords = util.NumberToWords(Convert.ToInt32(Member[i].amount));
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        string query = $"exec save_reminder '{Member[i].MemberId}',{Member[i].type},{Member[i].amount},'{Member[i].month}',{Member[i].category},{Convert.ToInt32(HttpContext.Session.GetString("UserId"))},'{refno}'";
                        string status = util.MultipleTransactions(query);
                        if (status == "Successfull")
                        {
                            string viewName;
                            if (Member[i].type == 1)
                            {
                                viewName = "ReminderGrid";
                            }
                            else if (Member[i].type == 2)
                            {
                                viewName = "ReminderGrid2";
                            }
                            else if (Member[i].type == 3)
                            {
                                
                                viewName = "ReminderFinal";
                            }
                            else
                            {
                                viewName = "LetterOftermination";
                            }
                            var pdf = new ViewAsPdf(viewName, Member[i])
                            {
                                FileName = fileName,
                                PageSize = Rotativa.AspNetCore.Options.Size.A4
                                //PageSize = Rotativa.AspNetCore.Options.Size.
                            };

                            byte[] pdfBytes = await pdf.BuildFile(ControllerContext);
                            await System.IO.File.WriteAllBytesAsync(filePath, pdfBytes);
                            string OtpStatus1 = ClsUtil.SendMailViaIIS_htmlClub("sonu@bsdinfotech.com", "valmiki@bsdinfotech.com", "", "", "Venue Booking", "Reminder", null, "SLpsP@2025#Meta", "mx2.mailguru.biz", filePath);
                            if (OtpStatus1 == "Sent")
                            {
                                message = "Reminder Sent";
                           }

                        }
                        else
                        {
                            message = "Failed to Send Reminder";
                        }
                    }

                }
            }
            catch (Exception ex)
                {
                    message = ex.Message;
                }
            return Json(message);
        }

        public PartialViewResult ReminderGrid()
        {
            return PartialView("_ReminderGrid");
        }

        #endregion


        #region Communication
        [HttpGet]
        public IActionResult Communication()
        {
            ViewBag.bindcat = util.PopulateDropDown("select CATEGORYCODE,CATEGORYNAME from TM_CATEGORYMASTER_Cre_Cou", util.Clubstr);
            return View();
        }
        [HttpGet]
        public IActionResult getcotMember()
        {
            sqlquery = "select CATEGORYCODE,CATEGORYNAME from TM_CATEGORYMASTER_Cre_Cou ";
            ds = util.BindDropDown(sqlquery);
            List<Category> Categoryobj = new List<Category>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Categoryobj.Add(new Category
                {
                    CId = Convert.ToInt32(dr["CATEGORYCODE"]),
                    CategoryName = dr["CATEGORYNAME"].ToString()
                });
            }

            return Json(Categoryobj);
        }

        public JsonResult getMemberasd(int id)
        {
            //  sqlquery = "select isnull(a.NAME,'')as NAME,isnull(a.id,0) as id from TYPE_VENUE as a join tblVenueMaster as b on a.venuid=b.ID  where a.venuid=" + id + "";

            sqlquery = $"select Membershipno id,FirstName+' '+LastName+'('+Membershipno+')' as NAME from TM_MemberDetail where category={id}";
            DataSet ds = util.BindDropDown(sqlquery);
            List<SelectListItem> SubCategory = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                SubCategory.Add(new SelectListItem { Text = dr["NAME"].ToString(), Value = dr["id"].ToString() });
            }
            ViewBag.SubCategory = SubCategory;
            return Json(SubCategory);
        }

        public IActionResult getcomMember(int id)
        {
            sqlquery = $"exec [Sp_TM_Communication] @statement='getmember',@cat={id}";
            DataSet ds = util.TableBind(sqlquery);
            DataTable dt =new DataTable();
            if (ds.Tables.Count > 0)
            {
                dt=ds.Tables[0];
            }
            return Json(JsonConvert.SerializeObject(dt));
        }
        [HttpPost]
        public async Task<JsonResult> Savecomm()
        {
            string data = Request.Form["data"].ToString();

            List<Communication> Member = JsonConvert.DeserializeObject<List<Communication>>(data);
            //string Description = Request.Form["description"].ToString();

            // int Category =Convert.ToInt32(Request.Form["Category"]);
            // int isActive =Convert.ToInt32(Request.Form["isActive"]);
            int comid = Convert.ToInt32(Request.Form["comid"]);
            string flg = Request.Form["flg"].ToString();
            string nefilname = Request.Form["filenmes"].ToString();
            string folderName = "wwwroot/communication/";
            string webRootPath = "";
            string filename = "";
            string newPath = "";
            string IDAppPath = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/";
            //string IDLogoName = IDPhoto + DateTime.Now.ToString("yymmssff") + IDextension;
           
            string IDSaveNameFPho = "communication/";
           // string IDfile = IDAppPath + IDSaveNameFPho + "/" + IDLogoName;
            string fullurl = "";
            
            if (flg == "okg")
            {
                nefilname = "";
                IFormFile file = Request.Form.Files[0];
                string extension = Path.GetExtension(file.FileName);
                filename = Path.GetFileNameWithoutExtension(file.FileName);
                webRootPath = filename + DateTime.Now.ToString("yymmssfff") + extension;
                nefilname = webRootPath;
                newPath = Path.Combine(folderName, webRootPath);
                fullurl = IDAppPath + IDSaveNameFPho + webRootPath;
                using (var fileStream = new FileStream(newPath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
                string img = "https://darjeelinggymkhanaclub.com/wp-content/uploads/2022/03/gymkhana-new-logo-150x150.png";

            if (Member.Count > 0) {
               
                    Statement = "insert";
              for(int i=0; i < Member.Count; i++) {
                
                    sqlquery = $"exec [Sp_TM_Communication] '{Statement}','{Member[i].subject}',{Member[i].cat},'{nefilname}','{Member[i].DESCRIPTION}',{Member[i].status},{Member[i].memid}";
                    status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                      //  string OtpStatus1 = "";
                        string OtpStatus1 = ClsUtil.SendMailViaIIS_htmlClub("sonu@bsdinfotech.com", "valmikiyadav.bbdu@gmail.com", "", "", "Venue Booking", Member[i].DESCRIPTION, null, "SLpsP@2025#Meta", "mx2.mailguru.biz", newPath);
                        if (flg == "ok")
                        {
                            //string nsg = await ClsUtil.SendWhatsAppMessageAsync("918130165560", Member[i].DESCRIPTION, "68B0032FEED71", "682eaf57552d7");
                            string nsg = await ClsUtil.SendWhatsAppMessageAsync("918130165560", Member[i].DESCRIPTION, _config["Instanceid"], _config["accessTocken"]);
                        }
                        else
                        {
                            string ansg = await ClsUtil.SendWhatsAppWithAttachmentAsync("918130165560", Member[i].DESCRIPTION, _config["Instanceid"], _config["accessTocken"], fullurl);
                        }
                        

                        if (OtpStatus1 == "Sent")
                        {
                            message = "Success";
                        }
                        else
                        {
                            message = OtpStatus1;
                        }
                    }
                    else
                    {
                        message = status;
                    }
                }
            }

            return Json(message);
        }

        #endregion
        #region ManagingCommitte
        public IActionResult ManagingCommitte()
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
        public JsonResult IUManagingCommitte()
        {
            string ManagingId = Request.Form["ManagingId"].ToString();
            string Name = Request.Form["Name"].ToString();
            string Designation = Request.Form["Designation"].ToString();
            string Address = Request.Form["Address"].ToString();
            string Mobile = Request.Form["Mobile"].ToString();
            string DESCRIPTION = Request.Form["DESCRIPTION"].ToString();
            string OrderBy = Request.Form["OrderBy"].ToString();
            string flg = Request.Form["flg"].ToString();
            string nefilname = Request.Form["filenmes"].ToString();
            string folderName = "wwwroot/ManagingCommitte/";
            string webRootPath = "";
            string filename = "";

            if (flg == "okg")
            {
                nefilname = "";
                IFormFile file = Request.Form.Files[0];
                string extension = Path.GetExtension(file.FileName);
                filename = Path.GetFileNameWithoutExtension(file.FileName);
                webRootPath = filename + DateTime.Now.ToString("yymmssfff") + extension;
                nefilname = webRootPath;
                string newPath = Path.Combine(folderName, webRootPath);
                using (var fileStream = new FileStream(newPath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            if (ManagingId == "0")
            {
                dt = util.SelectParticular("TblManagingCommitte", "*", "OrderBy ='" + OrderBy + "'");
                if (dt.Rows.Count > 0)
                {
                    message = "This order number is already exit";
                }
                else
                {
                    Statement = "INSERT";
                    sqlquery = "exec Usp_ManageCommitte '" + Statement + "'," + ManagingId + ",'" + Name + "','" + Designation + "','" + Address + "','" + Mobile + "','" + nefilname + "','" + DESCRIPTION + "'," + OrderBy + "";
                    status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        message = "Managing Committe added";
                    }
                    else
                    {
                        message = "Managing Committe not added";
                    }
                }
            }
            else
            {
                sqlquery = "select * from TblManagingCommitte where ManagingId != '" + ManagingId + "' and OrderBy= '" + OrderBy + "'";
                dt = util.execQuery(sqlquery);
                if (dt.Rows.Count > 0)
                {
                    message = "This order number is already exit";
                }
                else
                {
                    Statement = "UPDATE";
                    sqlquery = "exec Usp_ManageCommitte '" + Statement + "'," + ManagingId + ",'" + Name + "','" + Designation + "','" + Address + "','" + Mobile + "','" + nefilname + "','" + DESCRIPTION + "'," + OrderBy + "";
                    status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        message = "Managing Committe update";
                    }
                    else
                    {
                        message = "Managing Committe not update";
                    }
                }
            }
            var Data = new { message = message, id = ManagingId };
            return Json(Data);
        }
        [HttpGet]
        public JsonResult ShowManagingCommitte()
        {
            List<ManagingCommitte> Category = new List<ManagingCommitte>();
            sqlquery = "exec Usp_ManageCommitte";
            DataSet ds = util.TableBind(sqlquery);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Category.Add(new ManagingCommitte
                {
                    Name = Convert.ToString(dr["Name"]),
                    Designation = Convert.ToString(dr["Designation"]),
                    Address = Convert.ToString(dr["Address"]),
                    Mobile = Convert.ToString(dr["Mobile"]),
                    ManagingId = Convert.ToInt32(dr["ManagingId"]),
                    photo = Convert.ToString(dr["photo"]),
                    DESCRIPTION = Convert.ToString(dr["DESCRIPTION"]),
                    OrderBy = Convert.ToInt32(dr["OrderBy"]),
                });
            }
            return Json(Category);
        }
        [HttpPost]
        public JsonResult DeleteManagingCommitte(int ManagingId)
        {
            Statement = "DELETE";
            sqlquery = "exec Usp_ManageCommitte '" + Statement + "'," + ManagingId + "";
            status = util.MultipleTransactions(sqlquery);
            if (status == "Successfull")
            {
                message = "ManagingCommitte Delete Successfull!!";
            }
            else
            {
                message = "Failed to Delete";
            }
            var Data = new { msg = message };
            return Json(Data);
        }

        [HttpPost]
        public JsonResult EditManagingCommitte(int ManagingId)
        {
            Statement = "EDIT";
            ManagingCommitte category = new ManagingCommitte();
            sqlquery = "exec Usp_ManageCommitte '" + Statement + "', " + ManagingId + " ";
            DataSet ds = util.TableBind(sqlquery);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                category.Name = Convert.ToString(dr["Name"]);
                category.Designation = Convert.ToString(dr["Designation"]);
                category.Address = Convert.ToString(dr["Address"]);
                category.Mobile = Convert.ToString(dr["Mobile"]);
                category.ManagingId = Convert.ToInt32(dr["ManagingId"]);
                category.photo = Convert.ToString(dr["photo"]);
                category.DESCRIPTION = Convert.ToString(dr["DESCRIPTION"]);
                category.OrderBy = Convert.ToInt32(dr["OrderBy"]);
            }
            return Json(category);
        }

        #endregion

        #region Availability
        public IActionResult BookingReports()
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
        public JsonResult BindVenueData(string VENUE_DATE1, string VENUE_DATE2, int VenueId)
        {
            StringBuilder sb = new StringBuilder();
            sqlquery = "Usp_Bookings_Availability '" + VENUE_DATE1 + "','" + VENUE_DATE2 + "'," + VenueId + "";
            DataSet ds = util.TableBind(sqlquery);
            DataTable dt = ds.Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sb.Append(dt.Rows[i]["Str1"]);
            }
            string attend = sb.ToString();
            return Json(attend);
        }
        [HttpPost]
        public JsonResult IUBookingReports()
        {
            sqlquery = "select ID,VenueName from tblVenueMaster";
            DataSet ds = util.TableBind(sqlquery);
            return Json(JsonConvert.SerializeObject(ds.Tables[0]));
        }
        #endregion

        #region CMS_DROPDOWN
        //[Route("Admin/ClubApp/CMS_DROPDOWN/{id}")]

        public IActionResult BindCMS_Dropdown()
        {
            sqlquery = "select isnull(id,0)as id,isnull(name,'')as name from CMS_Dropdown";
            ds = util.BindDropDown(sqlquery);
            List<SelectListItem> Categoryobj = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Categoryobj.Add(new SelectListItem { Text = dr["name"].ToString(), Value = dr["id"].ToString() });
            }
            ViewBag.Categoryobj = Categoryobj;
            return View(Categoryobj);
        }
        public IActionResult CMS_DROPDOWN()
        {

            HttpContext.Session.GetString("UserId");
            if (HttpContext.Session.GetString("UserId") != null)
            {
                //ViewBag.ID = id;
                BindCMS_Dropdown();
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin", "Master", "Admin");
            }
        }

        [HttpPost]
        public JsonResult IUCMS_DROPDOWN(CMS_DROPDOWN cat)
        {
            if (cat.EventId == 0)
            {
                dt = util.SelectParticular("aboutus_news", "*", "EventName='" + cat.EventName + "'");
                if (dt.Rows.Count > 0)
                {
                    message = "This Event Name is already exit";
                }
                else
                {
                    Statement = "INSERT";
                    sqlquery = "exec Sp_CMS_Dropdown_new '" + Statement + "'," + cat.EventId + "," + cat.id + ",'" + cat.EventName + "'";
                    status = util.MultipleTransactions(sqlquery);
                    if (status == "Successfull")
                    {
                        message = "Event Name added";
                    }
                    else
                    {
                        message = "Event Name not added";
                    }
                }
            }
            else
            {
                Statement = "UPDATE";
                sqlquery = "exec Sp_CMS_Dropdown_new '" + Statement + "'," + cat.EventId + "," + cat.id + ",'" + cat.EventName + "'";
                status = util.MultipleTransactions(sqlquery);
                if (status == "Successfull")
                {
                    message = "Event Name update";
                }
                else
                {
                    message = "Event Name not update";
                }
            }
            var Data = new { message = message, EventId = cat.EventId };
            return Json(Data);
        }

        [HttpGet]
        public JsonResult ShowCMS_DROPDOWN(int EventId)
        {
            List<CMS_DROPDOWN> ItemGroups = new List<CMS_DROPDOWN>();
            sqlquery = "exec Sp_CMS_Dropdown_new 'SELECT'," + EventId + "";
            DataSet ds = util.TableBind(sqlquery);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                ItemGroups.Add(new CMS_DROPDOWN
                {
                    EventId = Convert.ToInt32(dr["EventId"]),
                    name = Convert.ToString(dr["name"]),
                    EventName = Convert.ToString(dr["EventName"]),
                });
            }

            return Json(ItemGroups);
        }
        [HttpPost]
        public JsonResult EditCMS_DROPDOWN(int EventId)
        {
            Statement = "EDIT";
            CMS_DROPDOWN category = new CMS_DROPDOWN();
            sqlquery = "exec Sp_CMS_Dropdown_new '" + Statement + "', " + EventId + " ";
            DataSet ds = util.TableBind(sqlquery);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                category.EventId = Convert.ToInt32(dr["EventId"]);
                category.EventId = Convert.ToInt32(dr["EventId"]);
                category.EventName = Convert.ToString(dr["EventName"]);
            }
            return Json(category);
        }
        [HttpPost]
        public JsonResult DeleteCMS_DROPDOWN(int EventId)
        {
            Statement = "DELETE";
            sqlquery = "exec Sp_CMS_Dropdown_new '" + Statement + "'," + EventId + "";
            status = util.MultipleTransactions(sqlquery);
            if (status == "Successfull")
            {
                message = "Delete Successfull!!";
            }
            else
            {
                message = "Failed to Delete";
            }
            var Data = new { msg = message };
            return Json(Data);
        }
        #endregion

        #region OTPReport
        public IActionResult BindOTPReport()
        {
            sqlquery = "select  isnull(Memid,'') as Memid from TblOtp order by OtoId";
            ds = util.BindDropDown(sqlquery);
            List<SelectListItem> Categoryobj = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Categoryobj.Add(new SelectListItem { Text = dr["Memid"].ToString() });
            }
            ViewBag.Category = Categoryobj;
            return View(Categoryobj);
        }

     

        public IActionResult OTPReport()
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

        public JsonResult ShowOTPReport(string FromDate, string ToDate)
        {
            List<OTP> Category = new List<OTP>();
            sqlquery = "exec Sp_tbl_otp_report  '" + FromDate + "', '" + ToDate + "'";
            DataSet ds = util.TableBind(sqlquery);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Category.Add(new OTP
                {
                    OtoId = Convert.ToInt32(dr["OtoId"]),
                    Memid = Convert.ToString(dr["Memid"]),
                    CreateDate = Convert.ToDateTime(dr["CreateDate"].ToString()).ToString("dd/MM/yyyy hh:mm:ss"),
                    OtpNumber = Convert.ToInt32(dr["OtpNumber"]),
                });
            }
            return Json(Category);
        }
        #endregion





        #region MemberDetail
        public IActionResult MemberDetail()
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

        public JsonResult ShowMemberDetail(string memberid, string name)
        {
            List<MemberDetail> Category = new List<MemberDetail>();
            sqlquery = "exec Sp_TM_Member_report  '" + memberid + "', '" + name + "'";
            DataSet ds = util.TableBind(sqlquery);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Category.Add(new MemberDetail
                {
                    Membershipno = Convert.ToString(dr["Membershipno"]),
                    Name = Convert.ToString(dr["Name"]),
                    PermanentMobile = Convert.ToString(dr["PermanentMobile"]),
                    Email = Convert.ToString(dr["Email"]),
                });
            }
            return Json(Category);
        }
        #endregion

    }
}
