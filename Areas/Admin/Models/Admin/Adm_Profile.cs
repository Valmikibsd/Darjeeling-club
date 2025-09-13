using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClubApp.Areas.Admin.Models
{
    public class Adm_Profile
    {
        public int ProfileId { get; set; }
        public int Status { get; set; }

        public string mod { get; set; }
        public string ProfileName { get; set; }
        public string CreateUser { get; set; }
        public int type { get; set; }
    }
}
