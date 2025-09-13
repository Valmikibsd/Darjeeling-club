using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Indiastat.Web.Model
{
    public class DateBlock
    {
        public int Id { get; set; }
        public string BlockDate { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<int> Month { get; set; }
        public Nullable<int> Day { get; set; }
        public string Status { get; set; }
        public string sid { get; set; }
    }
}