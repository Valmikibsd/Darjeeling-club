using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClubApp.Areas.Admin.Models
{
    public class ItemGroups
    {
        public int ItemGroupCode { set; get; }
        public string ItemGroup { set; get; }
        public int Status { set; get; }
    }
    public class ViewModel
    {
        public List<ItemGroups> Groups { set; get; }
        public List<ItemSubGroups> SubGroups { set; get; }
        public List<ItemMaster> Master { set; get; }
    }
    public class ContactForm
    {
        public int id { set; get; }
        public string FullName { set; get; }
        public string MobileNo { set; get; }
        public string Email { set; get; }
        public string Msg { set; get; }
    }
    public class ItemSubGroups
    {
        public int Itemsubgroupcode { set; get; }
        public int ItemGroupcode { set; get; }
        public string Itemsubgroup { set; get; }
        public string Itemgroup { set; get; }
        public int Status { set; get; }
    }
    public class ItemMaster
    {
        public int itemId { set; get; }
        public int Itemsubgroupcode { set; get; }
        public int ItemGroupcode { set; get; }
        public string Itemsubgroup { set; get; }
        public string Itemgroup { set; get; }

        public string Item_Image { set; get; }
        public string ItemName { set; get; }
        public string ItemPrice { set; get; }
        public int display { set; get; }
        public int ImageDisplay { set; get; }
    }

    public class Event
    {
        public int EventId { set; get; }
        public string EventName { set; get; }
        public string EventDate { set; get; }
        public string EventImage { set; get; }
        public int display { set; get; }
        public string Description { get; set; }

    }

    public class Gallery
    {
        public int Id { set; get; }
        public string GImage { set; get; }
        public int display { set; get; }
        public string name { set; get; }
        public int mainid { set; get; }
    }

    public class GalleryMain
    {
        public int Id { set; get; }
        public string name { set; get; }
        public string Image { set; get; }
        public int display { set; get; }

    }

    public class Affiliate
    {
        public int ID { set; get; }
        public string ClubName { set; get; }
        public string Address { set; get; }
        public string Email { set; get; }
        public string Contact { set; get; }
        public int Status { set; get; }
    }


    public class ManagingCommitte
    {
        public int ManagingId { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string photo { get; set; }
        public int OrderBy { get; set; }
        public string DESCRIPTION { get; set; }
    }
    public class Venue
    {
        public int Id { get; set; }
        public int venueid { get; set; }
        public string VenueName { get; set; }
        public string NAME { get; set; }
        public int venuetypeid { get; set; }
        public string BlockDate { get; set; }
    }
    public class TypeVenue
    {
        public int id { get; set; }
        public int venuid { get; set; }
        public string NAME { get; set; }
        public string VenueName { get; set; }
    }
    public class VenueMaster
    {
        public int ID { set; get; }
        public string VenueName { get; set; }
        public string ShortName { get; set; }
        public double RateForMember { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
    }
    public class TranVenue
    {
        public int id { get; set; }
        public int venueid { get; set; }
        public string VenueName { get; set; }
        public string NAME { get; set; }
        public int venuetypeid { get; set; }
        public string memberid { get; set; }
        public string MemberName { get; set; }
        public string VENUE_DATE { get; set; }
        public double cgst { get; set; }
        public double sgst { get; set; }
        public int pay_status { get; set; }
        public int PayMode { get; set; }
        public string Payname { get; set; }
        public double AC_Charge { get; set; }
        public double Maintenance_CHGS { get; set; }
        public double rental { get; set; }
        public DateTime curr_date { get; set; }
        public DateTime caldate { get; set; }
        public string partyno { get; set; }
        public double SecurityCharge { get; set; }
        public string RefrenceNo { get; set; }
        public DateTime CheckDate { get; set; }
        public string CheckNo { get; set; }
        public double Totalvenueamt { get; set; }
        public double totalvenuegst { get; set; }
        public string orderid { get; set; }
        public int flg { get; set; }
        public int CANCELFLAG { get; set; }
        public string Status { get; set; }
    }
    public class TranVenueCancel
    {
        public int id { get; set; }
        public int venueid { get; set; }
        public string VenueName { get; set; }
        public string NAME { get; set; }
        public int venuetypeid { get; set; }
        public string memberid { get; set; }
        public string MemberName { get; set; }
        public string VENUE_DATE { get; set; }
        public double cgst { get; set; }
        public double sgst { get; set; }
        public int pay_status { get; set; }
        public string Payname { get; set; }
        public double AC_Charge { get; set; }
        public double Maintenance_CHGS { get; set; }
        public double rental { get; set; }
        public DateTime curr_date { get; set; }
        public DateTime caldate { get; set; }
        public string partyno { get; set; }
        public double SecurityCharge { get; set; }
        public string RefrenceNo { get; set; }
        public DateTime CheckDate { get; set; }
        public string CheckNo { get; set; }
        public double Totalvenueamt { get; set; }
        public double totalvenuegst { get; set; }
        public string orderid { get; set; }
        public int flg { get; set; }
        public int CANCELFLAG { get; set; }
        public string VENUE_DATE1 { get; set; }
        public string VENUE_DATE2 { get; set; }
    }

    public class VenueReport
    {
        public int id { get; set; }
        public int venueid { get; set; }
        public string VenueName { get; set; }
        public string NAME { get; set; }
        public int venuetypeid { get; set; }
        public string memberid { get; set; }
        public string VENUE_DATE { get; set; }
        public string MemberName { get; set; }
        public string VENUE_DATE1 { get; set; }
        public string VENUE_DATE2 { get; set; }
    }

    public class AvailableVenueReport
    {
        public int id { get; set; }
        public string VenueName { get; set; }
        public string NAME { get; set; }
        public string VENUE_DATE1 { get; set; }
        public string VENUE_DATE2 { get; set; }
    }

    public class MemberBillReport
    {
        public int ID { get; set; }
        public string BillDate { get; set; }
        public string BillNo { get; set; }
        public string MemberID { get; set; }
        public double taxamount { get; set; }
        public double netamount { get; set; }
        public double OpeningBal { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string paymentno { get; set; }
        public int IsBilled { get; set; }
        public string p_entrydate { get; set; }
    }

    public class CMS_DROPDOWN
    {
        public int EventId { get; set; }
        public int id { get; set; }
        public string EventName { get; set; }
        public string name { get; set; }
    }

    public class OTP
    {
        public int OtoId { get; set; }
        public int OtpNumber { get; set; }
        public string Memid { get; set; }
        public string CreateDate { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
    public class MemberDetail
    {
        public string Membershipno { get; set; }
        public string Name { get; set; }
        public string PermanentMobile { get; set; }
        public string Email { get; set; }
        public string CreationDate { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
