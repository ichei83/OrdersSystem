using System;

namespace OrdersSystem.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public DateTime  Date { get; set; }
        public string Status { get; set; }
        public string Details { get; set; }
        public string CustomerSite { get; set; }
        public string Agent { get; set; }
        public User User { get; set; }

    }
}
