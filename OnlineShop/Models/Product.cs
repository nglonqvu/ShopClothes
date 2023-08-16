using System;
using System.Collections.Generic;

namespace OnlineShop.Models
{
    public partial class Product
    {
        public Product()
        {
            ProductDetails = new HashSet<ProductDetail>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public int? CateId { get; set; }
        public decimal? Price { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }
        public int? Quantity { get; set; }

        public virtual Category Cate { get; set; }
        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
