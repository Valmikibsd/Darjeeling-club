using Newtonsoft.Json;

namespace DarjeelingClubApp.Areas.Admin.Models
{
    public class ReminderModel
    {
        [JsonProperty("memid")]
        public string MemberId { get; set; }
        [JsonProperty("amount")]
        public string amount { get; set; }
        [JsonProperty("type")]
        public int type { get; set; }
        [JsonProperty("month")]
        public string month { get; set; }
        [JsonProperty("name")]
        public string MemberName { get; set; }
        public string address { get; set; }
        [JsonProperty("cat")]
        public string category { get; set; }
        [JsonProperty("contactno")]
        public string ContactNo { get; set; }
        public string remindertype { get; set; }
        public string email { get; set; }
        public string FirstRDate { get; set; }
        public string SecondRdate { get; set; }
        public string stipulated { get; set; }
        public string refno { get; set; }
        public string lastrefno { get; set; }
        public string thirddate { get; set; }
        public string amountinwords { get; set; } 
        public  string totamt { get; set; }
        public  string monthtext { get; set; }
    }
}
