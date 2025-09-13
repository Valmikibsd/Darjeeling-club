using System.Collections.Generic;

namespace ClubApp.Models {
    public class OrderStatusViewModel
    {
        public Dictionary<string, string> Order { get; set; }

        public Dictionary<string, string> RequestParams { get; set; }

        public string Message { get; set; }
        public string ORDERID { get; set; }
        public string amt { get; set; }
        public string paymentids { get; set; }
        public string payment_method_type { get; set; }
        public string custid { get; set; }
    }
}


