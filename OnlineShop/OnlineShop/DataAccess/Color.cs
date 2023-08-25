using System;
using System.Collections.Generic;

namespace OnlineShop.DataAccess
{
    public partial class Color
    {
        public Color()
        {
            ProductDetails = new HashSet<ProductDetail>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public int? ProductId { get; set; }

        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
