﻿using System;
using System.Collections.Generic;

namespace OnlineShop.DataAccess;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; }

    public int? CateId { get; set; }

    public int Quantity { get; set; }

    public string Image { get; set; }

    public double Price { get; set; }

    public string Size { get; set; }

    public string Description { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Category Cate { get; set; }

    public virtual ICollection<Color> Colors { get; set; } = new List<Color>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}