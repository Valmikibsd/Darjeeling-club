namespace ClubApp.Models
{
    public class Mybills
    {
        public List<Subscriptin> subscriptins { get; set; }
        public List<cardrecharge> cardrecharge { get; set; }
        public List<food> food { get; set; }
        public List<bar> bar { get; set; }
        public List<facility> facilities { get; set; }
        public List<managingcommitee> maging { get; set; }
        public List<affiliated> affilied { get; set; }

        public List<MYVENUEB> MUVENUEBOOKING { get; set; }

    }

    public class Subscriptin
    {
        public string billno { get; set; }
        public string amount { get; set; }
        public string billdate { get; set; }
        public string status { get; set; }
    }

    public class cardrecharge
    {
        public string billno { get; set; }
        public string amount { get; set; }
        public string billdate { get; set; }
    }

    public class food
    {
        public string billno { get; set; }
        public string amount { get; set; }
        public string billdate { get; set; }
    }


    public class bar
    {
        public string billno { get; set; }
        public string amount { get; set; }
        public string billdate { get; set; }
    }



    public class facility
    {
        public string billno { get; set; }
        public string amount { get; set; }
        public string billdate { get; set; }
    }

    public class managingcommitee
    {
        public string name { get; set; }
        public string photo { get; set; }
        public string digination { get; set; }
        public string DESCRIPTION { get; set; }
    }
    public class affiliated
    {
        public string clubname { get; set; }
        public string addresss { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
    }
    public class MYVENUEB
    {
        public string VenueName { get; set; }
        public string name { get; set; }
        public string partyno { get; set; }
        public string totalvenueamt { get; set; }
        public string curr_date { get; set; }
        public string venue_date { get; set; }
        public string RENTAL { get; set; }
        public string calcelamount { get; set; }

        public string STATUS { get; set; }
        public string refudamt { get; set; }
        public int tentivebooking { get; set; }
    }
  
}

