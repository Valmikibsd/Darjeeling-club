namespace ClubApp.Models
{

    public class Myevent
    {
        public List<Events> Events { get; set; }
    }
        public class Events
        {
            public int Id { get; set; }
            public string eventname { get; set; }
            public string eventdate { get; set; }
            public string image { get; set; }
        }
    
}
