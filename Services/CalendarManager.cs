using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClubApp.Models;
using System.Globalization;
using System.Data;
using Microsoft.Extensions.Logging;
using Indiastat.Web.Model;
using Syncfusion.EJ2.Charts;

namespace ClubApp.Services
{
    public class CalendarManager
    {
        db_Utility Util = new db_Utility();
        ClsUtility ClsUtil = new ClsUtility();
        DBAccess db = new DBAccess();
        public WeekForMonth getCalender(int month, int year,int venuid)
        {

            WeekForMonth weeks = new WeekForMonth();
            weeks.Week1 = new List<Day>();
            weeks.Week2 = new List<Day>();
            weeks.Week3 = new List<Day>();
            weeks.Week4 = new List<Day>();
            weeks.Week5 = new List<Day>();
            weeks.Week6 = new List<Day>();

           

            List<venuec> venuecs = new List<venuec>();
            List<venuec> bvenuecs = new List<venuec>();

            List<DateTime> dt = new List<DateTime>();

            dt = GetDates(year, month);

            DataTable vdt = venuetype(venuid);

            int i = 1;
            foreach (DataRow drr in vdt.Rows)
            {
               
                venuecs.Add(new venuec
                {
                    VENUE = drr["Name"].ToString(),
                    id = drr["id"].ToString()
                });
                i++;
            }

            string SLOTBOOK = "";
            string SLOTBOOK6 = "";
            string SLOTBOOK5 = "";
            string SLOTBOOK4 = "";
            string SLOTBOOK3 = "";
            string SLOTBOOK2 = "";
            string SLOTBOOK1 = "";

            DataTable booking = new DataTable ();

            foreach (DateTime day in dt)
            {
                switch (GetWeekOfMonth(day))
                {
                    case 1:
                        Day dy1 = new Day();

                        booking = venuebooking(venuid, day.ToString("dd-MM-yyyy"));
                        SLOTBOOK1 = "";

                        foreach (DataRow drrr in booking.Rows)
                        {
                            SLOTBOOK1 += drrr["id"].ToString() + ',';

                            
                        }

                        dy1.Date = day;
                        dy1._Date = day.ToShortDateString();
                        dy1.dateStr = day.ToString("dd/MM/yyyy");
                        dy1.dtDay = day.Day;
                        dy1.daycolumn = GetDateInfo(dy1.Date);
                        dy1.venuecdata =venuecs;
                        dy1.flg = SLOTBOOK1;
                        dy1.bookedvenued = bvenuecs;
                        weeks.Week1.Add(dy1);
                        break;
                    case 2:
                         booking = venuebooking(venuid, day.ToString("dd-MM-yyyy"));
                        SLOTBOOK2 = "";
                        foreach (DataRow drrr in booking.Rows)
                        {
                            SLOTBOOK2 += drrr["id"].ToString() + ',';
                           

                        }
                        Day dy2 = new Day();
                        dy2.Date = day;
                        dy2._Date = day.ToShortDateString();
                        dy2.dateStr = day.ToString("dd/MM/yyyy");
                        dy2.dtDay = day.Day;
                        dy2.venuecdata = venuecs;
                        dy2.flg = SLOTBOOK2;
                        dy2.bookedvenued = bvenuecs;
                        dy2.daycolumn = GetDateInfo(dy2.Date);
                        weeks.Week2.Add(dy2);
                        break;
                    case 3:
                        booking = venuebooking(venuid, day.ToString("dd-MM-yyyy"));
                        SLOTBOOK3 = "";
                        foreach (DataRow drrr in booking.Rows)
                        {
                            SLOTBOOK3 += drrr["id"].ToString() + ',';
                           
                        }
                        Day dy3 = new Day();                     
                        dy3.Date = day;
                        dy3._Date = day.ToShortDateString();
                        dy3.dateStr = day.ToString("dd/MM/yyyy");
                        dy3.dtDay = day.Day;
                        dy3.flg = SLOTBOOK3;
                        dy3.bookedvenued = bvenuecs;
                        dy3.venuecdata = venuecs;
                        dy3.daycolumn = GetDateInfo(dy3.Date);
                        weeks.Week3.Add(dy3);
                        break;
                    case 4:
                        booking = venuebooking(venuid, day.ToString("dd-MM-yyyy"));
                        SLOTBOOK4 = "";
                      
                        foreach (DataRow drrr in booking.Rows)
                        {
                            SLOTBOOK4 += drrr["id"].ToString() + ',';
                           

                        }
                        Day dy4 = new Day();
                        dy4.Date = day;
                        dy4._Date = day.ToShortDateString();
                        dy4.dateStr = day.ToString("dd/MM/yyyy");
                        dy4.dtDay = day.Day;
                        dy4.bookedvenued = bvenuecs;
                        dy4.venuecdata = venuecs;
                        dy4.flg = SLOTBOOK4;
                        dy4.daycolumn = GetDateInfo(dy4.Date);
                        weeks.Week4.Add(dy4);
                        break;
                    case 5:
                        booking = venuebooking(venuid, day.ToString("dd-MM-yyyy"));
                        SLOTBOOK5 = "";
                        if(booking.Rows.Count > 0)
                        {
                            foreach (DataRow drrr in booking.Rows)
                            {
                                SLOTBOOK5 += drrr["id"].ToString() + ',';
                               

                            }
                        }
                        Day dy5 = new Day();
                        dy5.Date = day;
                        dy5.flg = SLOTBOOK5;
                        dy5._Date = day.ToShortDateString();
                        dy5.dateStr = day.ToString("dd/MM/yyyy"); ;
                        dy5.dtDay = day.Day;
                        dy5.venuecdata = venuecs;
                        dy5.bookedvenued = bvenuecs;
                        dy5.daycolumn = GetDateInfo(dy5.Date);
                        weeks.Week5.Add(dy5);
                        break;
                    case 6:
                        SLOTBOOK6 = "";
                        booking = venuebooking(venuid, day.ToString("dd-MM-yyyy"));
                        foreach (DataRow drrr in booking.Rows)
                        {

                            SLOTBOOK6 += drrr["id"].ToString()+',';

                        }
                        Day dy6 = new Day();
                        dy6.Date = day;
                        dy6._Date = day.ToShortDateString();
                        dy6.dateStr = day.ToString("dd/MM/yyyy");
                        dy6.dtDay = day.Day;
                        dy6.flg = SLOTBOOK6;
                        dy6.venuecdata = venuecs;
                        dy6.bookedvenued = bvenuecs;
                        dy6.daycolumn = GetDateInfo(dy6.Date);
                        weeks.Week6.Add(dy6);
                        break;
                };
            }

            while (weeks.Week1.Count < 7) // not starting from sunday
            {
                Day dy = null;
                weeks.Week1.Insert(0, dy);
            }

            if (month == 12)
            {
                weeks.nextMonth = (01).ToString() + "/" + (year + 1).ToString();
                weeks.prevMonth = (month - 1).ToString() + "/" + (year).ToString();
            }
            else if (month == 1)
            {
                weeks.nextMonth = (month + 1).ToString() + "/" + (year).ToString();
                weeks.prevMonth = (12).ToString() + "/" + (year - 1).ToString();
            }
            else
            {
                weeks.nextMonth = (month + 1).ToString() + "/" + (year).ToString();
                weeks.prevMonth = (month - 1).ToString() + "/" + (year).ToString();
            }

            return weeks;
        }

        public static List<DateTime> GetDates(int year, int month)
        {
            return Enumerable.Range(1, DateTime.DaysInMonth(year, month))  // Days: 1, 2 ... 31 etc.
                             .Select(day => new DateTime(year, month, day)) // Map each day to a date
                             .ToList();
        }

        public  DataTable venuetype(int id)
        {
            DataTable dt = Util.execQuerydt(" select * from TYPE_VENUE ");

         return dt;
           
        }
        public DataTable  venuebooking(int id, string date)
        {
            string  masg = "";
            // DataTable dt = Util.execQuerydt("select  venue_date,venuetypeid id from TYPE_VENUE t left join [tran_venue] tv on  t.id=tv.venuetypeid  where [venueid]="+ id + " AND  pay_status=1  and convert(date,venue_date ,103) =  convert(date,'" + date + "',103)");
            DataTable dt = Util.execQuerydt("select  venue_date,venuetypeid id from TYPE_VENUE t left join [tran_venue] tv on  t.id=tv.venuetypeid  where [venueid]="+ id + " AND  pay_status =1 and calflag=0  and convert(date,cast(venue_date as datetime) ,103) =  convert(date,'" + date + "',103) union all select  BlockDate venue_date, venuetypeid id from TYPE_VENUE td ,[TblDateBlock] t where td.id = t.venuetypeid and [venueid] = "+ id + " and convert(date, BlockDate,103) = convert(date, '" + date + "',103) ");


            return dt;

        }
        public static int GetWeekOfMonth(DateTime date)
        {
            DateTime beginningOfMonth = new DateTime(date.Year, date.Month, 1);
            while (date.Date.AddDays(1).DayOfWeek != DayOfWeek.Sunday)//CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                date = date.AddDays(1);
            return (int)Math.Truncate((double)date.Subtract(beginningOfMonth).TotalDays / 7f) + 1;
        }

        public int GetDateInfo(DateTime now)
        {
            int dayNumber = 0;
            DateTime dt = now.Date;
            string dayStr = Convert.ToString(dt.DayOfWeek);

            if (dayStr.ToLower() == "sunday")
            {
                dayNumber = 0;
            }
            else if (dayStr.ToLower() == "monday")
            {
                dayNumber = 1;
            }
            else if (dayStr.ToLower() == "tuesday")
            {
                dayNumber = 2;
            }
            else if (dayStr.ToLower() == "wednesday")
            {
                dayNumber = 3;
            }
            else if (dayStr.ToLower() == "thursday")
            {
                dayNumber = 4;
            }
            else if (dayStr.ToLower() == "friday")
            {
                dayNumber = 5;
            }
            else if (dayStr.ToLower() == "saturday")
            {
                dayNumber = 6;
            }
            return dayNumber;
        }

    }
}