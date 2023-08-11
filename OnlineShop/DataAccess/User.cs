using System;
using System.Collections.Generic;

namespace OnlineShop.DataAccess;

public partial class User
{
    public int UserId { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? FullName { get; set; }

    public string? Phone { get; set; }

    public bool? Gender { get; set; }

    public DateTime? Dob { get; set; }

    public string? Avatar { get; set; }

    public string? Address { get; set; }

    public int? Role { get; set; }

    public bool? Status { get; set; }

    public string? CodeVerify { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role? RoleNavigation { get; set; }
}
