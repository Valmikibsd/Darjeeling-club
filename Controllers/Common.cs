using ClubApp.Models;
using ClubApp.Services;
using Indiastat.Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Nancy.Json;
using Newtonsoft.Json;
using PaymentHandlers;
using Razorpay.Api;
using SmartGatewayDotnetBackendApiKeyKit;
using Syncfusion.EJ2.Base;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.Net;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace ClubApp.Controllers
{
    public class CommonController : Controller
    {
      //  
        private IMemoryCache _cache;
        db_Utility Util = new db_Utility();
        ClsUtility ClsUtil = new ClsUtility();
        DBAccess db = new DBAccess();
        Dictionary<string, object> input = new Dictionary<string, object>();
        private CalendarManager _calendar = new CalendarManager();
        Payrequest.RootObject rt = new Payrequest.RootObject();
        Payrequest.MsgBdy mb = new Payrequest.MsgBdy();
        Payrequest.HeadDetails hd = new Payrequest.HeadDetails();
        // Payrequest.HeadDetails hd = new Payrequest.HeadDetails();
        Payrequest.MerchDetails md = new Payrequest.MerchDetails();
        Payrequest.PayDetails pd = new Payrequest.PayDetails();
        Payrequest.CustDetails cd = new Payrequest.CustDetails();
        Payrequest.Extras ex = new Payrequest.Extras();

        Payrequest.Payrequest pr = new Payrequest.Payrequest();
        public static PaymentHandlerConfig PaymentHandlerConfig { get; set; }
        public CommonController(IMemoryCache cache)
        {

            _cache = cache;
        }
        public IActionResult Index()
        {


            ViewBag.name = HttpContext.Request.Cookies["name"];
            if (ViewBag.name != null)
            {
                return RedirectToAction("home");
            }
            else
            {
                return RedirectToAction("Login");
            }

            return View();
        }

        public IActionResult memberentrnce()
        {
            ViewBag.name = HttpContext.Request.Cookies["name"];
            if (ViewBag.name != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public IActionResult clubameitie()
        {
            ViewBag.name = HttpContext.Request.Cookies["name"];
            if (ViewBag.name != null)
            {

                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }


        public IActionResult imagegallery(int id)
        {

            ViewBag.name = HttpContext.Request.Cookies["name"];
            if (ViewBag.name != null)
            {
                Myevent Myevent = new Myevent();
            List<Events> evnt = new List<Events>();
            DataTable dt = Util.GetSingleTable("exec Usp_gallery_id " + id + "", 20, Util.Clubstr);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    evnt.Add(new Events
                    {

                        image = dr["EventImage"].ToString()
                    });
                }
            }
            Myevent.Events = evnt;

            return View(Myevent);

                //return View();
            }
            else
            {
                return RedirectToAction("Login");
            }

           
        }
        public IActionResult imagegalleryevent(int id)
        {
            Myevent Myevent = new Myevent();
            List<Events> evnt = new List<Events>();
            DataTable dt = Util.GetSingleTable("exec Usp_event_id " + id + "", 20, Util.Clubstr);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    evnt.Add(new Events
                    {

                        image = dr["EventImage"].ToString()
                    });
                }
            }
            Myevent.Events = evnt;

            return View(Myevent);


            return View();
        }
        public IActionResult imagega()
        {
            Myevent Myevent = new Myevent();
            List<Events> evnt = new List<Events>();
            DataTable dt = Util.GetSingleTable("exec Usp_gallery_main", 20, Util.Clubstr);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    evnt.Add(new Events
                    {

                        image = dr["EventImage"].ToString(),
                        eventname = dr["name"].ToString(),
                        Id = Convert.ToInt32(dr["id"].ToString())
                    });
                }
            }
            Myevent.Events = evnt;

            return View(Myevent);


            //return View();
        }




        public IActionResult Login()
        {
            // ViewBag.name = HttpContext.Session.GetString("name");
            ViewBag.name = HttpContext.Request.Cookies["name"];
            if (ViewBag.name == null)
            {
                string sessionid = HttpContext.Session.Id;
                ViewBag.sessionid = sessionid;
                HttpContext.Session.SetString("sessionid", sessionid.ToString());
            }
            else
            {
                return RedirectToAction("home");
            }
            return View();
        }



        public IActionResult LoginOtp(string memid)
        {


            ViewBag.Email = HttpContext.Session.GetString("email");
            ViewBag.memid = HttpContext.Session.GetString("memid");
            ViewBag.sess = HttpContext.Session.GetString("sessionid");

            ViewBag.otpno = HttpContext.Session.GetString("otpnumber");
            ViewBag.pno = HttpContext.Session.GetString("pno");

            //string pmnd = TempData["pno"].ToString();// id will be 10;

            //ViewBag.Email = HttpContext.Request.Cookies["email"];
            //ViewBag.memid = HttpContext.Request.Cookies["memid"];
            // ViewBag.sess = HttpContext.Request.Cookies("sessionid");


            return View();
        }


        #region home       
        public IActionResult home()
        {

            Myevent Myevent = new Myevent();
            List<Events> evnt = new List<Events>();
            ViewBag.name = HttpContext.Request.Cookies["name"];

            if (ViewBag.name == null)
            {
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddDays(365);

                ViewBag.Email = HttpContext.Session.GetString("email");
                ViewBag.memid = HttpContext.Session.GetString("memid");
                ViewBag.name = HttpContext.Session.GetString("name");
                ViewBag.phone = HttpContext.Session.GetString("pno");

                Response.Cookies.Append("email", ViewBag.Email.ToString(), option);
                Response.Cookies.Append("memid", ViewBag.memid.ToString(), option);
                Response.Cookies.Append("name", ViewBag.name.ToString(), option);
                Response.Cookies.Append("pno", ViewBag.phone.ToString(), option);
            }
            else
            {

                ViewBag.Email = HttpContext.Request.Cookies["email"];
                ViewBag.memid = HttpContext.Request.Cookies["memid"];
                ViewBag.name = HttpContext.Request.Cookies["name"];

            }

            if (ViewBag.name != null)
            {
                ViewBag.name = ViewBag.name;
                ViewBag.PHOTO = ViewBag.memid + ".jpg";

                DataTable dt = Util.GetSingleTable("exec Usp_Events", 20, Util.Clubstr);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        evnt.Add(new Events
                        {
                            eventname = dr["EventName"].ToString(),
                            eventdate = dr["EventDate"].ToString(),
                            image = dr["EventImage"].ToString()
                        });
                    }
                }
                Myevent.Events = evnt;
            }
            else
            {
                return RedirectToAction("Login");
            }

            return View(Myevent);
        }
        [HttpPost]
        public string INSERTCAL(string PATRYNO, string AMOUNT)
        {
            string Message = string.Empty;
            string data = "";

            var sa = new JsonSerializerSettings();
            List<SelectListItem> list = new List<SelectListItem>();


            string MEMEID = HttpContext.Request.Cookies["memid"];


            data = db.MultipleTransactions("UPDATE tran_venue SET calflag=1 ,pay_status=3, calamts=" + AMOUNT + ",caluser='" + MEMEID + "',caldate=GETDATE(),calremark='BY MEMBER' WHERE partyno='" + PATRYNO + "' ");


            DataTable dtv = Util.execQuerydt("SELECT VenueName,name,convert(date,cast(VENUE_DATE as datetime),103)vdate,Totalvenueamt,FirstName +' '+LastName name1,email,tm.Memberidno,isnull(calamts,0)calamts,Totalvenueamt-isnull(calamts,0)cancelcghs FROM [dbo].[tran_venue] t,TYPE_VENUE v,tblVenueMaster m,[dbo].[TM_MemberDetail] tm where  t.venuetypeid=v.id and t.venueid=m.ID and  t.venueid=m.ID and memberid=tm.Memberidno and calflag=1 and    partyno='" + PATRYNO + "'");


            StringBuilder sBody = new StringBuilder();
            sBody.Append("<!DOCTYPE html><html><body>");

            sBody.Append("<p>Dear <b>" + dtv.Rows[0]["name1"].ToString() + "</b></p>");
            sBody.Append("<p>Greetings from darjeelinggymkhana club!</p>");
            sBody.Append("<p>Thanks for Venue Booking Cancel</p>");

            sBody.Append("<table  border='1' style=\"width:100%\"><tr><td colspan=\"2\" style=\"text-align:center;background-color:#000000;color:#ffffff;\">Venue Cancel Details </td></tr>");

            sBody.Append("<tr><td>Venue Name</td><td>" + dtv.Rows[0]["VenueName"].ToString() + "</td></tr>");
            sBody.Append("<tr><td>Venue Date</td><td>" + dtv.Rows[0]["vdate"].ToString() + "</td></tr>");
            sBody.Append("<tr><td>Venue Slot</td><td>" + dtv.Rows[0]["name"].ToString() + "</td></tr>");
            sBody.Append("<tr><td>Refund Amount</td><td>" + dtv.Rows[0]["calamts"].ToString() + "</td></tr>");
            sBody.Append("<tr><td>Cancel Amount</td><td>" + dtv.Rows[0]["cancelcghs"].ToString() + "</td></tr>");

            sBody.Append("</table>");

            sBody.Append("<p>Contact Person:</p>");
            sBody.Append("<p>Booking Manager</p>");
            sBody.Append("<p>Phone No-011-46525627</p>");
            sBody.Append("<p>Email-info@darjeelinggymkhanaclub.in</p>");
            sBody.Append("<p>Regards, </p>");
            sBody.Append("<b>Darjeelinggymkhanaclub.com </b></p>");
            sBody.Append("</body></html>");


            ///   string OtpStatus = ClsUtil.SendMailViaIIS_html("info@darjeelinggymkhanaclub.com", dtv.Rows[0]["email"].ToString(), "", "", "subscription pay amount", sBody.ToString(), null, "k20@aoA60", "mx2.mailguru.biz", "");


            string OtpStatus = ClsUtil.SendMailViaIIS_html("info@darjeelinggymkhanaclub.com", dtv.Rows[0]["email"].ToString(), "", "", "Venue Cancel", sBody.ToString(), null, "k20@aoA60", "mx2.mailguru.biz", "");

            //admin
            StringBuilder sBody1 = new StringBuilder();
            sBody1.Append("<!DOCTYPE html><html><body>");
            sBody1.Append("<p>Dear Darjeelinggymkhanaclub, " + Environment.NewLine);
            sBody1.Append("<p>Following has requested to venue booking <b>cancel</b>" + Environment.NewLine);

            sBody.Append("<table  border='1' style=\"width:100%\"><tr><td colspan=\"2\" style=\"text-align:center;background-color:#000000;color:#ffffff;\">Venue booking cancel Details <b>darjeelinggymkhanaclub.com</b></td></tr>");

            sBody1.Append("<tr><td>Member Name</td><td>" + dtv.Rows[0]["name1"].ToString() + '-' + dtv.Rows[0]["Memberidno"].ToString() + "</td></tr>");
            sBody1.Append("<tr><td>Venue Name</td><td>" + dtv.Rows[0]["VenueName"].ToString() + "</td></tr>");
            sBody1.Append("<tr><td>Venue Date</td><td>" + dtv.Rows[0]["vdate"].ToString() + "</td></tr>");
            sBody1.Append("<tr><td>Venue Slot</td><td>" + dtv.Rows[0]["name"].ToString() + "</td></tr>");
            sBody1.Append("<tr><td>Venue Amount</td><td>" + dtv.Rows[0]["Totalvenueamt"].ToString() + "</td></tr>");
            sBody1.Append("<tr><td>Refund Amount</td><td>" + dtv.Rows[0]["calamts"].ToString() + "</td></tr>");
            sBody1.Append("<tr><td>Cancel Amount</td><td>" + dtv.Rows[0]["cancelcghs"].ToString() + "</td></tr>");
            sBody1.Append("</table>");

            sBody1.Append("<p>Regards, </p>");
            sBody1.Append("<b>darjeelinggymkhanaclub.com </b></p>");
            sBody1.Append("</body></html>");


            string OtpStatus1 = ClsUtil.SendMailViaIIS_html("info@darjeelinggymkhanaclub.in", "info@darjeelinggymkhanaclub.in", "", "", "Venue Booking Cancel", sBody1.ToString(), null, "k20@aoA60", "mx2.mailguru.biz", "");



            return data;
        }

        public string insertdata(string datedata, string vanuid, string venetupeid, string amtoid)
        {
            string Message = string.Empty;
            string data = "";
            if (datedata == null)
            {
                DateTime dateAndTime = DateTime.Now.AddDays(0);
                datedata = dateAndTime.ToString("yyyy-MM-dd");

            }
            var sa = new JsonSerializerSettings();
            List<SelectListItem> list = new List<SelectListItem>();



            string patryno = DateTime.Now.ToString("ddMMyyyyHHmmss");


            DataTable dts = Util.execQuerydt("select *  from  [dbo].[tblVenueMaster] ts where id=" + vanuid + "");

            if (dts.Rows.Count > 0)
            {
                string rate;
                double cgst;
                double sgst;
                double totalamt;
                double totalgst;
                string memtype = "1";



                rate = dts.Rows[0]["RateForMember"].ToString();


                string accharge = dts.Rows[0]["AC_Charge"].ToString();
                string maintinaene = dts.Rows[0]["Maintenance_CHGS"].ToString();


                string csgt = Math.Round((((Convert.ToDouble(string.IsNullOrEmpty(rate) ? "0" : rate) + Convert.ToDouble(string.IsNullOrEmpty(accharge) ? "0" : accharge) + Convert.ToDouble(string.IsNullOrEmpty(maintinaene) ? "0" : maintinaene)) * 9) / 100), 0).ToString();


                string totalgsts = (Convert.ToDouble(csgt) + Convert.ToDouble(csgt)).ToString();

                string totalamts = (Convert.ToDouble(csgt) + Convert.ToDouble(csgt) + Convert.ToDouble(rate)).ToString();
                string memid = HttpContext.Request.Cookies["memid"].ToString();




                data = db.MultipleTransactions("Insert Into tran_venue(PartyNo, venueid, rental, SecurityCharge, AC_Charge, Maintenance_CHGS, curr_date, venuetypeid, cgst, sgst, flg, caldate, Totalvenueamt, totalvenuegst,memberid,orderid,taxind,pay_status,VENUE_DATE,PayMode) values('" + patryno + "'," + vanuid + "," + rate + ",0," + accharge + "," + maintinaene + ",getdate()," + venetupeid + "," + csgt + "," + csgt + "," + memtype + ",getdate()," + totalamts + "," + totalgsts + ",'" + memid + "','" + patryno + "','" + amtoid + "',1,CONVERT(DATE,'" + datedata + "',103),1)");



                HttpContext.Session.SetString("orderid", patryno.ToString());
                HttpContext.Session.SetString("amount", totalamts.ToString());
                HttpContext.Session.SetString("PARTYNO", patryno.ToString());

            }




            return data;
        }

        [HttpPost]
        public IActionResult Complete()
        {
            // Payment data comes in url so we have to get it from url

            //string IPAddressS = Request.;
            // if (IPAddressS == "::1")
            //   IPAddressS = "122.162.145.57";
            //EmailDetail adminemailDetail = new EmailDetail();
            // This id is razorpay unique payment id which can be use to get the payment details from razorpay server
            string paymentId = HttpContext.Request.Query["rzp_paymentid"];

            // This is orderId
            string orderId = HttpContext.Request.Query["rzp_orderid"];

            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("rzp_live_Bwy86Ja6TK3VTz", "ujtcW3K8zWem4DBjS3j9zRGH");

            Razorpay.Api.Payment payment = client.Payment.Fetch(paymentId);


            if (payment.Attributes["status"] == "captured")
            {
                string currency = payment.Attributes["currency"];
                string paymode = payment.Attributes["method"];
                ViewBag.order_id = orderId;
                ViewBag.tracking_id = paymentId;



                db.MultipleTransactions("update tran_venue set pay_status=1,taxind='" + paymentId + "' where orderid='" + orderId + "'  ;  ");

                string LanFor = "Thanks";
                string LanSec = "Online Payment";
                // Models.LanguageDetail languageDetail = new LanguageDetail();





                TempData["LNSGDATA"] = "";



                return RedirectToAction("Success");
            }
            else
            {
                return RedirectToAction("Failed");
            }
        }

        public static int GetWeekOfMonth(DateTime date)
        {
            DateTime beginningOfMonth = new DateTime(date.Year, date.Month, 1);
            while (date.Date.AddDays(1).DayOfWeek != DayOfWeek.Sunday)
                date = date.AddDays(1);
            return (int)Math.Truncate((double)date.Subtract(beginningOfMonth).TotalDays / 7f) + 1;
        }

        //translate each day to a day number for mapping to week

        public JsonResult AsyncUpdateCalender(int month, int year, int vanuid)
        {

            var model = _calendar.getCalender(month, year, vanuid);
            return Json(model);


        }
        public IActionResult partycalender(string venueid)
        {

            ViewBag.venueid = venueid;
            dynamic mymodel = new ExpandoObject();
            venuebooking Myevent = new venuebooking();
            List<venuebookingc> evnt = new List<venuebookingc>();
            DataTable dt = Util.GetSingleTable("exec Usp_venue", 20, Util.Clubstr);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["id"].ToString() == venueid)
                    {
                        ViewBag.name = dr["VenueName"].ToString();

                    }
                    evnt.Add(new venuebookingc
                    {
                        name = dr["VenueName"].ToString(),
                        Id = Convert.ToInt32(dr["id"].ToString()),
                        shortname = dr["ShortName"].ToString(),
                        memberrate = dr["RateForMember"].ToString(),
                        nonmemberrate = dr["RateForNonMember"].ToString(),
                        capcity = Convert.ToInt32(dr["CAPACITY"].ToString())
                    });
                }
            }
            Myevent.venuebookings = evnt;



            var model = _calendar.getCalender(DateTime.Now.Month, DateTime.Now.Year, Convert.ToInt32(venueid));

            mymodel.venuebookings = evnt;
            mymodel.WeekForMonth = model;

            return View(mymodel);
            //return View();
        }

        public IActionResult Logout()
        {

            foreach (var cookie in HttpContext.Request.Cookies)
            {
                Response.Cookies.Delete(cookie.Key);
            }

            return RedirectToAction("Login");


        }
        public IActionResult Success()
        {

            ViewBag.Thanks = "Thank you for venue booking.";
            return View();
        }
        public IActionResult Successcancel()
        {

            ViewBag.Thanks = "Your venue Cancel Complete.";
            return View();
        }
        public IActionResult aboutus()
        {

            DataTable dt = Util.execQuerydt("select * from [aboutus] where EventId=6 ", Util.Clubstr);

            ViewBag.descrip = dt.Rows[0][1].ToString();

            return View();
        }
        [HttpPost]
        public JsonResult IUContactUs(string Msg)
        {
            string message = "";
            string Statement = "INSERT";
            ViewBag.Email = HttpContext.Request.Cookies["email"];
            ViewBag.memid = HttpContext.Request.Cookies["memid"];
            ViewBag.name = HttpContext.Request.Cookies["name"];


            string sqlquery = "exec Sp_ContactForm '" + Statement + "'," + 0 + ",'" + ViewBag.name + "','','" + ViewBag.Email + "','" + Msg + "'";
            string status = db.MultipleTransactions(sqlquery);
            if (status == "Successfull")
            {
                message = "Contact Form added.";

                StringBuilder sBody1 = new StringBuilder();
                sBody1.Append("<!DOCTYPE html><html><body>");
                sBody1.Append("<p>Dear " + ViewBag.name + ", " + Environment.NewLine);
                sBody1.Append("<p>Thank you for submits query " + Environment.NewLine);
                sBody1.Append("<p> Club  contact  shortly with you." + Environment.NewLine);
                sBody1.Append("<p>Regards, </p>");
                sBody1.Append("<b>Darjeelinggymkhanaclub.com </b></p>");
                sBody1.Append("</body></html>");
                //  ViewBag.Email = "ajay@bsdinfotech.com";

                string OtpStatus = ClsUtil.SendMailViaIIS_html("info@darjeelinggymkhanaclub.in", ViewBag.Email, "", "", "Contactus", sBody1.ToString(), null, "k20@aoA60", "mx2.mailguru.biz", "");

                StringBuilder sBody = new StringBuilder();
                sBody.Append("<!DOCTYPE html><html><body>");
                sBody.Append("<p>Dear Darjeelinggymkhanaclub, " + Environment.NewLine);
                sBody.Append("<p>Member  query " + Environment.NewLine);


                sBody.Append("<table  border='1' style=\"width:100%\"><tr><td colspan=\"2\" style=\"text-align:center;background-color:#000000;color:#ffffff;\"> Contactus Details <b>darjeelinggymkhanaclub.com</b></td></tr>");

                sBody.Append("<tr><td> Name</td><td>" + ViewBag.name + "</td></tr>");
                sBody.Append("<tr><td>Email</td><td>" + ViewBag.Email + "</td></tr>");
                // sBody.Append("<tr><td>Mobile</td><td>" + MobileNo + "</td></tr>");
                sBody.Append("<tr><td>Message</td><td>" + Msg + "</td></tr>");

                sBody.Append("</table>");

                sBody.Append("<p>Regards, </p>");
                sBody.Append("<b>darjeelinggymkhanaclub.com </b></p>");
                sBody.Append("</body></html>");

                OtpStatus = ClsUtil.SendMailViaIIS_html("info@darjeelinggymkhanaclub.in", "info@darjeelinggymkhanaclub.in", "", "", "Contactus", sBody.ToString(), null, "k20@aoA60", "mx2.mailguru.biz", "");


                //    string OtpStatus1 = ClsUtil.SendMailViaIIS_html("Bookings@darjeelinggymkhanaclub.com", "ajay@bsdinfotech.com", "", "", "Venue Booking", sBody1.ToString(), null, "k20@aoA60", "mx2.mailguru.biz", "");

            }
            else
            {
                message = "Contact Form not added.";
            }
            var Data = new { message = message };
            return Json(Data);
        }
        public IActionResult Contactus()
        {

            //DataTable dt = Util.execQuerydt("select * from [aboutus] where EventId=1 ", Util.Clubstr);

            //ViewBag.descrip = dt.Rows[0][1].ToString();

            return View();
        }
        public IActionResult Failed()
        {

            ViewBag.Thanks = "Please try agian your payment failed.";
            return View();
        }

        public string razorpayment(string amount, string orderId)
        {
            //Set a name for the form
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

            double amt = Convert.ToDouble(amount) * 100;

            input.Add("amount", amt); // this amount should be same as transaction amount
            input.Add("currency", "INR");
            input.Add("receipt", "1212gg1dd");
            input.Add("payment_capture", "1");
            //input.Add("contact", "8587068357");
            //  input.Add("email", "ajay@bsdinfotech.com");

            string key = "rzp_live_9xiBaDsn0v3Tjm";
            // string key = "rzp_test_cBZVE4UXNgg1wp";
            string secret = "Ef6bMckvK4Z2Fj9Yccz8ER0Y";
            // string secret = "NvQvydcluXywksaz9cdz9PrS";

            RazorpayClient client = new RazorpayClient(key, secret);
            Razorpay.Api.Order order = client.Order.Create(input);


            string surl = "/Comman/UpcomingEvents";
            //  surl.Text = "" + Application["Mainpath"] + "OrderSuccess?RO=1";
            // furl.Text = "" + Application["Mainpath"] + "OrderSuccess?RO=1";
            string formID = "PostForm";
            //Build the form using the specified data to be posted.
            StringBuilder strForm = new StringBuilder();
            strForm.Append("<form id=\"" + formID + "\" name=\"" +
                           formID + "\" action=\"" + surl +
                           "\" method=\"POST\">");




            //Build the JavaScript which will do the Posting operation.
            StringBuilder strScript = new StringBuilder();

            string Email = HttpContext.Session.GetString("email");
            string memid = HttpContext.Session.GetString("memid");
            string name = HttpContext.Session.GetString("name");

            strScript.Append("<script language='javascript' src='https://checkout.razorpay.com/v1/checkout.js' " +
                "data-key='rzp_live_9xiBaDsn0v3Tjm'" +
                "data-amount='" + amt + "'  " +
                "data-name='Retailmall' " +
                "data-description='order booking' " +
                "data-order_id='" + order["id"].ToString() + "' " +
                "data-image='https://razorpay.com/favicon.png' " +
                "data-prefill.name='" + name + "'" +
                "data-prefill.email='" + Email + "' " +
                "data-prefill.contact='999' " +
                "data-theme.color='#F32524' >");




            strScript.Append("var v " + formID + " = document." +
                             formID + ";");
            strScript.Append("v " + formID + ".submit();");


            //   strScript.Append("alert()");
            strScript.Append("</script>");
            strScript.Append("</form>");

            strScript.Append("<script > window.onload = function() { $('.razorpay-payment-button').click();};</script>");

            //Page.Controls.Add(new LiteralControl(strForm.ToString() + strScript.ToString()));

            return (strForm.ToString() + strScript.ToString());
        }
        [HttpPost]
        public string BindSlotTime(string datedata, string vanuid)
        {
            string Message = string.Empty;
            if (datedata == null)
            {
                DateTime dateAndTime = DateTime.Now.AddDays(0);
                datedata = dateAndTime.ToString("dd-MM-yyyy");

            }
            var sa = new JsonSerializerSettings();
            List<SelectListItem> list = new List<SelectListItem>();
            var lst = db.ListAllTimeSlot(datedata, vanuid);
            string data = "";
            foreach (var item in lst)
            {
                data += "<option value=" + item.Value + ">" + item.Text.ToString() + "</option>";
                //list.Add(new SelectListItem { Text = item.Text.ToString(), Value = item.Value.ToString() });
            }
            return data;
        }

        [HttpPost]
        public string Usp_details(string billno, string type)
        {

            var sa = new JsonSerializerSettings();
            List<SelectListItem> list = new List<SelectListItem>();
            var lst = db.getdetils(billno, type);
            string data = "";
            foreach (var item in lst)
            {
                data += item.Text.ToString();
                //list.Add(new SelectListItem { Text = item.Text.ToString(), Value = item.Value.ToString() });
            }
            return data;
        }
        public string Bindbookfreeslot(string datedata, string vanuid)
        {
            string Message = string.Empty;
            if (datedata == null)
            {
                DateTime dateAndTime = DateTime.Now.AddDays(0);
                datedata = dateAndTime.ToString("yyyy-MM-dd");

            }
            var sa = new JsonSerializerSettings();
            List<SelectListItem> list = new List<SelectListItem>();
            var lst = db.ListAllTimeSlotnot(datedata, vanuid);
            string data = "";
            foreach (var item in lst)
            {
                data += item.Text.ToString() + "<br/>";
                //list.Add(new SelectListItem { Text = item.Text.ToString(), Value = item.Value.ToString() });
            }
            return data;
        }

        [HttpPost]
        public JsonResult AllBlockDateList()
        {
            string Message = string.Empty;
            var sa = new JsonSerializerSettings();

            var lst = db.GetAllBlockDate();
            if (lst.Count < 0)
            {
                Message = "Data Not Found.!";
                return Json(Message, sa);
            }
            else
            {
                return Json(lst, sa);
            }
        }

        public IActionResult CALLBACKAPI()
        {
            /// Request.Form["encResp"]
            //NameValueCollection nvc = Request.Form;
            byte[] iv = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            int iterations = 65536;
            int keysize = 256;

            string hashAlgorithm = "SHA1";
            string encdata = Request.Form["encData"];

            string passphrase1 = "75AEF0FA1B94B3C10D4F5B268F757F11";
            string salt1 = "75AEF0FA1B94B3C10D4F5B268F757F11";
            string Decryptval = ClsUtil.decrypt(encdata, passphrase1, salt1, iv, iterations);


            Payresponse.Rootobject root = new Payresponse.Rootobject();
            Payresponse.Parent objectres = new Payresponse.Parent();

            //    objectres = new JavaScriptSerializer().Deserialize<Payverify.Payverify>(Decryptval);

            objectres = new JavaScriptSerializer().Deserialize<Payresponse.Parent>(Decryptval);
            string message = objectres.payInstrument.responseDetails.message;

            //if (message == "SUCCESS")
            //{

            string amount = objectres.payInstrument.payDetails.amount;
            string BILNO = objectres.payInstrument.merchDetails.merchTxnId;
            string atomTxnId = objectres.payInstrument.payDetails.atomTxnId;
            string bankTxnId = objectres.payInstrument.payModeSpecificData.bankDetails.bankTxnId;

            db.MultipleTransactions("insert into TblResponse_details(atomid,banktransactionid,orderId,status,CreateDate)values('" + atomTxnId + "','" + bankTxnId + "','" + BILNO + "','" + message + "',getdate())");


            return View();
        }
        public IActionResult BookingResponse(OrderStatusViewModel ODERSTUST)
        {


            if (ODERSTUST.Message == "successfully")
            {
               
                db.MultipleTransactions("UPDATE tran_venue SET pay_status=1,taxind='" + ODERSTUST.paymentids + "', tentive_flg=0 WHERE PartyNo='" + ODERSTUST.ORDERID + "'");


                ViewBag.Thanks = "Thank you for venue booking.your booking no- " + ODERSTUST.ORDERID + "";



                DataTable dtv = Util.execQuerydt("SELECT VenueName,name,convert(date,cast(VENUE_DATE as datetime),103)vdate,Totalvenueamt,FirstName +' '+LastName name1,email,tm.Memberidno FROM [dbo].[tran_venue] t,TYPE_VENUE v,tblVenueMaster m,[dbo].[TM_MemberDetail] tm where  t.venuetypeid=v.id and t.venueid=m.ID and  t.venueid=m.ID and memberid=tm.Memberidno and   partyno='" + ODERSTUST.ORDERID + "'");


                StringBuilder sBody = new StringBuilder();
                sBody.Append("<!DOCTYPE html><html><body>");

                sBody.Append("<p>Dear <b>" + dtv.Rows[0]["name1"].ToString() + "</b></p>");
                sBody.Append("<p>Greetings from Darjeelinggymkhana club!</p>");
                sBody.Append("<p>Thank you for making your booking for " + dtv.Rows[0]["VenueName"].ToString() + ". Your booking number is " + ODERSTUST.ORDERID + ", below are the booking details </p>");

                sBody.Append("<table  border='1' style=\"width:100%\"><tr><td colspan=\"2\" style=\"text-align:center;background-color:#000000;color:#ffffff;\">Venue booking Details </td></tr>");

                sBody.Append("<tr><td>Venue Name</td><td>" + dtv.Rows[0]["VenueName"].ToString() + "</td></tr>");
                sBody.Append("<tr><td>Venue Date</td><td>" + Convert.ToDateTime(dtv.Rows[0]["vdate"]).ToShortDateString() + "</td></tr>");
                sBody.Append("<tr><td>Venue Slot</td><td>" + dtv.Rows[0]["name"].ToString() + "</td></tr>");
                sBody.Append("<tr><td>Venue Amount</td><td>" + dtv.Rows[0]["Totalvenueamt"].ToString() + "</td></tr>");
                sBody.Append("<tr><td>Paid Amount</td><td>" + dtv.Rows[0]["Totalvenueamt"].ToString() + "</td></tr>");
                sBody.Append("<tr><td>Transaction id</td><td>" + ODERSTUST.paymentids + "</td></tr>");
                sBody.Append("</table>");
                //  sBody.Append("<p>Your bank payment transaction id = '" + atomTxnId + "'</p>");
                // sBody.Append("<span style=\"font-style:italic\">For security reasons, you are requested to change the password while accessing the site  </span>");
                sBody.Append("<p>Contact Person:</p>");
                sBody.Append("<p>Booking Manager</p>");
               // sBody.Append("<p>Phone No-011-46525627</p>");
                sBody.Append("<p>Email-info@darjeelinggymkhanaclub.in</p>");
                // sBody.Append("<p>For any query please contact club booking office.</p>");
                sBody.Append("<p>Regards, </p>");
                sBody.Append("<b>Darjeelinggymkhanaclub.com </b></p>");
                sBody.Append("</body></html>");



                string OtpStatus = ClsUtil.SendMailViaIIS_html("info@darjeelinggymkhanaclub.in", dtv.Rows[0]["email"].ToString(), "", "", "Venue Booking", sBody.ToString(), null, "k20@aoA60", "mx2.mailguru.biz", "");

                //admin
                StringBuilder sBody1 = new StringBuilder();
                sBody1.Append("<!DOCTYPE html><html><body>");
                sBody1.Append("<p>Dear Darjeelinggymkhana club, " + Environment.NewLine);
                sBody1.Append("<p>Following has requested to venue booking <b>successful</b>" + Environment.NewLine);

                sBody1.Append("<table  border='1' style=\"width:100%\"><tr><td colspan=\"2\" style=\"text-align:center;background-color:#000000;color:#ffffff;\">Venue booking Details </td></tr>");

                sBody1.Append("<tr><td>Member Name</td><td>" + dtv.Rows[0]["name1"].ToString() + '-' + dtv.Rows[0]["Memberidno"].ToString() + "</td></tr>");
                sBody1.Append("<tr><td>Venue Name</td><td>" + dtv.Rows[0]["VenueName"].ToString() + "</td></tr>");
                sBody1.Append("<tr><td>Venue Date</td><td>" + dtv.Rows[0]["vdate"].ToString() + "</td></tr>");
                sBody1.Append("<tr><td>Venue Slot</td><td>" + dtv.Rows[0]["name"].ToString() + "</td></tr>");
                sBody1.Append("<tr><td>Venue Amount</td><td>" + dtv.Rows[0]["Totalvenueamt"].ToString() + "</td></tr>");
                sBody1.Append("</table>");

                sBody1.Append("<p>Regards, </p>");
                sBody1.Append("<b>Darjeelinggymkhanaclub.com </b></p>");
                sBody1.Append("</body></html>");


                string OtpStatus1 = ClsUtil.SendMailViaIIS_html("info@darjeelinggymkhanaclub.in", "info@darjeelinggymkhanaclub.in", "", "", "Venue Booking", sBody1.ToString(), null, "k20@aoA60", "mx2.mailguru.biz", "");

            }
            else
            {
                ViewBag.Thanks = "Your Payemnt has been failed please try again.";
            }

			return View();
        }
        public async Task<IActionResult> VenueBookingspayement(string datedata, string vanuid, string venetupeid)
        {
            string Email = HttpContext.Request.Cookies["email"];
            string memid = HttpContext.Request.Cookies["memid"];
            string name = HttpContext.Request.Cookies["name"];

            string orderId = $"V_{DateTime.Now.ToString("yyyyMMddHHmmss")}";
            string patryno = orderId;
            DataTable dts = Util.execQuerydt("select *  from  [dbo].[tblVenueMaster] ts where   id=" + vanuid + "");

            string totalamts = "0";
            if (dts.Rows.Count > 0)
            {
                string rate;
                double cgst;
                double sgst;
                double totalamt;
                double totalgst;
                string memtype = "1";
                rate = dts.Rows[0]["RateForMember"].ToString();


                string accharge = dts.Rows[0]["AC_Charge"].ToString();
                string maintinaene = dts.Rows[0]["Maintenance_CHGS"].ToString();


                string csgt = Math.Round((((Convert.ToDouble(string.IsNullOrEmpty(rate) ? "0" : rate) + Convert.ToDouble(string.IsNullOrEmpty(accharge) ? "0" : accharge) + Convert.ToDouble(string.IsNullOrEmpty(maintinaene) ? "0" : maintinaene)) * 9) / 100), 0).ToString();


                string totalgsts = (Convert.ToDouble(csgt) + Convert.ToDouble(csgt)).ToString();

                 totalamts = (Convert.ToDouble(csgt) + Convert.ToDouble(csgt) + Convert.ToDouble(rate)).ToString();

               string mash = db.MultipleTransactions("Insert Into tran_venue(PartyNo, venueid, rental, SecurityCharge, AC_Charge, Maintenance_CHGS, curr_date, venuetypeid, cgst, sgst, flg, caldate, Totalvenueamt, totalvenuegst,memberid,orderid,pay_status,VENUE_DATE) values('" + patryno + "'," + vanuid + "," + rate + ",0," + accharge + "," + maintinaene + ",getdate()," + venetupeid + "," + csgt + "," + csgt + "," + memtype + ",getdate()," + totalamts + "," + totalgsts + ",'" + memid + "','" + patryno + "',0,CONVERT(DATE,'" + datedata + "',103))");

            }




            List<venuebookingc> evnt = new List<venuebookingc>();
            venuebooking Myevent = new venuebooking();
            DataSet ds = Util.Fill("exec Usp_MYAccountDetails '" + memid + "'", Util.Clubstr);
          
            string phone = ds.Tables[0].Rows[0]["CurrentPhone"].ToString();
            string address = ds.Tables[0].Rows[0]["CurrentAddress"].ToString();


            ViewBag.EMAIL = Email;
            ViewBag.MOBILE = phone;

           
            int amount = new Random().Next(0, 100);
            string customerId = memid.ToString();
            PaymentHandler paymentHandler = new PaymentHandler();
            var sessionInput = new Dictionary<string, object>
                    {
                            { "amount", totalamts },
                            { "order_id", orderId },
                            { "customer_id", customerId },
                              { "customer_phone",  ViewBag.MOBILE.ToString() },
                               { "customer_email", ViewBag.EMAIL.ToString() },
                            { "payment_page_client_id", paymentHandler.paymentHandlerConfig.PAYMENT_PAGE_CLIENT_ID },
                            { "action", "paymentPage" },
                            { "return_url", "https://memberlogin.darjeelinggymkhanaclub.in/Common/handlePaymentResponse" }
                    };
            var orderSession = await paymentHandler.OrderSession(sessionInput);
            // block:end:session-function
            if (orderSession?.payment_links?.web != null) return Redirect((string)orderSession.payment_links.web);
            throw new Exception("Invalid Response unable to find web payment link");

            //DataTable dt = Util.GetSingleTable("exec Usp_venue", 20, Util.Clubstr);
            //if (dt.Rows.Count > 0)
            //{
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        evnt.Add(new venuebookingc
            //        {
            //            name = dr["VenueName"].ToString(),
            //            Id = Convert.ToInt32(dr["id"].ToString()),
            //            shortname = dr["ShortName"].ToString(),
            //            memberrate = dr["RateForMember"].ToString(),
            //            nonmemberrate = dr["RateForNonMember"].ToString(),
            //            capcity = Convert.ToInt32(dr["CAPACITY"].ToString())
            //        });
            //    }
            //}
            //Myevent.venuebookings = evnt;
          
            //  DateTime dateAndTime = DateTime.Now.AddDays(0);
            //Myevent.Date = dateAndTime.ToString("yyyy-MM-dd");

           // return View(Myevent);
        }
        public IActionResult VenueBookingbycash(string datedata, string vanuid, string venetupeid)
        {
            string Email = HttpContext.Request.Cookies["email"];
            string memid = HttpContext.Request.Cookies["memid"];
            string name = HttpContext.Request.Cookies["name"];


            DataTable dtsss = Util.execQuerydt("select * from [dbo].[tran_venue] t where   [venueid]=" + vanuid + " and t.venuetypeid=" + venetupeid + " and pay_status=1   and  convert(date, cast( VENUE_DATE as datetime),103)= convert(date,'"+ datedata + "',103)");
            if (dtsss.Rows.Count == 0)
            {

                string patryno = DateTime.Now.ToString("ddMMyyyyHHmmss");
            DataTable dts = Util.execQuerydt("select *  from  [dbo].[tblVenueMaster] ts where  id=" + vanuid + "");

            string totalamts = "0";
                if (dts.Rows.Count > 0)
                {
                    string rate;
                    double cgst;
                    double sgst;
                    double totalamt;
                    double totalgst;
                    string memtype = "1";
                    rate = dts.Rows[0]["RateForMember"].ToString();


                    string accharge = dts.Rows[0]["AC_Charge"].ToString();
                    string maintinaene = dts.Rows[0]["Maintenance_CHGS"].ToString();


                    string csgt = Math.Round((((Convert.ToDouble(string.IsNullOrEmpty(rate) ? "0" : rate) + Convert.ToDouble(string.IsNullOrEmpty(accharge) ? "0" : accharge) + Convert.ToDouble(string.IsNullOrEmpty(maintinaene) ? "0" : maintinaene)) * 9) / 100), 0).ToString();


                    string totalgsts = (Convert.ToDouble(csgt) + Convert.ToDouble(csgt)).ToString();

                    totalamts = (Convert.ToDouble(csgt) + Convert.ToDouble(csgt) + Convert.ToDouble(rate)).ToString();

                    string mash = db.MultipleTransactions("Insert Into tran_venue(PartyNo, venueid, rental, SecurityCharge, AC_Charge, Maintenance_CHGS, curr_date, venuetypeid, cgst, sgst, flg, caldate, Totalvenueamt, totalvenuegst,memberid,orderid,pay_status,VENUE_DATE,PayMode,tentive_flg) values('" + patryno + "'," + vanuid + "," + rate + ",0," + accharge + "," + maintinaene + ",getdate()," + venetupeid + "," + csgt + "," + csgt + "," + memtype + ",getdate()," + totalamts + "," + totalgsts + ",'" + memid + "','" + patryno + "',1,CONVERT(DATE,'" + datedata + "',103),2,1)");



                    if (mash == "Successfull")

                    {

                        List<venuebookingc> evnt = new List<venuebookingc>();
                        venuebooking Myevent = new venuebooking();
                        DataSet ds = Util.Fill("exec Usp_MYAccountDetails '" + memid + "'", Util.Clubstr);

                        string phone = ds.Tables[0].Rows[0]["CurrentPhone"].ToString();
                        string address = ds.Tables[0].Rows[0]["CurrentAddress"].ToString();


                        ViewBag.EMAIL = Email;
                        ViewBag.MOBILE = phone;



                        DataTable dtv = Util.execQuerydt("SELECT VenueName,name,convert(date,cast(VENUE_DATE as datetime),103)vdate,Totalvenueamt,FirstName +' '+LastName name1,email,tm.Memberidno FROM [dbo].[tran_venue] t,TYPE_VENUE v,tblVenueMaster m,[dbo].[TM_MemberDetail] tm where  t.venuetypeid=v.id and t.venueid=m.ID and  t.venueid=m.ID and memberid=tm.Memberidno and venuetypeid=" + venetupeid + " and venueid=" + vanuid + " and    partyno='" + patryno + "'");


                        StringBuilder sBody = new StringBuilder();
                        sBody.Append("<!DOCTYPE html><html><body>");

                        sBody.Append("<p>Dear <b>" + dtv.Rows[0]["name1"].ToString() + "</b></p>");
                        sBody.Append("<p>Greetings from darjeelinggymkhana club!</p>");
                        sBody.Append("<p>Thank you for making your booking for " + dtv.Rows[0]["VenueName"].ToString() + ". Your booking number is " + patryno + ", below are the booking details </p>");

                        sBody.Append("<table  border='1' style=\"width:100%\"><tr><td colspan=\"2\" style=\"text-align:center;background-color:#000000;color:#ffffff;\">Venue booking Details </td></tr>");

                        sBody.Append("<tr><td>Venue Name</td><td>" + dtv.Rows[0]["VenueName"].ToString() + "</td></tr>");
                        sBody.Append("<tr><td>Venue Date</td><td>" + dtv.Rows[0]["vdate"].ToString() + "</td></tr>");
                        sBody.Append("<tr><td>Venue Slot</td><td>" + dtv.Rows[0]["name"].ToString() + "</td></tr>");
                        sBody.Append("<tr><td>Venue Amount</td><td>" + dtv.Rows[0]["Totalvenueamt"].ToString() + "</td></tr>");
                        sBody.Append("<tr><td>Paid Amount</td><td>0</td></tr>");
                        sBody.Append("<tr><td>Balance Amount</td><td>" + dtv.Rows[0]["Totalvenueamt"].ToString() + "</td></tr>");
                        sBody.Append("</table>");

                        sBody.Append("<span style=\"font-style:italic\">(NOTE:Confrimation of Boooking is subject to payment made within 48 hours of the booking. if payment is not made ,booking shall cancelled automactically.)  </span>");
                        sBody.Append("<p>Contact Person:</p>");
                        sBody.Append("<p>Booking Manager</p>");
                        sBody.Append("<p>Phone No-011-46525627</p>");
                        sBody.Append("<p>Email-info@darjeelinggymkhanaclub.in</p>");
                        sBody.Append("<p>Regards, </p>");
                        sBody.Append("<b>Darjeelinggymkhanaclub.com </b></p>");
                        sBody.Append("</body></html>");



                          string OtpStatus = ClsUtil.SendMailViaIIS_html("info@darjeelinggymkhanaclub.in", dtv.Rows[0]["email"].ToString(), "", "", "Venue Booking by cash", sBody.ToString(), null, "k20@aoA60", "mx2.mailguru.biz", "");
                   

                        //admin
                        StringBuilder sBody1 = new StringBuilder();
                        sBody1.Append("<!DOCTYPE html><html><body>");
                        sBody1.Append("<p>Dear Darjeelinggymkhana club, " + Environment.NewLine);
                        sBody1.Append("<p>Following has requested to venue booking BY CASH <b>successful</b>" + Environment.NewLine);

                        sBody1.Append("<table  border='1' style=\"width:100%\"><tr><td colspan=\"2\" style=\"text-align:center;background-color:#000000;color:#ffffff;\">Venue booking Details </td></tr>");

                        sBody1.Append("<tr><td>Member Name</td><td>" + dtv.Rows[0]["name1"].ToString() + '-' + dtv.Rows[0]["Memberidno"].ToString() + "</td></tr>");
                        sBody1.Append("<tr><td>Venue Name</td><td>" + dtv.Rows[0]["VenueName"].ToString() + "</td></tr>");
                        sBody1.Append("<tr><td>Venue Date</td><td>" + dtv.Rows[0]["vdate"].ToString() + "</td></tr>");
                        sBody1.Append("<tr><td>Venue Slot</td><td>" + dtv.Rows[0]["name"].ToString() + "</td></tr>");
                        sBody1.Append("<tr><td>Venue Amount</td><td>" + dtv.Rows[0]["Totalvenueamt"].ToString() + "</td></tr>");
                        sBody1.Append("</table>");

                        sBody1.Append("<p>Regards, </p>");
                        sBody1.Append("<b>Darjeelinggymkhanaclub.com </b></p>");
                        sBody1.Append("</body></html>");


                      string OtpStatus1 = ClsUtil.SendMailViaIIS_html("info@darjeelinggymkhanaclub.in", "info@darjeelinggymkhanaclub.in", "", "", "Venue Booking by cash", sBody1.ToString(), null, "k20@aoA60", "mx2.mailguru.biz", "");

                        ViewBag.Thanks = "Thank you for venue booking.your booking no- " + patryno + "";

                    }
                }
            }
            else
            {
                return RedirectToAction("home");
            }



            return View();
        }
        
        #region HolidayClubGroupMaster
        public IActionResult ClubGroupMaster()
        {

            ViewModel viewModels = new ViewModel();
            List<ItemGroups> Itemgroup = new List<ItemGroups>();
            List<ItemSubGroups> subGroups = new List<ItemSubGroups>();
            List<ItemMaster> itemMasters = new List<ItemMaster>();
           string sqlquery = "exec Usp_Item_Sub_Master_Group";
           DataSet ds = Util.Fill(sqlquery, Util.Clubstr);

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Itemgroup.Add(new ItemGroups
                    {
                        ItemGroupCode = Convert.ToInt32(row["ItemGroupCode"]),
                        ItemGroup = Convert.ToString(row["ItemGroup"]),

                    });
                }
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[1].Rows)
                {
                    subGroups.Add(new ItemSubGroups
                    {
                        Itemsubgroupcode = Convert.ToInt32(row["Itemsubgroupcode"]),
                        ItemGroupcode = Convert.ToInt32(row["ItemGroupcode"]),
                        Itemsubgroup = Convert.ToString(row["Itemsubgroup"]),
                    });
                }
            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[2].Rows)
                {
                    itemMasters.Add(new ItemMaster
                    {
                        itemId = Convert.ToInt32(row["itemId"]),
                        ItemGroupcode = Convert.ToInt32(row["ItemGroupcode"]),
                        Itemsubgroupcode = Convert.ToInt32(row["Itemsubgroupcode"]),
                        ItemName = Convert.ToString(row["Itemname"]),
                        ItemPrice = Convert.ToString(row["ItemPrice"]),
                        Item_Image = Convert.ToString(row["Item_Image"]),
                    });
                }
            }

            viewModels.SubGroups = subGroups;
            viewModels.Master = itemMasters;
            viewModels.Groups = Itemgroup;
            return View(viewModels);
        }
        #endregion
        #region HolidayClubGroupMaster
        public IActionResult ClubGroupMasterBAR()
        {

            ViewModel viewModels = new ViewModel();
            List<ItemGroups> Itemgroup = new List<ItemGroups>();
            List<ItemSubGroups> subGroups = new List<ItemSubGroups>();
            List<ItemMaster> itemMasters = new List<ItemMaster>();
            string sqlquery = "exec Usp_Item_Sub_Master_GroupBAR";
            DataSet ds = Util.Fill(sqlquery, Util.Clubstr);

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Itemgroup.Add(new ItemGroups
                    {
                        ItemGroupCode = Convert.ToInt32(row["ItemGroupCode"]),
                        ItemGroup = Convert.ToString(row["ItemGroup"]),

                    });
                }
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[1].Rows)
                {
                    subGroups.Add(new ItemSubGroups
                    {
                        Itemsubgroupcode = Convert.ToInt32(row["Itemsubgroupcode"]),
                        ItemGroupcode = Convert.ToInt32(row["ItemGroupcode"]),
                        Itemsubgroup = Convert.ToString(row["Itemsubgroup"]),
                    });
                }
            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[2].Rows)
                {
                    itemMasters.Add(new ItemMaster
                    {
                        itemId = Convert.ToInt32(row["itemId"]),
                        ItemGroupcode = Convert.ToInt32(row["ItemGroupcode"]),
                        Itemsubgroupcode = Convert.ToInt32(row["Itemsubgroupcode"]),
                        ItemName = Convert.ToString(row["Itemname"]),
                        ItemPrice = Convert.ToString(row["ItemPrice"]),
                        Item_Image = Convert.ToString(row["Item_Image"]),
                    });
                }
            }

            viewModels.SubGroups = subGroups;
            viewModels.Master = itemMasters;
            viewModels.Groups = Itemgroup;
            return View(viewModels);
        }
        #endregion

        public IActionResult VenueBookings()
        {

            ViewBag.name = HttpContext.Request.Cookies["name"];

            if (ViewBag.name != null)
            {
                //ViewBag.name = HttpContext.Request.Cookies["name"];
                ViewBag.PHOTO = HttpContext.Request.Cookies["memid"] + ".jpg";
                venuebooking Myevent = new venuebooking();
                List<venuebookingc> evnt = new List<venuebookingc>();
                DataTable dt = Util.GetSingleTable("exec Usp_venue", 20, Util.Clubstr);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        evnt.Add(new venuebookingc
                        {
                            name = dr["VenueName"].ToString(),
                            Id = Convert.ToInt32(dr["id"].ToString()),
                            shortname = dr["ShortName"].ToString(),
                            memberrate = dr["RateForMember"].ToString(),
                            nonmemberrate = dr["RateForNonMember"].ToString(),
                            photo = dr["photo"].ToString(),
                            capcity = Convert.ToInt32(dr["CAPACITY"].ToString())
                        });
                    }
                }
                Myevent.venuebookings = evnt;
                //  DateTime dateAndTime = DateTime.Now.AddDays(0);
                //Myevent.Date = dateAndTime.ToString("yyyy-MM-dd");

                return View(Myevent);
            }
            else
            {
                return RedirectToAction("login");
            }
        }
        public IActionResult passevent()
        {
            ViewBag.name = HttpContext.Request.Cookies["name"];
            ViewBag.PHOTO = HttpContext.Request.Cookies["memid"] + ".jpg";
            Myevent Myevent = new Myevent();
            List<Events> evnt = new List<Events>();
            DataTable dt = Util.GetSingleTable("exec Usp_Events_pass", 20, Util.Clubstr);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    evnt.Add(new Events
                    {
                        eventname = dr["EventName"].ToString(),
                        eventdate = dr["EventDate"].ToString(),
                        image = dr["EventImage"].ToString()
                    });
                }
            }
            Myevent.Events = evnt;

            return View(Myevent);
        }
        public IActionResult UpcomingEvents()
        {
            ViewBag.name = HttpContext.Request.Cookies["name"];
            ViewBag.PHOTO = HttpContext.Request.Cookies["memid"] + ".jpg";
            Myevent Myevent = new Myevent();
            List<Events> evnt = new List<Events>();
            DataTable dt = Util.GetSingleTable("exec Usp_Events", 20, Util.Clubstr);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    evnt.Add(new Events
                    {
                        eventname = dr["EventName"].ToString(),
                        eventdate = dr["EventDate"].ToString(),
                        Id    = Convert.ToInt32( dr["id"].ToString()),
                        image = dr["EventImage"].ToString()
                    });
                }
            }
            Myevent.Events = evnt;

            return View(Myevent);
        }
        #endregion

        #region ManagingCommittee       
        public IActionResult ManagingCommittee()
        {
            Mybills bills = new Mybills();
            List<managingcommitee> subs = new List<managingcommitee>();

            DataTable dt = Util.execQuerydt("select name, Designation,photo,replace(isnull(DESCRIPTION,''),'&amp;','&')DESCRIPTION from [dbo].[TblManagingCommitte] order by OrderBy asc", Util.Clubstr);

            foreach (DataRow row in dt.Rows)
            {
                subs.Add(new managingcommitee { name = row["name"].ToString(), digination = row["Designation"].ToString(), photo = row["photo"].ToString(),DESCRIPTION= row["DESCRIPTION"].ToString() });
            }

            bills.maging = subs;

            return View(bills);
        }
        public IActionResult Affiliatedclub()
        {
            Mybills bills = new Mybills();
            List<affiliated> subs = new List<affiliated>();

            DataTable dt = Util.execQuerydt("select* from [APP_tblAffiliatedClub] where status=1", Util.Clubstr);

            foreach (DataRow row in dt.Rows)
            {
                subs.Add(new affiliated { clubname = row["ClubName"].ToString(), addresss = row["Address"].ToString(), email = row["Email"].ToString(), phone = row["Contact"].ToString() });
            }

            bills.affilied = subs;

            return View(bills);
        }
        #endregion


        #region MyAccount   
        
        public IActionResult MyAccount()
        {
            MyaccDetails myacc = new MyaccDetails();
            SpouseDetails spouse = new SpouseDetails();
            List<DependentDetails> depdeta = new List<DependentDetails>();
            string Memid = HttpContext.Request.Cookies["memid"];

            ViewBag.name = HttpContext.Request.Cookies["name"];
            if (ViewBag.name != null)
            {
                //HttpContext.Session.GetString("memid");
                DataSet ds = Util.Fill("exec Usp_MYAccountDetails '" + Memid + "'", Util.Clubstr);
                if (ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[1].Rows)
                    {
                        depdeta.Add(new DependentDetails { depenName = row["DEPENDANTNAME"].ToString(), dob = row["DEPENDANTDOB"].ToString(), gender = row["Gender"].ToString() });
                    }
                }

                if (ds.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[2].Rows)
                    {
                        spouse.spouseName = row["SpouseName"].ToString();
                        spouse.dob = row["SpouseDob"].ToString();
                        spouse.SpouseIdNo = row["SpouseIdNo"].ToString();
                        spouse.MariageAniversary = row["MariageAniversary"].ToString();
                        spouse.PHOTO = row["SpouseIdNo"].ToString() + ".jpg";
                    }
                }



                myacc.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                myacc.Dob = ds.Tables[0].Rows[0]["Dob"].ToString();
                myacc.phone = ds.Tables[0].Rows[0]["CurrentPhone"].ToString();
                myacc.address = ds.Tables[0].Rows[0]["CurrentAddress"].ToString();
                myacc.email = ds.Tables[0].Rows[0]["email"].ToString();
                myacc.PHOTO = ds.Tables[0].Rows[0]["MEMBERIDNO"].ToString() + ".jpg";
                myacc.memid = ds.Tables[0].Rows[0]["MEMBERIDNO"].ToString();
                //myacc.MariageAniversary = ds.Tables[0].Rows[0]["MariageAniversary"].ToString();

                myacc.Spouse = spouse;
                myacc.depdetails = depdeta;
            }
            else
            {
                return RedirectToAction("Login");
            }
            return View(myacc);
        }
        #endregion

        [NonAction]
        public bool ValidateHMAC(Dictionary<string, string> input)
        {
            return Utility.ValidateHMAC_SHA256(input, PaymentHandlerConfig.Instance.RESPONSE_KEY);
        }
        // block:end:construct-params

        // block:start:order-status-function
        [NonAction]
        public Task<dynamic> GetOrder(string orderId)
        {
            PaymentHandler paymentHandler = new PaymentHandler();
            return paymentHandler.OrderStatus(orderId);
        }
       

       
        // block:end:order-status-function
        [NonAction]
        public async Task<IActionResult> HandleJuspayResponse()
        {
            string Memid = HttpContext.Request.Cookies["memid"];
            try
            {
               
                string orderId = HttpContext.Request.Form["order_id"];
                string status = HttpContext.Request.Form["status"];
                string signature = HttpContext.Request.Form["signature"];
                string statusId = HttpContext.Request.Form["status_id"];
                if (orderId == null || status == null || signature == null || statusId == null) return RedirectToAction("Failed"); 
                Dictionary<string, string> RequestParams = new Dictionary<string, string> { { "order_id", orderId }, { "status", status }, { "status_id", statusId }, { "signature", signature }, { "signature_algorithm", "HMAC-SHA256" } };
                if (ValidateHMAC(RequestParams))
                {
                    var order = await GetOrder(orderId);
                    string message = null;
                    switch ((string)order.status)
                    {
                        case "CHARGED":
                            message = "successfully";
                            break;
                        case "PENDING":
                        case "PENDING_VBV":
                            message = "order payment pending";
                            break;
                        case "AUTHENTICATION_FAILED":
                            message = "authentication failed";
                            break;
                        case "AUTHORIZATION_FAILED":
                            message = "order payment authorization failed";
                            break;
                        default:
                            message = $"order status {order.status}";
                            break;
                    }

                    Dictionary<string, string> orderResponse = Utils.FlattenJson(order);
                    String[] ordertype = orderId.ToString().Split("_");
                    if (ordertype[0] == "C")
                    {
                        return RedirectToAction("CardRechargeresponse", new OrderStatusViewModel
                        {
                            ORDERID = orderId,
                            Message = message,
                            amt = order.amount,
                            paymentids = order.txn_id,
                            payment_method_type = order.payment_method_type
                            //paymentids= order.

                        });
                    }
                    else if (ordertype[0] == "V")
                    {
                        return RedirectToAction("BookingResponse", new OrderStatusViewModel
                        {
                            ORDERID = orderId,
                            Message = message,
                            amt = order.amount,
                            paymentids = order.txn_id,
                            payment_method_type = order.payment_method_type
                            //paymentids= order.

                        });

                    }
                    else
                    {
                        return RedirectToAction("BILLResponse", new OrderStatusViewModel
                        {
                            ORDERID = orderId,
                            Message = message,
                            amt = order.amount,
                            paymentids = order.txn_id,
                            payment_method_type = order.payment_method_type,
                           custid = order.customer_id.ToString()
                            //paymentids= order.

                        }); ;

                    }
                }
                else
                {
                    throw new Exception($"Signature Verification failed");
                }
            }
            catch (Exception Ex)
            {
                return RedirectToAction("Error");


            }
        }
        [HttpGet("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet("Common/HandlePaymentResponse")]
        public Task<IActionResult> Get()
        {
            
            return HandleJuspayResponse();
        }

        [HttpPost("Common/HandlePaymentResponse")]
        public Task<IActionResult> Post()
        {
           
            return HandleJuspayResponse();
        }
        public IActionResult CardRechargeresponse(OrderStatusViewModel ODERSTUST )
        {
           
            string Memid = HttpContext.Request.Cookies["memid"];

            try
            {
                

                if (ODERSTUST.Message == "successfully")
                {


                    DataTable dtvS = Util.execQuerydt("select * from [CARD_RECHARGE] tm where order_id ='" + ODERSTUST.ORDERID + "'");
                    if (dtvS.Rows.Count >0)
                    {
                        ViewBag.Thanks = "Your Payment has been already paid.";

                    }
                    else
                    {

                        DataTable dtv = Util.execQuerydt("select Memberidno memberid,FirstName +' '+LastName name,email  from [TM_MemberDetail] tm where tm.Memberidno ='" + Memid + "'");

                        db.MultipleTransactions("INSERT INTO [dbo].[CARD_RECHARGE]([memberid],[amount],[Recharge_Date],[TAXID],order_id)VALUES('" + Memid + "'," + ODERSTUST.amt + ",GETDATE(),'" + ODERSTUST.paymentids + "','" + ODERSTUST.ORDERID + "')");


                        StringBuilder sBody = new StringBuilder();
                        sBody.Append("<!DOCTYPE html><html><body>");

                        sBody.Append("<p>Dear <b>" + dtv.Rows[0]["name"].ToString() + "</b></p>");
                        sBody.Append("<p>Greetings from Darjeelinggymkhana Club!</p>");
                        sBody.Append("<p>Thanks for pay Card recharge Amount = " + ODERSTUST.amt + "</p>");
                        sBody.Append("<p>Your  bank payment transaction id = " + ODERSTUST.paymentids + "</p>");

                        sBody.Append("<p>Regards, </p>");
                        sBody.Append("<b>Darjeelinggymkhanaclub.com </b></p>");
                        sBody.Append("</body></html>");

                        string OtpStatus = ClsUtil.SendMailViaIIS_html("info@darjeelinggymkhanaclub.com", dtv.Rows[0]["email"].ToString(), "", "", "Card Recharge  amount", sBody.ToString(), null, "k20@aoA60", "mx2.mailguru.biz", "");
                        //admin

                        StringBuilder sBody1 = new StringBuilder();
                        sBody1.Append("<!DOCTYPE html><html><body>");
                        sBody1.Append("<p>Dear Darjeelinggymkhanaclub, " + Environment.NewLine);
                        sBody1.Append("<p>Following has requested to pay the Card Recharge Amount <b>successful</b>" + Environment.NewLine);

                        sBody1.Append("<table  border='1' style=\"width:100%\"><tr><td colspan=\"2\" style=\"text-align:center;background-color:#000000;color:#ffffff;\">Card Recharge Amount Details <b>darjeelinggymkhanaclub.com</b></td></tr>");

                        sBody1.Append("<tr><td>Name</td><td>" + dtv.Rows[0]["name"].ToString() + '-' + dtv.Rows[0]["memberid"].ToString() + "</td></tr>");
                        sBody1.Append("<tr><td>Recharge Amount</td><td>" + ODERSTUST.amt + "</td></tr>");
                        sBody1.Append("<tr><td>Billno</td><td>" + ODERSTUST.ORDERID + "</td></tr>");
                        sBody1.Append("<tr><td>Atomid</td><td>" + ODERSTUST.paymentids + "</td></tr>");

                        sBody1.Append("</table>");

                        sBody1.Append("<p>Regards, </p>");
                        sBody1.Append("<b>Darjeelinggymkhanaclub.com </b></p>");
                        sBody1.Append("</body></html>");


                        string OtpStatus1 = ClsUtil.SendMailViaIIS_html("info@darjeelinggymkhanaclub.in", "ajay@bsdinfotech.com", "", "", "Card Recharge amount", sBody1.ToString(), null, "k20@aoA60", "mx2.mailguru.biz", "");

                        ViewBag.Thanks = "Thank you for Card Recharge and your transaction id-" + ODERSTUST.paymentids + "";
                    }

                }
                else
                {
                    ViewBag.Thanks = "Your Payment has been failed please try again.";
                }
            }
            catch (Exception Ex)
            {
                ViewBag.Thanks = "Your Payment has been failed please try again.";
                //return View("Error");
            }




            return View();
        }
        public IActionResult Myvenue()
        {

            Mybills bills = new Mybills();
            List<MYVENUEB> subs = new List<MYVENUEB>();
            DataTable ddy = new DataTable();
            double venvueamount = 0;
            double menuamounts = 0;
            double totrantel = 0;
            double cancelsgst = 0;
            double cancelcgst = 0;
            string orinalefund = "0";
         
            ViewBag.name = HttpContext.Request.Cookies["name"];
           

            if (ViewBag.name != null)
            {
                string Memid = HttpContext.Request.Cookies["memid"];
                DataSet ds = Util.Fill("exec Usp_MYAccountDetails '" + Memid + "'", Util.Clubstr);

            string phone = ds.Tables[0].Rows[0]["CurrentPhone"].ToString();
            string Email = HttpContext.Request.Cookies["email"];

            ViewBag.EMAIL = Email;
            ViewBag.MOBILE = phone;

                DataTable dt = Util.execQuerydt("select VenueName,name,partyno,totalvenueamt,convert(date,cast(VENUE_DATE as datetime),103) venue_date,curr_date,RENTAL,CASE WHEN calflag=0 THEN 'Book' else 'Cancel' END STATUS,isnull(calamts,0)calamts, isnull(tentive_flg,0)tentive_flg  from [dbo].[tran_venue] t,[dbo].[tblVenueMaster]tv, TYPE_VENUE v where tv.id=t.venueid and v.id=t.venuetypeid and pay_status<>0  and memberid='" + Memid + "' ", Util.Clubstr);

                foreach (DataRow row in dt.Rows)
                {
                    orinalefund = "0";
                    DateTime f = DateTime.Now;
                    System.DateTime s = Convert.ToDateTime(row["venue_date"].ToString());

                    if (f <= s)
                    {
                        String diff2 = Math.Round((s - f).TotalDays, 0).ToString();


                        ddy = Util.SelectParticular("Cancel_rules", "*", " " + diff2.Replace("-", "") + " between dayfrom and dayto  and ref_flg=0");
                        if (ddy.Rows.Count > 0)
                        {
                            string dd = ddy.Rows[0]["dedectiontype"].ToString();

                            if (dd == "1")
                            {
                                venvueamount = ((Convert.ToDouble(row["RENTAL"].ToString()) * Convert.ToDouble(ddy.Rows[0]["Pervenuedec"].ToString())) / 100);
                            }
                            double amsmmd = Math.Round((venvueamount + menuamounts), 0);

                            cancelsgst = ((amsmmd * 9) / 100);
                            cancelcgst = ((amsmmd * 9) / 100);

                            string refundamt = Math.Round((amsmmd + cancelsgst + cancelcgst), 0).ToString();
                            orinalefund = (Convert.ToDouble(row["totalvenueamt"].ToString()) - Convert.ToDouble(refundamt)).ToString();

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

                    subs.Add(new MYVENUEB { VenueName = row["VenueName"].ToString(), name = row["name"].ToString(), partyno = row["partyno"].ToString(), totalvenueamt = row["totalvenueamt"].ToString(), venue_date = row["venue_date"].ToString(), curr_date = row["curr_date"].ToString(), RENTAL = row["RENTAL"].ToString(), calcelamount = orinalefund, STATUS = row["STATUS"].ToString(), refudamt = row["calamts"].ToString(),tentivebooking= Convert.ToInt32( row["tentive_flg"].ToString() )});

                }




                bills.MUVENUEBOOKING = subs;
            }
            else
            {
                return RedirectToAction("Login");
            }
            return View(bills);
        }
        public IActionResult BILLResponse(OrderStatusViewModel ODERSTUST)
        {
            /// Request.Form["encResp"]
            //NameValueCollection nvc = Request.Form;
            try
            { 
            if (ODERSTUST.Message == "successfully")
            
                {
                    //string statusCode = objectres.payInstrument.responseDetails.statusCode;
                    //string bankTxnId = objectres.payInstrument.payModeSpecificData.bankDetails.bankTxnId;
                    //string atomTxnId = objectres.payInstrument.payDetails.atomTxnId;
                    //string txnCompleteDate = objectres.payInstrument.payDetails.txnCompleteDate;

                    string amount = ODERSTUST.amt;
                    string BILNO = ODERSTUST.ORDERID;
                    string customerid = ODERSTUST.custid;


                    //string txtatomTxnId = atomTxnId;
                    //string txtbankTxnId = bankTxnId;
                    //  string txtamount = amount;
                    //string txtdate = txnCompleteDate;
                    //string txtstatusCode = statusCode;
                    //  string txtmessage = message;

                    db.MultipleTransactions("UPDATE tbl_MemberBill_Main SET IsBilled=1,IsPayment=1,P_BillNO='" + BILNO + "',P_BillDate=GETDATE(),PAYMENTNO='" + ODERSTUST.paymentids + "' WHERE MemberID='" + customerid + "'");


                    DataTable dtv = Util.execQuerydt("select memberid,FirstName +' '+LastName name,email  from tbl_MemberBill_Main t, [TM_MemberDetail] tm where t.MemberID=tm.Memberidno and t.MemberID ='" + customerid + "'");


                    StringBuilder sBody = new StringBuilder();
                    sBody.Append("<!DOCTYPE html><html><body>");

                    sBody.Append("<p>Dear <b>" + dtv.Rows[0]["name"].ToString() + "</b></p>");
                    sBody.Append("<p>Greetings from Darjeelinggymkhanaclub!</p>");
                    sBody.Append("<p>Thanks for pay subscription Amount = " + amount + "</p>");
                    sBody.Append("<p>Your  bank payment transaction id = " + ODERSTUST.paymentids + "</p>");
                    ///sBody.Append("<p>For any query mail at info@darjeelinggymkhanaclub.com</p>");
                    sBody.Append("<p>Regards, </p>");
                    sBody.Append("<b>Darjeelinggymkhanaclub.com </b></p>");
                    sBody.Append("</body></html>");



                    //  mail = "ajay@bsdinfotech.com";

                  string OtpStatus = ClsUtil.SendMailViaIIS_html("info@darjeelinggymkhanaclub.in", dtv.Rows[0]["email"].ToString(), "", "", "subscription pay amount", sBody.ToString(), null, "k20@aoA60", "mx2.mailguru.biz", "");
                    //admin

                    StringBuilder sBody1 = new StringBuilder();
                    sBody1.Append("<!DOCTYPE html><html><body>");
                    sBody1.Append("<p>Dear Darjeelinggymkhanaclub, " + Environment.NewLine);
                    sBody1.Append("<p>Following has requested to pay the subscription Amount <b>successful</b>" + Environment.NewLine);

                    sBody1.Append("<table  border='1' style=\"width:100%\"><tr><td colspan=\"2\" style=\"text-align:center;background-color:#000000;color:#ffffff;\">Subscription Amount Details <b>Darjeelinggymkhanaclub.com</b></td></tr>");

                    sBody1.Append("<tr><td>Name</td><td>" + dtv.Rows[0]["name"].ToString() + '-' + dtv.Rows[0]["memberid"].ToString() + "</td></tr>");
                    sBody1.Append("<tr><td>Amount</td><td>" + amount + "</td></tr>");
                    sBody1.Append("<tr><td>PIno</td><td>" + BILNO + "</td></tr>");
                    sBody1.Append("<tr><td>Transaction id</td><td>" + ODERSTUST.paymentids + "</td></tr>");

                    sBody1.Append("</table>");

                    sBody1.Append("<p>Regards, </p>");
                    sBody1.Append("<b>Darjeelinggymkhanaclub.com </b></p>");
                    sBody1.Append("</body></html>");


                    string OtpStatus1 = ClsUtil.SendMailViaIIS_html("info@darjeelinggymkhanaclub.in", "info@darjeelinggymkhanaclub.in", "", "", "subscription pay amount", sBody1.ToString(), null, "k20@aoA60", "mx2.mailguru.biz", "");
                    ViewBag.Thanks = "Thank you for Subscription Payment.";

                }
                else
                {
                    ViewBag.Thanks = "Your Payemnt has been failed please try again.";
                }

            }
			catch (Exception Ex)
			{
				ViewBag.Thanks = "Your Payemnt has been failed please try again.";
				//return View("Error");
			}




			return View();
        }

        [HttpPost]
        public JsonResult paynow(string PATRYNO,string AMOUNT)
        {
            string message = "";

            try
            {

                ViewBag.Email = HttpContext.Request.Cookies["email"];
                ViewBag.memid = HttpContext.Request.Cookies["memid"];
                ViewBag.name = HttpContext.Request.Cookies["name"];

                DataSet ds = Util.Fill("exec Usp_MYAccountDetails '" + ViewBag.memid + "'", Util.Clubstr);

                string phone = ds.Tables[0].Rows[0]["CurrentPhone"].ToString();

                string patryno = DateTime.Now.ToString("ddMMyyyyHHmmss");

                hd.version = "OTSv1.1";
                hd.api = "AUTH";
                hd.platform = "FLASH";

                md.merchId = "438556";
                md.userId = "";
                md.password = "0fc7a6d8";
                md.merchTxnDate = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
                md.merchTxnId = patryno;



                pd.amount = AMOUNT.ToString();
                pd.product = "CLUB";
                pd.custAccNo = "";
                pd.txnCurrency = "INR";

                cd.custEmail = ViewBag.Email;
                cd.custMobile = phone;

                ex.udf1 = "";
                ex.udf2 = "";
                ex.udf3 = "";
                ex.udf4 = "";
                ex.udf5 = "";


                pr.headDetails = hd;
                pr.merchDetails = md;
                pr.payDetails = pd;
                pr.custDetails = cd;
                pr.extras = ex;

                rt.payInstrument = pr;

                var json = JsonConvert.SerializeObject(rt, Formatting.Indented);

                string passphrase = "82B3285AF51866742D8BA11D1ED38F89";
                string salt = "82B3285AF51866742D8BA11D1ED38F89";
                byte[] iv = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
                int iterations = 65536;
                int keysize = 256;

                string hashAlgorithm = "SHA1";
                string Encryptval = ClsUtil.Encrypt(json, passphrase, salt, iv, iterations);

                string testurleq = "https://payment1.atomtech.in/ots/aipay/auth?merchId=438556&encData=" + Encryptval;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(testurleq);
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                request.Proxy.Credentials = CredentialCache.DefaultCredentials;
                Encoding encoding = new UTF8Encoding();
                byte[] data = encoding.GetBytes(json);
                request.ProtocolVersion = HttpVersion.Version11;
                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = data.Length;
                //request.Timeout = 600000;
                Stream stream = request.GetRequestStream();
                stream.Write(data, 0, data.Length);
                //Console.WriteLine(stream);
                // Console.WriteLine(json);
                stream.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string jsonresponse = response.ToString();

                StreamReader reader = new StreamReader(response.GetResponseStream());
                ////  string jsonresponse = post;
                string temp = null;
                string status = "";
                while ((temp = reader.ReadLine()) != null)
                {
                    jsonresponse += temp;
                }
                //InitiateOrderResEq.RootObject objectres = new InitiateOrderResEq.RootObject();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                var result = jsonresponse.Replace("System.Net.HttpWebResponse", "");


                var uri = new Uri("http://atom.in?" + result);


                var query = HttpUtility.ParseQueryString(uri.Query);

                string encData = query.Get("encData");

                string passphrase1 = "92388804AECC4B3EC4D9C9AFCE2E8C36";
                string salt1 = "92388804AECC4B3EC4D9C9AFCE2E8C36";
                string Decryptval = ClsUtil.decrypt(encData, passphrase1, salt1, iv, iterations);

                Payverify.Payverify objectres = new Payverify.Payverify();
                objectres = new JavaScriptSerializer().Deserialize<Payverify.Payverify>(Decryptval);
                string txnMessage = objectres.responseDetails.txnMessage;
                ViewBag.tokenid = objectres.atomTokenId;


                var Data = new { message = "ok", datas = ViewBag.tokenid };
                return Json(Data);
            }
            catch (Exception Ex)
            {

                var Data = new { message = "not", datas = "" };
                return Json(Data);

            }



            return Json("");
        }


        [HttpPost]
        public async Task<IActionResult> cardrechagre(IFormCollection collection)
        {
           
                ClsUtil.WriteLogFile("logs", "input'" + "1" + "'", "", "", "", "", "", "", "execQuery", "");
                // block:start:session-function
                // PaymentHandlerConfig = PaymentHandlerConfig.Instance.WithInstance("config.json");
                string memid = HttpContext.Request.Cookies["memid"];
           string mailid = HttpContext.Request.Cookies["email"];
            string pno = HttpContext.Request.Cookies["pno"];
            string amount1 = collection["txtmsg"];
           

            string orderId = $"C_{DateTime.Now.ToString("yyyyMMddHHmmss")}";
                int amount = new Random().Next(0, 100);
                string customerId = memid.ToString();
                ClsUtil.WriteLogFile("logs", "input'" + "2" + "'", "", "", "", "", "", "", "execQuery", "");
                PaymentHandler paymentHandler = new PaymentHandler();
                ClsUtil.WriteLogFile("logs", "input'" + "3" + "'", "", "", "", "", "", "", "execQuery", "");
                var sessionInput = new Dictionary<string, object>
                    {
                            { "amount", amount1 },
                            { "order_id", orderId },
                            { "customer_id", customerId },
                              { "customer_phone", pno.ToString() },
                               { "customer_email", mailid.ToString() },
                            { "payment_page_client_id", paymentHandler.paymentHandlerConfig.PAYMENT_PAGE_CLIENT_ID },
                            { "action", "paymentPage" },
                             { "return_url", "https://memberlogin.darjeelinggymkhanaclub.in/Common/handlePaymentResponse" }
                          //  { "return_url", "https://memberlogin.darjeelinggymkhanaclub.in/Common/handlePaymentResponse" }
                    }; 

            ClsUtil.WriteLogFile("logs", "input'" + "4" + "'", "", "", "", "", "", "", "execQuery", "");
                var orderSession = await paymentHandler.OrderSession(sessionInput);
                ClsUtil.WriteLogFile("logs", "input'" + "5" + "'", "", "", "", "", "", "", "execQuery", "");
            // block:end:session-function
            try
            {
                if (orderSession?.payment_links?.web != null)
                    return Redirect((string)orderSession.payment_links.web);
                throw new Exception("Invalid Response unable to find web payment link");
            }
            catch (Exception ex)
            {
                ClsUtil.WriteLogFile("logs", "input'" + ex.Message + "'", "", "", "", "", "", "", "execQuery", "");
                return Redirect((string)orderSession.payment_links.web);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error1()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //[HttpPost]
        //public JsonResult cardrechagres(string amount)
        //{
        //    string message = "";

        //    try
        //    {

        //        ViewBag.Email = HttpContext.Request.Cookies["email"];
        //    ViewBag.memid = HttpContext.Request.Cookies["memid"];
        //    ViewBag.name = HttpContext.Request.Cookies["name"];

        //    DataSet ds = Util.Fill("exec Usp_MYAccountDetails '" + ViewBag.memid + "'", Util.Clubstr);

        //    string phone = ds.Tables[0].Rows[0]["CurrentPhone"].ToString();

        //    string patryno = DateTime.Now.ToString("ddMMyyyyHHmmss");

        //    hd.version = "OTSv1.1";
        //    hd.api = "AUTH";
        //    hd.platform = "FLASH";

        //    md.merchId = "438556";
        //    md.userId = "";
        //    md.password = "0fc7a6d8";
        //    md.merchTxnDate = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
        //    md.merchTxnId = patryno;



        //    pd.amount = amount.ToString();
        //    pd.product = "CLUB";
        //    pd.custAccNo = "";
        //    pd.txnCurrency = "INR";

        //    cd.custEmail = ViewBag.Email;
        //    cd.custMobile = phone;

        //    ex.udf1 = "";
        //    ex.udf2 = "";
        //    ex.udf3 = "";
        //    ex.udf4 = "";
        //    ex.udf5 = "";


        //    pr.headDetails = hd;
        //    pr.merchDetails = md;
        //    pr.payDetails = pd;
        //    pr.custDetails = cd;
        //    pr.extras = ex;

        //    rt.payInstrument = pr;

        //    var json = JsonConvert.SerializeObject(rt, Formatting.Indented);

        //    string passphrase = "82B3285AF51866742D8BA11D1ED38F89";
        //    string salt = "82B3285AF51866742D8BA11D1ED38F89";
        //    byte[] iv = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
        //    int iterations = 65536;
        //    int keysize = 256;

        //    string hashAlgorithm = "SHA1";
        //    string Encryptval = ClsUtil.Encrypt(json, passphrase, salt, iv, iterations);

        //    string testurleq = "https://payment1.atomtech.in/ots/aipay/auth?merchId=438556&encData=" + Encryptval;
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(testurleq);
        //    ServicePointManager.Expect100Continue = true;
        //    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
        //    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

        //    request.Proxy.Credentials = CredentialCache.DefaultCredentials;
        //    Encoding encoding = new UTF8Encoding();
        //    byte[] data = encoding.GetBytes(json);
        //    request.ProtocolVersion = HttpVersion.Version11;
        //    request.Method = "POST";
        //    request.ContentType = "application/json";
        //    request.ContentLength = data.Length;
        //    //request.Timeout = 600000;
        //    Stream stream = request.GetRequestStream();
        //    stream.Write(data, 0, data.Length);
        //    //Console.WriteLine(stream);
        //    // Console.WriteLine(json);
        //    stream.Close();
        //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //    string jsonresponse = response.ToString();

        //    StreamReader reader = new StreamReader(response.GetResponseStream());
        //    ////  string jsonresponse = post;
        //    string temp = null;
        //    string status = "";
        //    while ((temp = reader.ReadLine()) != null)
        //    {
        //        jsonresponse += temp;
        //    }
        //    //InitiateOrderResEq.RootObject objectres = new InitiateOrderResEq.RootObject();
        //    JavaScriptSerializer serializer = new JavaScriptSerializer();
        //    var result = jsonresponse.Replace("System.Net.HttpWebResponse", "");


        //    var uri = new Uri("http://atom.in?" + result);


        //    var query = HttpUtility.ParseQueryString(uri.Query);

        //    string encData = query.Get("encData");

        //    string passphrase1 = "92388804AECC4B3EC4D9C9AFCE2E8C36";
        //    string salt1 = "92388804AECC4B3EC4D9C9AFCE2E8C36";
        //    string Decryptval = ClsUtil.decrypt(encData, passphrase1, salt1, iv, iterations);

        //    Payverify.Payverify objectres = new Payverify.Payverify();
        //    objectres = new JavaScriptSerializer().Deserialize<Payverify.Payverify>(Decryptval);
        //    string txnMessage = objectres.responseDetails.txnMessage;
        //    ViewBag.tokenid = objectres.atomTokenId;


        //    var Data = new { message ="ok" ,datas= ViewBag.tokenid };
        //        return Json(Data);
        //    }
        //    catch (Exception Ex)
        //    {

        //        var Data = new { message = "not", datas = ""};
        //        return Json(Data);

        //    }



        //    return Json("");
        //}
        [HttpGet]
        public IActionResult cardrechagre()
        {
            ClsUtil.WriteLogFile("logs", "input'" + "1gdg" + "'", "", "", "", "", "", "", "execQuery", "");
            string Email = HttpContext.Request.Cookies["email"];
            string memid = HttpContext.Request.Cookies["memid"];
            string name = HttpContext.Request.Cookies["name"];

            if (memid != null)
            {
                DataSet ds = Util.Fill("exec Usp_MYAccountDetails '" + memid + "'", Util.Clubstr);

                string phone = ds.Tables[0].Rows[0]["CurrentPhone"].ToString();
                string address = ds.Tables[0].Rows[0]["CurrentAddress"].ToString();
                if (ds.Tables.Count>3)
                {
                    ViewBag.cardbal = ds.Tables[3].Rows[0]["amount"].ToString();
                }

                ViewBag.EMAIL = Email;
                ViewBag.MOBILE = phone;
            }
            else
            {
                return RedirectToAction("Login");
            }

            return View();
        }


        #region MyBill   
        [HttpPost]
        public async Task<IActionResult> MyBill(IFormCollection collection)
        {

           // ClsUtil.WriteLogFile("logs", "input'" + "1" + "'", "", "", "", "", "", "", "execQuery", "");
            // block:start:session-function
            // PaymentHandlerConfig = PaymentHandlerConfig.Instance.WithInstance("config.json");
            string memid = HttpContext.Request.Cookies["memid"];
            string Email = HttpContext.Request.Cookies["email"];
            DataSet ds = Util.Fill("exec Usp_MYAccountDetails '" + memid + "'", Util.Clubstr);

            string phone = ds.Tables[0].Rows[0]["CurrentPhone"].ToString();
            string address = ds.Tables[0].Rows[0]["CurrentAddress"].ToString();


            ViewBag.EMAIL = Email;
            ViewBag.MOBILE = phone;
            string amount1 =   Math.Round (Convert.ToDouble( collection["outs"]),0).ToString();

            string orderId = $"B_{DateTime.Now.ToString("yyyyMMddHHmmss")}";
            int amount = new Random().Next(0, 100);
            string customerId = memid.ToString();
          //  ClsUtil.WriteLogFile("logs", "input'" + "2" + "'", "", "", "", "", "", "", "execQuery", "");
            PaymentHandler paymentHandler = new PaymentHandler();
         //   ClsUtil.WriteLogFile("logs", "input'" + "3" + "'", "", "", "", "", "", "", "execQuery", "");
            var sessionInput1 = new Dictionary<string, object>
                    {
                            { "amount", amount1 },
                            { "order_id", orderId },
                            { "customer_id", customerId },
                            { "customer_phone",  ViewBag.MOBILE.ToString() },
                               { "customer_email", ViewBag.EMAIL.ToString() },
                            { "payment_page_client_id", paymentHandler.paymentHandlerConfig.PAYMENT_PAGE_CLIENT_ID },
                            { "action", "paymentPage" },
                           // { "return_url", "https://localhost:7134/Common/handlePaymentResponse" }
                           { "return_url", "https://memberlogin.darjeelinggymkhanaclub.in/Common/handlePaymentResponse" }
                    };

           // ClsUtil.WriteLogFile("logs", "input'" + "4" + "'", "", "", "", "", "", "", "execQuery", "");
            var orderSession1 = await paymentHandler.OrderSession(sessionInput1);
            //ClsUtil.WriteLogFile("logs", "input'" + "5" + "'", "", "", "", "", "", "", "execQuery", "");
            // block:end:session-function
            try
            {
                if (orderSession1?.payment_links?.web != null)
                    return Redirect((string)orderSession1.payment_links.web);
                throw new Exception("Invalid Response unable to find web payment link");
            }
            catch (Exception ex)
            {
                ClsUtil.WriteLogFile("logs", "input'" + ex.Message + "'", "", "", "", "", "", "", "execQuery", "");
                return Redirect((string)orderSession1.payment_links.web);
            }
        }
        public IActionResult MyBill( string years="",string monthss="",string module="")
        {

            Mybills bills = new Mybills();
            List<Subscriptin> subs = new List<Subscriptin>();
            List<food> food = new List<food>();
            List<bar> bars = new List<bar>();
            List<facility> facity = new List<facility>();
            List<cardrecharge> cardrech = new List<cardrecharge>();

            DateTime? date = DateTime.Now;



           string months= DateTime.Now.Month.ToString();

            string month = date.Value.ToString("MMMM");
            string year = DateTime.Now.Year.ToString();


            
            string Memid = HttpContext.Request.Cookies["memid"];
            if(years=="")
            {
                years = year;
            }
            if (monthss == "")
            {
                ViewBag.yeardata = month + ' ' + years;
            }
            else
            {
                ViewBag.yeardata = monthss + ' ' + years;
            }

            if (monthss != "")
            {
                if (monthss == "January")
                {
                    monthss = "1";
                }
                else if (monthss == "February")
                {
                    monthss = "2";
                }
                else if (monthss == "March")
                {
                    monthss = "3";
                }
                else if (monthss == "April")
                {
                    monthss = "4";
                }
                else if (monthss == "May")
                {
                    monthss = "5";
                }
                else if (monthss == "June")
                {
                    monthss = "6";
                }
                else if (monthss == "July")
                {
                    monthss = "7";
                }
                else if (monthss == "August")
                {
                    monthss = "8";
                }
                else if (monthss == "September")
                {
                    monthss = "9";
                }
                else if (monthss == "October")
                {
                    monthss = "10";
                }
                else if (monthss == "November")
                {
                    monthss = "11";
                }
                else if (monthss == "December")
                {
                    monthss = "12";
                }

            }

            if (monthss == "")
            {
                monthss = months;
            }
           
            ViewBag.yearss = years;

            //HttpContext.Session.GetString("memid");
            DataSet ds = Util.Fill("exec Usp_MyBillsDetails_new_app '" + Memid + "',"+ years + ","+ monthss + "", Util.Clubstr);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                facity.Add(new facility { billno = row["BillNo"].ToString(), billdate = row["Billdate"].ToString(), amount = row["AMOUNT"].ToString() });
            }
            foreach (DataRow row in ds.Tables[1].Rows)
            {
                bars.Add(new bar { billno = row["BillNo"].ToString(), billdate = row["Billdate"].ToString(), amount = row["AMOUNT"].ToString() });
            }
            foreach (DataRow row in ds.Tables[2].Rows)
            {
                food.Add(new food { billno = row["BillNo"].ToString(), billdate = row["Billdate"].ToString(), amount = row["AMOUNT"].ToString() });
            }
            foreach (DataRow row in ds.Tables[3].Rows)
            {
                cardrech.Add(new cardrecharge { billno = row["BillNo"].ToString(), billdate = row["Billdate"].ToString(), amount = row["AMOUNT"].ToString() });
            }
            foreach (DataRow row in ds.Tables[4].Rows)
            {
                subs.Add(new Subscriptin { billno = row["BillNo"].ToString(), billdate = row["Billdate"].ToString(), amount = row["AMOUNT"].ToString(), status = row["STATUSDATA"].ToString() });
                ViewBag.stateus = row["STATUSDATA"].ToString();
            }

            if (ds.Tables[5].Rows.Count > 0)
            {
                ViewBag.totaloutstanding = ds.Tables[5].Rows[0][0].ToString();
                ViewBag.EMAIL = ds.Tables[5].Rows[0][1].ToString();
                ViewBag.MOBILE = ds.Tables[5].Rows[0][2].ToString();
                ViewBag.BILLNOO = ds.Tables[5].Rows[0][3].ToString();
                ViewBag.OPENBAL = ds.Tables[5].Rows[0][4].ToString();
                ViewBag.Memorandum = ds.Tables[5].Rows[0][5].ToString();
            }

            string dd = DateTime.Now.ToString("ddMMyyyyHHmmss");

            //if (ds.Tables[5].Rows.Count > 0)
            //{

            //    hd.version = "OTSv1.1";
            //    hd.api = "AUTH";
            //    hd.platform = "FLASH";

            //    md.merchId = "438556";
            //    md.userId = "";
            //    md.password = "0fc7a6d8";
            //    md.merchTxnDate = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
            //    md.merchTxnId = ViewBag.BILLNOO;



            //    pd.amount = ViewBag.totaloutstanding;
            //    pd.product = "CLUB";
            //    pd.custAccNo = "";
            //    pd.txnCurrency = "INR";

            //    cd.custEmail = ViewBag.EMAIL;
            //    cd.custMobile = ViewBag.MOBILE;

            //    ex.udf1 = "";
            //    ex.udf2 = "";
            //    ex.udf3 = "";
            //    ex.udf4 = "";
            //    ex.udf5 = "";


            //    pr.headDetails = hd;
            //    pr.merchDetails = md;
            //    pr.payDetails = pd;
            //    pr.custDetails = cd;
            //    pr.extras = ex;

            //    rt.payInstrument = pr;

            //    var json = JsonConvert.SerializeObject(rt, Formatting.Indented);

            //    string passphrase = "82B3285AF51866742D8BA11D1ED38F89";
            //    string salt = "82B3285AF51866742D8BA11D1ED38F89";
            //    byte[] iv = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            //    int iterations = 65536;
            //    int keysize = 256;

            //    string hashAlgorithm = "SHA1";
            //    string Encryptval = ClsUtil.Encrypt(json, passphrase, salt, iv, iterations);

            //    string testurleq = "https://payment1.atomtech.in/ots/aipay/auth?merchId=438556&encData=" + Encryptval;
            //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(testurleq);
            //    ServicePointManager.Expect100Continue = true;
            //    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            //    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            //    request.Proxy.Credentials = CredentialCache.DefaultCredentials;
            //    Encoding encoding = new UTF8Encoding();
            //    byte[] data = encoding.GetBytes(json);
            //    request.ProtocolVersion = HttpVersion.Version11;
            //    request.Method = "POST";
            //    request.ContentType = "application/json";
            //    request.ContentLength = data.Length;
            //    //request.Timeout = 600000;
            //    Stream stream = request.GetRequestStream();
            //    stream.Write(data, 0, data.Length);
            //    //Console.WriteLine(stream);
            //    // Console.WriteLine(json);
            //    stream.Close();
            //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //    string jsonresponse = response.ToString();

            //    StreamReader reader = new StreamReader(response.GetResponseStream());
            //    ////  string jsonresponse = post;
            //    string temp = null;
            //    string status = "";
            //    while ((temp = reader.ReadLine()) != null)
            //    {
            //        jsonresponse += temp;
            //    }
            //    //InitiateOrderResEq.RootObject objectres = new InitiateOrderResEq.RootObject();
            //    JavaScriptSerializer serializer = new JavaScriptSerializer();
            //    var result = jsonresponse.Replace("System.Net.HttpWebResponse", "");
            //    //// var result = "{\"initiateDigiOrderResponse\":{ \"msgHdr\":{ \"rslt\":\"OK\"},\"msgBdy\":{ \"sts\":\"ACPT\",\"txnId\":\"DIG2019039816365405440004\"}}}";
            //    //  var  objectres = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Payverify.Payverify>(result);

            //    var uri = new Uri("http://atom.in?" + result);


            //    var query = HttpUtility.ParseQueryString(uri.Query);

            //    string encData = query.Get("encData");

            //    string passphrase1 = "92388804AECC4B3EC4D9C9AFCE2E8C36";
            //    string salt1 = "92388804AECC4B3EC4D9C9AFCE2E8C36";
            //    string Decryptval = ClsUtil.decrypt(encData, passphrase1, salt1, iv, iterations);

            //    Payverify.Payverify objectres = new Payverify.Payverify();
            //    objectres = new JavaScriptSerializer().Deserialize<Payverify.Payverify>(Decryptval);
            //    string txnMessage = objectres.responseDetails.txnMessage;
            //    ViewBag.tokenid = objectres.atomTokenId;
            //}

            bills.facilities = facity;
            bills.bar = bars;
            bills.food = food;
            bills.cardrecharge = cardrech;
            bills.subscriptins = subs;
            return View(bills);
        }


        #endregion

       

        #region  Venue Bookings   Venue-Bookings-calendar

       
        #endregion

        #region Venue-Bookings-calendar

        public IActionResult VenueBookingscalendar()
        {
            return View();
        }

        


        #endregion

        public void SMSAPI(string msg, string mobno)
        {

            // HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("https://japi.instaalerts.zone/httpapi/QueryStringReceiver?ver=1.0&key=pFwC2xIKeVBApF5XIp2Ydg==&encrpt=0&dest="+ mobno + "&send=HDLCLB&text=OTP for Login at darjeelinggymkhana club App / Website is " + msg + ". Please enter to verify. OTP will be valid for 10 minutes.");

            //  HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("https://api.smartping.ai/fe/api/v1/send?username=holidayclub.trans&password=Hclub@24&unicode=false&from=HDLCLB&to="+ mobno + "&text=OTP for Login at darjeelinggymkhana club App / Website is " + msg + ". Please enter to verify. OTP will be valid for 10 minutes.");

            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("https://hisocial.in/api/send?number=91"+ mobno + "&type=text&message=OTP for Login at darjeeling gymkhana club App / Website is " + msg + ". Please enter to verify. OTP will be valid for 10 minutes.&instance_id=682ECA553995E&access_token=682eaf57552d7");



            HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
            System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
            string responseString = respStreamReader.ReadToEnd();
            respStreamReader.Close();
            myResp.Close();
        }
        public IActionResult commonMethod(string JsonData)
        {
            String div = "abc";
            //string sessionid = HttpContext.Session.Id;
            //HttpContext.Session.SetString("sessionid", sessionid.ToString());
            DataSet ds = Util.CommonFill("exec Usp_Common '"+ JsonData + "'", Util.Clubstr, 0);
            String msg = ds.Tables[0].Rows[0]["Message"].ToString();

            String status = ds.Tables[0].Rows[0]["Status"].ToString();

            DataTable datads = JsonConvert.DeserializeObject<DataTable>(ds.Tables[0].Rows[0]["Data"].ToString());
            DataTable mailds = JsonConvert.DeserializeObject<DataTable>(ds.Tables[0].Rows[0]["mail"].ToString());
            if (datads.Rows.Count >0) {

               

                HttpContext.Session.SetString("email", datads.Rows[0]["Email"].ToString());
                HttpContext.Session.SetString("memid", datads.Rows[0]["Memid"].ToString());
                HttpContext.Session.SetString("name", datads.Rows[0]["FirstName"].ToString());
                HttpContext.Session.SetString("otpnumber", datads.Rows[0]["otpnumber"].ToString());
                HttpContext.Session.SetString("pno", mailds.Rows[0]["attachPath"].ToString());

            }
          
         //   DataTable mailds = JsonConvert.DeserializeObject<DataTable>(ds.Tables[0].Rows[0]["mail"].ToString());

            if (mailds.Rows.Count > 0)
            {
                for (int i = 0; i < mailds.Rows.Count; i++)
                {
                    StringBuilder sBody1 = new StringBuilder();
                    sBody1.Append("<!DOCTYPE html><html><body>");
                    sBody1.Append("<p>Your verification code "+ mailds.Rows[0]["_body"].ToString() + "" + Environment.NewLine);
                    sBody1.Append("<p>The verification code will be valid for 10 minutes. Please do not share this code with anyone. " + Environment.NewLine);
                    sBody1.Append("<p>Regards, </p>");
                    sBody1.Append("<b>Darjeeling gymkhana club </b></p>");
                    sBody1.Append("</body></html>");

                    //SMSAPI(mailds.Rows[0]["_body"].ToString(), "918587068357");

                    SMSAPI(mailds.Rows[0]["_body"].ToString(), mailds.Rows[0]["attachPath"].ToString());


                string OtpStatus = ClsUtil.SendMailViaIIS_html("info@darjeelinggymkhanaclub.in", mailds.Rows[0]["to"].ToString(), mailds.Rows[0]["cc"].ToString(), mailds.Rows[0]["bcc"].ToString(), "Login Verification", sBody1.ToString(), null, "k20@aoA60", "smtp.gmail.com", "");



                }
            }
            var Data = new { status = status, message = msg,data=JsonConvert.SerializeObject(datads) };
            return Json(Data);
        }
        public IActionResult commonMethods(string JsonData)
        {
            String div = "abc";
            //string sessionid = HttpContext.Session.Id;
            //HttpContext.Session.SetString("sessionid", sessionid.ToString());
            DataSet ds = Util.CommonFill("exec Usp_Common_test '" + JsonData + "'", Util.Clubstr, 0);
            String msg = ds.Tables[0].Rows[0]["Message"].ToString();

            String status = ds.Tables[0].Rows[0]["Status"].ToString();

            DataTable datads = JsonConvert.DeserializeObject<DataTable>(ds.Tables[0].Rows[0]["Data"].ToString());
            DataTable mailds = JsonConvert.DeserializeObject<DataTable>(ds.Tables[0].Rows[0]["mail"].ToString());
            if (datads.Rows.Count > 0)
            {



                HttpContext.Session.SetString("email", datads.Rows[0]["Email"].ToString());
                HttpContext.Session.SetString("memid", datads.Rows[0]["Memid"].ToString());
                HttpContext.Session.SetString("name", datads.Rows[0]["FirstName"].ToString());
                HttpContext.Session.SetString("otpnumber", datads.Rows[0]["otpnumber"].ToString());
                HttpContext.Session.SetString("pno", mailds.Rows[0]["attachPath"].ToString());

                //TempData["pno"] = mailds.Rows[0]["attachPath"].ToString();
            }

         

            if (mailds.Rows.Count > 0)
            {
                for (int i = 0; i < mailds.Rows.Count; i++)
                {
                    StringBuilder sBody1 = new StringBuilder();
                    sBody1.Append("<!DOCTYPE html><html><body>");
                    sBody1.Append("<p>Your verification code " + mailds.Rows[0]["_body"].ToString() + "" + Environment.NewLine);
                    sBody1.Append("<p>The verification code will be valid for 10 minutes. Please do not share this code with anyone. " + Environment.NewLine);
                    sBody1.Append("<p>Regards, </p>");
                    sBody1.Append("<b>Darjeelinggymkhanaclub.com </b></p>");
                    sBody1.Append("</body></html>");

                   // HttpContext.Session.SetString("phno", mailds.Rows[0]["attachPath"].ToString());
                    //SMSAPI(mailds.Rows[0]["_body"].ToString(), "918587068357");

                    SMSAPI(mailds.Rows[0]["_body"].ToString(), mailds.Rows[0]["attachPath"].ToString());


                    string OtpStatus = ClsUtil.SendMailViaIIS_html("info@darjeelinggymkhanaclub.in", mailds.Rows[0]["to"].ToString(), mailds.Rows[0]["cc"].ToString(), mailds.Rows[0]["bcc"].ToString(), "Login Verification", sBody1.ToString(), null, "k20@aoA60", "smtp.gmail.com", "");



                }
            }
            var Data = new { status = status, message = msg, data = JsonConvert.SerializeObject(datads) };
            return Json(Data);
        }
    }
}
