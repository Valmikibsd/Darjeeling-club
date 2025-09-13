using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClubApp.Areas.Admin.Models
{
    public class Adm_User
    {
        public string uname { get; set; }
        public string pwd { get; set; }
        public string deviceid { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string address { get; set; }
    }
}
