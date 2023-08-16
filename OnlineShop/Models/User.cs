using System;
using System.Collections.Generic;

namespace OnlineShop.Models
{
    public partial class User
    {
        public User()
        {
            Carts = new HashSet<Cart>();
            Orders = new HashSet<Order>();
        }

        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public bool? Gender { get; set; }
        public DateTime? Dob { get; set; }
        public string Avatar { get; set; }
        public string Address { get; set; }
        public int? Role { get; set; }
        public bool? Status { get; set; }
        public string CodeVerify { get; set; }

        public virtual Role RoleNavigation { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
