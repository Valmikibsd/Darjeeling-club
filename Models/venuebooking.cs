namespace ClubApp.Models
{

    public class venuebooking
    {
        public List<venuebookingc> venuebookings { get; set; }
        public string Date { get; set; }
        public string Timing { get; set; }
        public string Status { get; set; }
        public string fullDate { get; set; }
        public string ToTime { get; set; }
        public string LoginDate { get; set; }
        public string orderId { get; set; }
        public string razorpayKey { get; set; }
        public Double amount { get; set; }
        public string currency { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string contactNumber { get; set; }
        public string address { get; set; }
        public string description { get; set; }
    }
        public class venuebookingc
    {
            public int Id { get; set; }
            public string name { get; set; }
            public string shortname { get; set; }
           public string memberrate { get; set; }
           public string photo { get; set; }
            public string nonmemberrate { get; set; }
        public int capcity { get; set; }
        

    }

  

}
