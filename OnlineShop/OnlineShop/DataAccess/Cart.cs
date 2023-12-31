﻿using System;
using System.Collections.Generic;

namespace OnlineShop.DataAccess
{
    public partial class Cart
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int ProductDetailId { get; set; }
        public int? Quantity { get; set; }
        public int? Total { get; set; }

        public virtual ProductDetail ProductDetail { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
