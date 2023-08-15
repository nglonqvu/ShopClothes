using System;
using System.Collections.Generic;

namespace OnlineShop.DataAccess;

public partial class Cart
{
    public int CartId { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public int Total { get; set; }

    public virtual Product Product { get; set; }

    public virtual User User { get; set; }
}
