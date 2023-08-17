using System;
using System.Collections.Generic;

namespace OnlineShop.DataAccess
{
    public partial class ProductDetail
    {
        public ProductDetail()
        {
            Carts = new HashSet<Cart>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ProductDetailId { get; set; }
        public int? ProductId { get; set; }
        public int? ColorId { get; set; }
        public int? SizeId { get; set; }
        public string? Image { get; set; }
        public int? ThumbnailId { get; set; }
        public int? Quantity { get; set; }

        public virtual Color? Color { get; set; }
        public virtual Product? Product { get; set; }
        public virtual Size? Size { get; set; }
        public virtual Thumbnail? Thumbnail { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
