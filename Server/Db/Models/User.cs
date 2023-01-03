using System.Collections.Generic;

namespace OrdersSystem.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public virtual ICollection<Order> Orders { get; set; }


    }
}
