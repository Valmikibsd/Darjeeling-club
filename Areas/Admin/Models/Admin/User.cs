using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClubApp.Areas.Admin.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string MobileNo { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public int CreateUser { get; set; }
        public string mod { get; set; }
        public int type { get; set; }
        public string ProfileName { get; set; }
        public int ProfileId { get; set; }

    }
}
