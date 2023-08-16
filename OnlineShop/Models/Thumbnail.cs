using System;
using System.Collections.Generic;

namespace OnlineShop.Models
{
    public partial class Thumbnail
    {
        public Thumbnail()
        {
            ProductDetails = new HashSet<ProductDetail>();
        }

        public int Id { get; set; }
        public string Thumbnail1 { get; set; }

        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
