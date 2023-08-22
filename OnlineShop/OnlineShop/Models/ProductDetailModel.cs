using OnlineShop.DataAccess;

namespace OnlineShop.Models
{
    public class ProductDetailModel
    {
        public Product Product { get; set; }
        public ProductDetail ProductDetail { get; set; }
        public List<Color> Colors { get; set; }
        public List<Size> Sizes { get; set; }
        public List<Thumbnail> Thumbnails { get; set; }
    }
}
