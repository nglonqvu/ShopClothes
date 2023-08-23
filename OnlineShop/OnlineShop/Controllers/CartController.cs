using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.DataAccess;
using OnlineShop.Extentions;

namespace OnlineShop.Controllers
{
    public class CartController : Controller
    {
        private PRN211_BL5Context _context;

        public CartController(PRN211_BL5Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddToCart(int productId, int colorId, int quantity)
        {
            var productdetail = _context.ProductDetails.Include(pd => pd.Product).Include(pd => pd.Color).Include(pd => pd.Thumbnail).FirstOrDefault(pd => pd.ProductId == productId&&pd.ColorId==colorId);
            Dictionary<int, CartElement> Cart = HttpContext.Session.Get<Dictionary<int, CartElement>>("cart");
            if(Cart == null)
            {
                Cart = new Dictionary<int, CartElement>();
                Cart.Add(productdetail.ProductDetailId, new CartElement
                {
                    ProductName = productdetail.Product.Name,
                    Color = productdetail.Color.Name,
                    Thumbnail = productdetail.Thumbnail.Thumbnail1,
                    Price = productdetail.Product.Price,
                    Quantity = quantity     
                });
            }
            else
            {
                if (Cart.ContainsKey(productdetail.ProductDetailId))
                {
                    Cart[productdetail.ProductDetailId].Quantity += quantity;
                }
                else
                {
                    Cart.Add(productdetail.ProductDetailId, new CartElement
                    {
                        ProductName = productdetail.Product.Name,
                        Color = productdetail.Color.Name,
                        Thumbnail = productdetail.Thumbnail.Thumbnail1,
                        Price = productdetail.Product.Price,
                        Quantity = quantity
                    });
                }
            }
            HttpContext.Session.Set<Dictionary<int, CartElement>>("cart", Cart);
            return View("Index");
        }
    }

    public class CartElement
    {
        public string ProductName { get; set; }
        public string Color { get; set; }
        public decimal? Price { get; set; }
        public string Thumbnail { get; set; }
        public int Quantity { get; set; }
    }
}
