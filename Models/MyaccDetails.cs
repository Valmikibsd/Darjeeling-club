using System.Globalization;

namespace ClubApp.Models
{
    public class MyaccDetails
    {
        public string memid { get; set; }
        public string PHOTO { get; set; }
        public string Name { get; set; }
        public string Dob { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        
        public int Age { get; set; }
        public SpouseDetails Spouse { get; set; }
        public List<DependentDetails> depdetails { get;set; }
    }
    public class SpouseDetails
    {
        public string spouseName { get; set; }
        public string dob { get; set; }
        public string SpouseIdNo { get; set; }
        public string MariageAniversary { get; set; }
        public string PHOTO { get; set; }
    }

    public class DependentDetails
    {
        public string depenName { get; set; }
        public string dob { get; set; }
        public string gender { get; set; }
        public string PHOTO { get; set; }
    }
}
