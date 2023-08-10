using System;
using System.Collections.Generic;

namespace OnlineShop.DataAccess;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public int? CateId { get; set; }

    public int Quantity { get; set; }

    public double Price { get; set; }

    public string Description { get; set; } = null!;

    public string Image { get; set; } = null!;

    public int? Status { get; set; }

    public virtual Category? Cate { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
