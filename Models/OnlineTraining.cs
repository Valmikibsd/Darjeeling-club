using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Indiastat.Web.Model
{
    public class OnlineTraining
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter Company Name"), MaxLength(30)]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "Please Select Subscriber Type")]
        public string SubscriberfromIndia { get; set; }
        [Required(ErrorMessage = "Please enter Contact Person Name"), MaxLength(30)]
        [Display(Name = "Contact Person Name")]
        public string ContactPersonName { get; set; }
        [Required(ErrorMessage = "Please enter Designation"), MaxLength(30)]
        [Display(Name = "Designation")]
        public string Designation { get; set; }
        [Required(ErrorMessage = "Please enter your email")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        [MaxLength(100)]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        //[RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "You must provide a phone number")]
        [Display(Name = "Home Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string ContactNumber { get; set; }
        [Required(ErrorMessage = "Please Select Expected Atendees")]
        [Display(Name = "Expected Atendees")]
        public string ExpectedAtendees { get; set; }
        [Required(ErrorMessage = "Please Select Attendee Type")]
        public string AtendeeType { get; set; }
        [Required(ErrorMessage = "Please enter Date")]
        [Display(Name = "Date")]
        public string Date { get; set; }
        public string Timing { get; set; }
        public string Status { get; set; }
        public string fullDate { get; set; }
        public string ToTime { get; set; }
        public string LoginDate { get; set; }

        public string datas { get; set; }
    }
}