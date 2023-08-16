using System;
using System.Collections.Generic;

namespace OnlineShop.Models
{
    public partial class Size
    {
        public Size()
        {
            ProductDetails = new HashSet<ProductDetail>();
        }

        public int SizeId { get; set; }
        public string Name { get; set; }
        public int? ProductId { get; set; }

        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
