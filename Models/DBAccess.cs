using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using ClubApp.Models;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Syncfusion.EJ2.Base;
using Syncfusion.EJ2.Charts;
using ClubApp;
using Microsoft.Extensions.Logging;

namespace Indiastat.Web.Model
{
    public class DBAccess
    {
        ClsUtility ClsUtil = new ClsUtility();
        SqlCommand cmd;
        //string cs = ConfigurationManager.ConnectionStrings["OnlineTrainingDB"].ConnectionString;

        public static string cs = "Data Source=103.21.58.192;Initial Catalog=dgcgym;Persist Security Info=True;User ID=dgcgym1;Password=~1FXvhq~f0T1albd";

        public int InsertForm(OnlineTraining obj)
        {
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("USPOnlineTraining", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@CompanyName", obj.CompanyName);
                com.Parameters.AddWithValue("@SubscriberfromIndia", obj.SubscriberfromIndia);
                com.Parameters.AddWithValue("@ContactPersonName", obj.ContactPersonName);
                com.Parameters.AddWithValue("@Designation", obj.Designation);
                com.Parameters.AddWithValue("@Email", obj.Email);
                com.Parameters.AddWithValue("@ContactNumber", obj.ContactNumber);
                com.Parameters.AddWithValue("@ExpectedAtendees", obj.ExpectedAtendees);
                com.Parameters.AddWithValue("@AtendeeType", obj.AtendeeType);
                com.Parameters.AddWithValue("@Date", obj.Date);
                com.Parameters.AddWithValue("@Timing", obj.Timing);
                com.Parameters.AddWithValue("@Action", "Insert");
                i = com.ExecuteNonQuery();
            }
            return i;
        }
        public List<OnlineTraining> ListAllRegistration()
        {
            List<OnlineTraining> lst = new List<OnlineTraining>();
            OnlineTraining obj;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("USPOnlineTraining", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Action", "AllRegistrationList");

                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    obj = new OnlineTraining();
                    if (rdr["Id"] != DBNull.Value)
                    {
                        obj.Id = Convert.ToInt32(rdr["Id"]);
                    }
                    if (rdr["CompanyName"] != DBNull.Value)
                    {
                        obj.CompanyName = rdr["CompanyName"].ToString();
                    }
                    if (rdr["SubscriberfromIndia"] != DBNull.Value)
                    {
                        obj.SubscriberfromIndia = rdr["SubscriberfromIndia"].ToString();
                    }
                    if (rdr["ContactPersonName"] != DBNull.Value)
                    {
                        obj.ContactPersonName = rdr["ContactPersonName"].ToString();
                    }
                    if (rdr["Designation"] != DBNull.Value)
                    {
                        obj.Designation = rdr["Designation"].ToString();
                    }
                    if (rdr["Email"] != DBNull.Value)
                    {
                        obj.Email = rdr["Email"].ToString();
                    }
                    if (rdr["ContactNumber"] != DBNull.Value)
                    {
                        obj.ContactNumber = rdr["ContactNumber"].ToString();
                    }
                    if (rdr["ExpectedAtendees"] != DBNull.Value)
                    {
                        obj.ExpectedAtendees = rdr["ExpectedAtendees"].ToString();
                    }
                    if (rdr["AtendeeType"] != DBNull.Value)
                    {
                        obj.AtendeeType = rdr["AtendeeType"].ToString();
                    }
                    if (rdr["Date"] != DBNull.Value)
                    {
                        obj.Date = rdr["Date"].ToString();
                    }
                    if (rdr["Timing"] != DBNull.Value)
                    {
                        obj.Timing = rdr["Timing"].ToString();
                    }
                    if (rdr["Status"] != DBNull.Value)
                    {
                        obj.Status = rdr["Status"].ToString();
                    }
                    else
                    {
                        obj.Status = "Pending";
                    }
                    lst.Add(obj);
                }
            }
            return lst;
        }
        public OnlineTraining GetRegistrationById(int? Id)
        {
            OnlineTraining obj = new OnlineTraining();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("USPOnlineTraining", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", Id);
                com.Parameters.AddWithValue("@Action", "GetRegistrationById");

                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    if (rdr["Id"] != DBNull.Value)
                    {
                        obj.Id = Convert.ToInt32(rdr["Id"]);
                    }
                    if (rdr["CompanyName"] != DBNull.Value)
                    {
                        obj.CompanyName = rdr["CompanyName"].ToString();
                    }
                    if (rdr["SubscriberfromIndia"] != DBNull.Value)
                    {
                        obj.SubscriberfromIndia = rdr["SubscriberfromIndia"].ToString();
                    }
                    if (rdr["ContactPersonName"] != DBNull.Value)
                    {
                        obj.ContactPersonName = rdr["ContactPersonName"].ToString();
                    }
                    if (rdr["Designation"] != DBNull.Value)
                    {
                        obj.Designation = rdr["Designation"].ToString();
                    }
                    if (rdr["Email"] != DBNull.Value)
                    {
                        obj.Email = rdr["Email"].ToString();
                    }
                    if (rdr["ContactNumber"] != DBNull.Value)
                    {
                        obj.ContactNumber = rdr["ContactNumber"].ToString();
                    }
                    if (rdr["ExpectedAtendees"] != DBNull.Value)
                    {
                        obj.ExpectedAtendees = rdr["ExpectedAtendees"].ToString();
                    }
                    if (rdr["AtendeeType"] != DBNull.Value)
                    {
                        obj.AtendeeType = rdr["AtendeeType"].ToString();
                    }
                    if (rdr["Date"] != DBNull.Value)
                    {
                        obj.Date = rdr["Date"].ToString();
                        DateTime dateval = new DateTime();
                        dateval = Convert.ToDateTime(obj.Date);
                        string year = dateval.ToString("yyy");
                        string month_name = dateval.ToString("MMMM");
                        string daynamenum = dateval.ToString("dd");
                        string dayname = dateval.ToString("dddd");
                        obj.fullDate = daynamenum + " " + month_name + " " + year;
                    }
                    if (rdr["Timing"] != DBNull.Value)
                    {
                        obj.Timing = rdr["Timing"].ToString();
                        string[] multiArray = obj.Timing.Split(new Char[] { '-' });
                        obj.ToTime = multiArray[0];
                    }
                    if (rdr["Status"] != DBNull.Value)
                    {
                        obj.Status = rdr["Status"].ToString();
                    }
                }
            }
            return obj;
        }
        public List<SelectListItem> ListAllTimeSlot()
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            SelectListItem obj;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("select* from TblTimeSlot WHERE CONVERT(VARCHAR(10),GETDATE(),103)+' '+ CONVERT(VARCHAR(5), timeFROM, 108)  > CONVERT(VARCHAR(10),GETDATE(),103)+' '+Format(GetDate(), 'HH:mm')  and timing not in ( select Timing from TblOnlineTraining where  status<>'Rejected'  and convert(VARCHAR(10), CONVERT(datetime, Date, 101),103)=convert(VARCHAR(10),CONVERT(date, GETDATE(), 101),103)); ", con);
                com.CommandType = CommandType.Text;
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    obj = new SelectListItem();
                    if (rdr["id"] != DBNull.Value)
                    {
                        obj.Value = rdr["id"].ToString();
                    }
                    if (rdr["Timing"] != DBNull.Value)
                    {
                        obj.Text = rdr["Timing"].ToString();
                    }
                    lst.Add(obj);
                }
            }
            return lst;
        }

        public List<SelectListItem> getdetils(string billno, string id)
        {
            List<SelectListItem> Datelst = new List<SelectListItem>();
            SelectListItem obj;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("Usp_details", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@billno", billno);
                com.Parameters.AddWithValue("@id ", id);
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    obj = new SelectListItem();
                   
                        obj.Text = rdr["datas"].ToString();
                   
                    Datelst.Add(obj);
                }
            }
            return Datelst;
        }
        public List<OnlineTraining> GetAllDate()
        {
            List<OnlineTraining> Datelst = new List<OnlineTraining>();
            OnlineTraining obj;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("USPOnlineTraining", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Action", "GetAllDate");
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    obj = new OnlineTraining();
                    if (rdr["Date"] != DBNull.Value)
                    {
                        obj.Date = rdr["Date"].ToString();
                    }
                    Datelst.Add(obj);
                }
            }
            return Datelst;
        }
        public List<OnlineTraining> GetSlotDetails(int year, int Month)
        {
            List<OnlineTraining> lst = new List<OnlineTraining>();
            OnlineTraining obj;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("USPOnlineTraining", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@year", year);
                com.Parameters.AddWithValue("@Month", Month);
                com.Parameters.AddWithValue("@Action", "GetSlotDetails");

                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    obj = new OnlineTraining();
                    if (rdr["Id"] != DBNull.Value)
                    {
                        obj.Id = Convert.ToInt32(rdr["Id"]);
                    }
                    if (rdr["CompanyName"] != DBNull.Value)
                    {
                        obj.CompanyName = rdr["CompanyName"].ToString();
                    }
                    if (rdr["ContactPersonName"] != DBNull.Value)
                    {
                        obj.ContactPersonName = rdr["ContactPersonName"].ToString();
                    }
                    if (rdr["ContactNumber"] != DBNull.Value)
                    {
                        obj.ContactNumber = rdr["ContactNumber"].ToString();
                    }
                    if (rdr["Email"] != DBNull.Value)
                    {
                        obj.Email = rdr["Email"].ToString();
                    }
                    if (rdr["Date"] != DBNull.Value)
                    {
                        obj.Date = rdr["Date"].ToString();
                    }
                    if (rdr["Timing"] != DBNull.Value)
                    {
                        obj.Timing = rdr["Timing"].ToString();
                    }
                    lst.Add(obj);
                }
            }
            return lst;
        }


        public string MultipleTransactions(string query)
        {
            SqlTransaction SqlTran = null;
            using (SqlConnection sqcon = new SqlConnection(cs))
            {
                try
                {
                    if (sqcon.State == ConnectionState.Closed) sqcon.Open();

                    SqlTran = sqcon.BeginTransaction();
                    cmd = new SqlCommand(query, sqcon, SqlTran);
                    cmd.ExecuteNonQuery();
                    SqlTran.Commit();
                    query = "Successfull";
                }
                catch (Exception exce)
                {
                    SqlTran.Rollback();
                    query = "Transaction Rolledback. Due to " + exce.Message;
                }
                finally
                {
                    sqcon.Close();
                }
            }
            return query;
        }

        public List<SelectListItem> ListAllTimeSlot( string DATE, string venudid)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            SelectListItem obj;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();


               // SqlCommand com = new SqlCommand("  If Exists (select * from [dbo].[tran_venue] t,TYPE_VENUE tv where tv.id=t.venuetypeid  and  [venueid]="+ venudid + " and pay_status=1   and  convert(date, cast( VENUE_DATE as datetime),103)= convert(date,'" + DATE+ "',103) and name='HI TEA') BEGIN   select * from TYPE_VENUE tv where venuid=" + venudid + " and  venuid not in ( select [venueid] from [dbo].[tran_venue] where[venueid]="+ venudid + "  and pay_status=1  and  convert(date, cast( VENUE_DATE as datetime),103)= convert(date,'" + DATE+ "',103))  and  id not in (select venuetypeid from [dbo].[TblDateBlock] t where [venueid]=" + venudid + " and  tv.id=t.venuetypeid and  convert(date,BlockDate,103)= convert(date,'" + DATE+ "',103)) end  else  begin If Exists(select * from[dbo].[tran_venue] t, TYPE_VENUE tv where tv.id = t.venuetypeid  and[venueid] = " + venudid + " and pay_status = 1   and  convert(date, cast(VENUE_DATE as datetime), 103) = convert(date, '" + DATE+ "', 103)) BEGIN select * from TYPE_VENUE tv where venuid = " + venudid + " and NAME <> 'HI TEA' and    id not in (select[venuetypeid] from[dbo].[tran_venue] where[venueid] = "+ venudid + "  and pay_status = 1  and  convert(date, cast(VENUE_DATE as datetime), 103) = convert(date, '" + DATE+ "', 103))  and id not in (select venuetypeid from[dbo].[TblDateBlock] t where[venueid] = " + venudid + " and tv.id = t.venuetypeid and convert(date, BlockDate,103)= convert(date, '" + DATE+ "', 103)) end else begin select* from TYPE_VENUE tv where venuid = " + venudid + "  and id not in (select[venuetypeid] from[dbo].[tran_venue] where[venueid] = "+ venudid + "  and pay_status = 1  and convert(date, cast(VENUE_DATE as datetime), 103) = convert(date, '" + DATE+ "', 103))  and id not in (select venuetypeid from[dbo].[TblDateBlock] t where[venueid] = " + venudid + " and tv.id = t.venuetypeid and convert(date, BlockDate,103)= convert(date, '" + DATE+ "', 103)) end end", con);

                //   SqlCommand com = new SqlCommand(" If Exists (select * from [dbo].[tran_venue] t,TYPE_VENUE tv where tv.id=t.venuetypeid  and  [venueid]="+ venudid + " and pay_status=1   and  convert(date, cast( VENUE_DATE as datetime),103)= convert(date,'" + DATE+ "',103) and name='HI TEA') BEGIN   select * from TYPE_VENUE tv where venuid="+ venudid + " and  venuid not in ( select [venueid] from [dbo].[tran_venue] where[venueid]="+ venudid + "  and pay_status=1  and  convert(date, cast( VENUE_DATE as datetime),103)= convert(date,'" + DATE+ "',103))  and  id not in (select venuetypeid from [dbo].[TblDateBlock] t where [venueid]="+ venudid + " and  tv.id=t.venuetypeid and  convert(date,BlockDate,103)= convert(date,'" + DATE+ "',103)) end else begin select * from TYPE_VENUE tv where venuid = "+ venudid + " and NAME <> 'HI TEA' and    id not in (select[venuetypeid] from[dbo].[tran_venue] where[venueid] = "+ venudid + "  and pay_status = 1  and  convert(date, cast(VENUE_DATE as datetime), 103) = convert(date, '" + DATE+ "', 103))  and id not in (select venuetypeid from[dbo].[TblDateBlock] t where [venueid] = "+ venudid + " and tv.id = t.venuetypeid and convert(date, BlockDate,103)= convert(date, '" + DATE+ "', 103)) end", con);


                SqlCommand com = new SqlCommand("select * from TYPE_VENUE tv where   id not in ( select [venuetypeid] from [dbo].[tran_venue] where[venueid]="+ venudid + "  and pay_status=1  and  convert(date, cast( VENUE_DATE as datetime),103)= convert(date,'" + DATE+ "',103))  and  id not in (select venuetypeid from [dbo].[TblDateBlock] t where [venueid]="+ venudid + " and  tv.id=t.venuetypeid and  convert(date,BlockDate,103)= convert(date,'" + DATE+ "',103))", con);

               // SqlCommand com = new SqlCommand("select * from TYPE_VENUE tv where venuid=" + venudid + " and  id not in ( select [venuetypeid] from [dbo].[tran_venue] where[venueid]=" + venudid + "  and pay_status=1  and  convert(date, cast( VENUE_DATE as datetime),103)= convert(date,'" + DATE + "',103))  and  id not in (select venuetypeid from [dbo].[TblDateBlock] t where [venueid]=" + venudid + " and  tv.id=t.venuetypeid and  convert(date,BlockDate,103)= convert(date,'" + DATE + "',103))", con);



                com.CommandType = CommandType.Text;
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    obj = new SelectListItem();
                    if (rdr["id"] != DBNull.Value)
                    {
                        obj.Value = rdr["id"].ToString();
                    }
                    if (rdr["name"] != DBNull.Value)
                    {
                        obj.Text = rdr["name"].ToString();
                    }
                    lst.Add(obj);
                }
            }
            return lst;
        }
        public List<SelectListItem> ListAllTimeSlotnot(string DATE, string venudid)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            SelectListItem obj;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();

                SqlCommand com = new SqlCommand("select '<span style=color:red>Book- '+ name+' </span>' name from TYPE_VENUE tv where   id  in ( select [venuetypeid] from [dbo].[tran_venue] where[venueid]=" + venudid + "  and pay_status=1  and  convert(date, cast( VENUE_DATE as datetime) ,103)= convert(date,'" + DATE + "',103)) and  id not in (select venuetypeid from [dbo].[TblDateBlock] t where [venueid]=" + venudid + " and  tv.id=t.venuetypeid and  convert(date,BlockDate,103)= convert(date,'" + DATE + "',103)) union all select  '<span style=color:green>Available- ' + name + '</span>' name from TYPE_VENUE tv where  id not  in (select[venuetypeid] from[dbo].[tran_venue] where[venueid] = " + venudid + "  and pay_status = 1  and  convert(date, cast( VENUE_DATE as datetime),103) = convert(date,'" + DATE + "',103)) and  id not in (select venuetypeid from [dbo].[TblDateBlock] t where [venueid]=" + venudid + " and  tv.id=t.venuetypeid and  convert(date,BlockDate,103)= convert(date,'" + DATE + "',103))", con);

                //SqlCommand com = new SqlCommand("select '<span style=color:red>Book- '+ name+' </span>' name from TYPE_VENUE tv where venuid="+ venudid + " and  id  in ( select [venuetypeid] from [dbo].[tran_venue] where[venueid]=" + venudid + "  and pay_status=1  and  convert(date, cast( VENUE_DATE as datetime) ,103)= convert(date,'" + DATE+ "',103)) and  id not in (select venuetypeid from [dbo].[TblDateBlock] t where [venueid]=" + venudid + " and  tv.id=t.venuetypeid and  convert(date,BlockDate,103)= convert(date,'" + DATE+ "',103)) union all select  '<span style=color:green>Available- ' + name + '</span>' name from TYPE_VENUE tv where venuid = "+ venudid + " and  id not  in (select[venuetypeid] from[dbo].[tran_venue] where[venueid] = " + venudid + "  and pay_status = 1  and  convert(date, cast( VENUE_DATE as datetime),103) = convert(date,'" + DATE+ "',103)) and  id not in (select venuetypeid from [dbo].[TblDateBlock] t where [venueid]=" + venudid + " and  tv.id=t.venuetypeid and  convert(date,BlockDate,103)= convert(date,'" + DATE+ "',103))", con);

                // SqlCommand com = new SqlCommand(" If Exists (select * from [dbo].[tran_venue] t,TYPE_VENUE tv where tv.id=t.venuetypeid  and  [venueid]=" + venudid + " and pay_status=1   and  convert(date, cast( VENUE_DATE as datetime),103)= convert(date,'" + DATE+ "',103) and name='HI TEA') begin select '<span style=color:red>Book- '+ name+' </span>' name from TYPE_VENUE tv where venuid=" + venudid + " and  id  in ( select [venuetypeid] from [dbo].[tran_venue] where[venueid]=" + venudid + "  and pay_status=1  and  convert(date, cast( VENUE_DATE as datetime) ,103)= convert(date,'" + DATE+ "',103)) and  id not in (select venuetypeid from [dbo].[TblDateBlock] t where [venueid]=" + venudid + " and  tv.id=t.venuetypeid and  convert(date,BlockDate,103)= convert(date,'" + DATE+ "',103)) union all select  '<span style=color:red>Not Available- ' + name + '</span>' name from TYPE_VENUE tv where venuid = " + venudid + " and  id not  in (select[venuetypeid] from[dbo].[tran_venue] where[venueid] = " + venudid + "  and pay_status = 1  and  convert(date, cast(VENUE_DATE as datetime), 103) = convert(date, '" + DATE+ "', 103)) and id not in (select venuetypeid from[dbo].[TblDateBlock] t where [venueid] = " + venudid + " and tv.id = t.venuetypeid and convert(date, BlockDate,103)= convert(date, '" + DATE+ "', 103))end else begin If Exists(select * from[dbo].[tran_venue] t, TYPE_VENUE tv where tv.id = t.venuetypeid  and[venueid] = " + venudid + " and pay_status = 1   and  convert(date, cast(VENUE_DATE as datetime), 103) = convert(date, '" + DATE+ "', 103)) BEGIN select '<span style=color:red>Book- ' + name + ' </span>' name from TYPE_VENUE tv where venuid = " + venudid + " and id  in (select[venuetypeid] from[dbo].[tran_venue] where[venueid] = " + venudid + "  and pay_status = 1  and convert(date, cast(VENUE_DATE as datetime), 103) = convert(date, '" + DATE+ "', 103)) and id not in (select venuetypeid from[dbo].[TblDateBlock] t where[venueid] = " + venudid + " and tv.id = t.venuetypeid and convert(date, BlockDate,103)= convert(date, '" + DATE+ "', 103)) union all select  '<span style=color:green>Available- ' + name + '</span>' name from TYPE_VENUE tv where venuid = " + venudid + " and NAME<>'HI TEA' and id not  in (select[venuetypeid] from[dbo].[tran_venue] where[venueid] = " + venudid + "  and pay_status = 1  and convert(date, cast(VENUE_DATE as datetime),103) = convert(date, '" + DATE+ "', 103)) and id not in (select venuetypeid from[dbo].[TblDateBlock] t where[venueid] = " + venudid + " and tv.id = t.venuetypeid and convert(date, BlockDate,103)= convert(date, '" + DATE+ "', 103)) end else select '<span style=color:red>Book- ' + name + ' </span>' name from TYPE_VENUE tv where venuid = " + venudid + " and id  in (select[venuetypeid] from[dbo].[tran_venue] where[venueid] = " + venudid + "  and pay_status = 1  and convert(date, cast(VENUE_DATE as datetime) ,103)= convert(date, '" + DATE+ "', 103)) and id not in (select venuetypeid from[dbo].[TblDateBlock] t where[venueid] = " + venudid + " and tv.id = t.venuetypeid and convert(date, BlockDate,103)= convert(date, '" + DATE+ "', 103)) union all select  '<span style=color:green>Available- ' + name + '</span>' name from TYPE_VENUE tv where venuid = " + venudid + "  and id not  in (select[venuetypeid] from[dbo].[tran_venue] where[venueid] = " + venudid + "  and pay_status = 1  and convert(date, cast(VENUE_DATE as datetime),103) = convert(date, '" + DATE+ "', 103)) and id not in (select venuetypeid from[dbo].[TblDateBlock] t where[venueid] = " + venudid + " and tv.id = t.venuetypeid and convert(date, BlockDate,103)= convert(date, '" + DATE+ "', 103)) end ", con);


                com.CommandType = CommandType.Text;
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    obj = new SelectListItem();
                    //if (rdr["id"] != DBNull.Value)
                    //{
                    //    obj.Value = rdr["id"].ToString();
                    //}
                    if (rdr["name"] != DBNull.Value)
                    {
                        obj.Text = rdr["name"].ToString();
                    }
                    lst.Add(obj);
                }
            }
            return lst;
        }

        public List<SelectListItem> ListAllTimeSlots(string DATE)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            SelectListItem obj;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("select* from TblTimeSlot  ", con);
                com.CommandType = CommandType.Text;
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    obj = new SelectListItem();
                    if (rdr["id"] != DBNull.Value)
                    {
                        obj.Value = rdr["id"].ToString();
                    }
                    if (rdr["Timing"] != DBNull.Value)
                    {
                        obj.Text = rdr["Timing"].ToString();
                    }
                    lst.Add(obj);
                }
            }
            return lst;
        }

        public string getTiming(string timingVal)
        {
            string Gettiming = string.Empty;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("USPOnlineTraining", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", Convert.ToInt32(timingVal));
                com.Parameters.AddWithValue("@Action", "GetTiming");
                SqlDataReader rdr = com.ExecuteReader();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                while (rdr.Read())
                {
                    if (rdr["Timing"] != DBNull.Value)
                    {
                        Gettiming = rdr["Timing"].ToString();
                    }
                }
                con.Close();
            }
            return Gettiming;
        }

        public int AcceptedEmail(int? Id,string url)
        {
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("USPOnlineTraining", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", Id);
                com.Parameters.AddWithValue("@Status", "Accepted");
                com.Parameters.AddWithValue("@meetingurl", url);
                com.Parameters.AddWithValue("@cancelreason", "");
                com.Parameters.AddWithValue("@Action", "UpdateStatusForEmail");
                i = com.ExecuteNonQuery();
            }
            return i;
        }
        public int RejectedEmail(int? Id,string reason)
        {
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("USPOnlineTraining", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", Id);
                com.Parameters.AddWithValue("@Status", "Rejected");
                com.Parameters.AddWithValue("@meetingurl", "");
                com.Parameters.AddWithValue("@cancelreason", reason);
                com.Parameters.AddWithValue("@Action", "UpdateStatusForEmail");
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        public List<SelectListItem> GetReasonAccept()
        {
            List<SelectListItem> AccptReasionlst = new List<SelectListItem>();
            SelectListItem obj;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("USPOnlineTraining", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Action", "GetReasonAccept");
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    obj = new SelectListItem();
                    if (rdr["Id"] != DBNull.Value)
                    {
                        obj.Value = rdr["Id"].ToString();
                    }
                    if (rdr["Reason"] != DBNull.Value)
                    {
                        obj.Text = rdr["Reason"].ToString();
                    }
                    AccptReasionlst.Add(obj);
                }
            }
            return AccptReasionlst;
        }
        public List<SelectListItem> GetReasonReject()
        {
            List<SelectListItem> RejctReasionlst = new List<SelectListItem>();
            SelectListItem obj;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("USPOnlineTraining", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Action", "GetReasonReject");
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    obj = new SelectListItem();
                    if (rdr["Id"] != DBNull.Value)
                    {
                        obj.Value = rdr["Id"].ToString();
                    }
                    if (rdr["Reason"] != DBNull.Value)
                    {
                        obj.Text = rdr["Reason"].ToString();
                    }
                    RejctReasionlst.Add(obj);
                }
            }
            return RejctReasionlst;
        }

       

       
        public List<OnlineTraining> RegistrationListByDate(string Fromdate, string ToDate)
        {
            List<OnlineTraining> lst = new List<OnlineTraining>();
            OnlineTraining obj;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("USPOnlineTraining", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Fromdate", Fromdate);
                com.Parameters.AddWithValue("@ToDate", ToDate);
                com.Parameters.AddWithValue("@Action", "RegistrationListByDate");

                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    obj = new OnlineTraining();
                    if (rdr["Id"] != DBNull.Value)
                    {
                        obj.Id = Convert.ToInt32(rdr["Id"]);
                    }
                    if (rdr["CompanyName"] != DBNull.Value)
                    {
                        obj.CompanyName = rdr["CompanyName"].ToString();
                    }
                    if (rdr["SubscriberfromIndia"] != DBNull.Value)
                    {
                        obj.SubscriberfromIndia = rdr["SubscriberfromIndia"].ToString();
                    }
                    if (rdr["ContactPersonName"] != DBNull.Value)
                    {
                        obj.ContactPersonName = rdr["ContactPersonName"].ToString();
                    }
                    if (rdr["Designation"] != DBNull.Value)
                    {
                        obj.Designation = rdr["Designation"].ToString();
                    }
                    if (rdr["Email"] != DBNull.Value)
                    {
                        obj.Email = rdr["Email"].ToString();
                    }
                    if (rdr["ContactNumber"] != DBNull.Value)
                    {
                        obj.ContactNumber = rdr["ContactNumber"].ToString();
                    }
                    if (rdr["ExpectedAtendees"] != DBNull.Value)
                    {
                        obj.ExpectedAtendees = rdr["ExpectedAtendees"].ToString();
                    }
                    if (rdr["AtendeeType"] != DBNull.Value)
                    {
                        obj.AtendeeType = rdr["AtendeeType"].ToString();
                    }
                    if (rdr["Date"] != DBNull.Value)
                    {
                        obj.Date = rdr["Date"].ToString();
                    }
                    if (rdr["Timing"] != DBNull.Value)
                    {
                        obj.Timing = rdr["Timing"].ToString();
                    }
                    if (rdr["Status"] != DBNull.Value)
                    {
                        obj.Status = rdr["Status"].ToString();
                    }
                    else
                    {
                        obj.Status = "Pending";
                    }
                    lst.Add(obj);
                }
            }
            return lst;
        }
        public int AddBlockDate(DateBlock obj)
        {
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("USPOnlineTraining", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@BlockDate", obj.BlockDate);
                com.Parameters.AddWithValue("@Year", obj.Year);
                com.Parameters.AddWithValue("@Month", obj.Month);
                com.Parameters.AddWithValue("@Day", obj.Day);
                com.Parameters.AddWithValue("@Action", "AddBlockDate");
                i = com.ExecuteNonQuery();
            }
            return i;
        }
        public int AddBlockDates(DateBlock obj)
        {
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("USPOnlineTraining", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@BlockDate", obj.BlockDate);
                com.Parameters.AddWithValue("@AtendeeType", obj.sid);
                com.Parameters.AddWithValue("@Action", "AddBlockslot");
                i = com.ExecuteNonQuery();
            }
            return i;
        }
        public string ChckBlockDate(string Date)
        {
            string CheckDate = string.Empty;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("USPOnlineTraining", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("BlockDate", Date);
                com.Parameters.AddWithValue("@Action", "ChckBlockDate");
                SqlDataReader rdr = com.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        if (rdr["BlockDate"] != DBNull.Value)
                        {
                            CheckDate = rdr["BlockDate"].ToString();
                        }
                        else
                        {
                            CheckDate = null;
                        }
                    }
                }
                else
                {
                    CheckDate = null;
                }
            }
            return CheckDate;
        }
        public string ChckBlockDates(string Date)
        {
            string CheckDate = string.Empty;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("USPOnlineTraining", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("BlockDate", Date);
                com.Parameters.AddWithValue("@Action", "ChckBlockDate");
                SqlDataReader rdr = com.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        if (rdr["BlockDate"] != DBNull.Value)
                        {
                            CheckDate = rdr["BlockDate"].ToString();
                        }
                        else
                        {
                            CheckDate = null;
                        }
                    }
                }
                else
                {
                    CheckDate = null;
                }
            }
            return CheckDate;
        }
        public List<DateBlock> GetListBlockDateByDate(string Date)
        {
            List<DateBlock> lst = new List<DateBlock>();
            DateBlock obj;
            string Month = Date.Split('/')[1].ToString();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("USPOnlineTraining", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Month", Month);
                com.Parameters.AddWithValue("@Action", "GetListBlockDateByDate");

                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    obj = new DateBlock();
                    if (rdr["Id"] != DBNull.Value)
                    {
                        obj.Id = Convert.ToInt32(rdr["Id"]);
                    }
                    if (rdr["BlockDate"] != DBNull.Value)
                    {
                        obj.BlockDate = rdr["BlockDate"].ToString();
                    }
                    if (rdr["Year"] != DBNull.Value)
                    {
                        obj.Year = Convert.ToInt32(rdr["Year"]);
                    }
                    if (rdr["Month"] != DBNull.Value)
                    {
                        obj.Month = Convert.ToInt32(rdr["Month"]);
                    }
                    if (rdr["Day"] != DBNull.Value)
                    {
                        obj.Day = Convert.ToInt32(rdr["Day"]);
                    }
                    if (rdr["Status"] != DBNull.Value)
                    {
                        obj.Status = rdr["Status"].ToString();
                    }
                    lst.Add(obj);
                }
            }
            return lst;
        }
        public List<DateBlock> GetListBlockDateByDates(string Date)
        {
            List<DateBlock> lst = new List<DateBlock>();
            DateBlock obj;
            string Month = Date.Split('/')[1].ToString();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("USPOnlineTraining", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Month", Month);
                com.Parameters.AddWithValue("@Action", "GetListBlockslotByDate");

                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    obj = new DateBlock();
                    if (rdr["Id"] != DBNull.Value)
                    {
                        obj.Id = Convert.ToInt32(rdr["Id"]);
                    }
                    if (rdr["slodate"] != DBNull.Value)
                    {
                        obj.BlockDate = rdr["slodate"].ToString();
                    }
                   
                    if (rdr["slot"] != DBNull.Value)
                    {
                        obj.sid = (rdr["slot"]).ToString();
                    }
                 
                    lst.Add(obj);
                }
            }
            return lst;
        }
        public string ChkOnlineDate(string Date)
        {
            string GetDate = string.Empty;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("USPOnlineTraining", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Date", Date);
                com.Parameters.AddWithValue("@Action", "ChkOnlineDate");
                SqlDataReader rdr = com.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        if (rdr["Date"] != DBNull.Value)
                        {
                            GetDate = rdr["Date"].ToString();
                        }
                        else
                        {
                            GetDate = null;
                        }
                    }
                }
            }
            return GetDate;
        }
        public int DeleteDate(int Id)
        {
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("USPOnlineTraining", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", Id);
                com.Parameters.AddWithValue("@Action", "DeleteDate");
                i = com.ExecuteNonQuery();
            }
            return i;
        }
        public int DeleteDates(int Id)
        {
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("USPOnlineTraining", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", Id);
                com.Parameters.AddWithValue("@Action", "DeleteDates");
                i = com.ExecuteNonQuery();
            }
            return i;
        }
        public List<OnlineTraining> GetAllBlockDate()
        {
            List<OnlineTraining> Datelst = new List<OnlineTraining>();
            OnlineTraining obj;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("USPOnlineTraining", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Action", "GetAllBlockDate");
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    obj = new OnlineTraining();
                    if (rdr["BlockDate"] != DBNull.Value)
                    {
                        string date = rdr["BlockDate"].ToString();
                     
                        obj.Date = date.ToString();
                    }
                    Datelst.Add(obj);
                }
            }
            return Datelst;
        }
        public List<OnlineTraining> GetAllBlockDates()
        {
            List<OnlineTraining> Datelst = new List<OnlineTraining>();
            OnlineTraining obj;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("USPOnlineTraining", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Action", "GetAllBlockDate");
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    obj = new OnlineTraining();
                    if (rdr["BlockDate"] != DBNull.Value)
                    {
                        string date = rdr["BlockDate"].ToString();
                        DateTime dateTime = DateTime.ParseExact(date, "dd/MM/yyyy", null);
                        string valdate1 = dateTime.ToString("MM/dd/yyy");
                        obj.Date = valdate1.ToString();
                    }
                    Datelst.Add(obj);
                }
            }
            return Datelst;
        }
        public List<OnlineTraining> RegistrationListByStatus(string Status)
        {
            List<OnlineTraining> lst = new List<OnlineTraining>();
            OnlineTraining obj;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("USPOnlineTraining", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Status", Status);
                com.Parameters.AddWithValue("@Action", "RegistrationListByStatus");

                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    obj = new OnlineTraining();
                    if (rdr["Id"] != DBNull.Value)
                    {
                        obj.Id = Convert.ToInt32(rdr["Id"]);
                    }
                    if (rdr["CompanyName"] != DBNull.Value)
                    {
                        obj.CompanyName = rdr["CompanyName"].ToString();
                    }
                    if (rdr["SubscriberfromIndia"] != DBNull.Value)
                    {
                        obj.SubscriberfromIndia = rdr["SubscriberfromIndia"].ToString();
                    }
                    if (rdr["ContactPersonName"] != DBNull.Value)
                    {
                        obj.ContactPersonName = rdr["ContactPersonName"].ToString();
                    }
                    if (rdr["Designation"] != DBNull.Value)
                    {
                        obj.Designation = rdr["Designation"].ToString();
                    }
                    if (rdr["Email"] != DBNull.Value)
                    {
                        obj.Email = rdr["Email"].ToString();
                    }
                    if (rdr["ContactNumber"] != DBNull.Value)
                    {
                        obj.ContactNumber = rdr["ContactNumber"].ToString();
                    }
                    if (rdr["ExpectedAtendees"] != DBNull.Value)
                    {
                        obj.ExpectedAtendees = rdr["ExpectedAtendees"].ToString();
                    }
                    if (rdr["AtendeeType"] != DBNull.Value)
                    {
                        obj.AtendeeType = rdr["AtendeeType"].ToString();
                    }
                    if (rdr["Date"] != DBNull.Value)
                    {
                        obj.Date = rdr["Date"].ToString();
                    }
                    if (rdr["Timing"] != DBNull.Value)
                    {
                        obj.Timing = rdr["Timing"].ToString();
                    }
                    if (rdr["Status"] != DBNull.Value)
                    {
                        obj.Status = rdr["Status"].ToString();
                    }
                    else
                    {
                        obj.Status = "Pending";
                    }
                    lst.Add(obj);
                }
            }
            return lst;
        }
    }

}