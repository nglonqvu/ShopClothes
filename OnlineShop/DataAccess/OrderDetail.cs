﻿using System;
using System.Collections.Generic;

namespace OnlineShop.DataAccess;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public int Total { get; set; }

    public virtual Order Order { get; set; }

    public virtual Product Product { get; set; }
}
