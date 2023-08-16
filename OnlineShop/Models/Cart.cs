using System;
using System.Collections.Generic;

namespace OnlineShop.Models
{
    public partial class Cart
    {
        public int CartId { get; set; }
        public int? UserId { get; set; }
        public int? ProductDetailId { get; set; }
        public int? Quantity { get; set; }
        public int? Total { get; set; }

        public virtual ProductDetail ProductDetail { get; set; }
        public virtual User User { get; set; }
    }
}
