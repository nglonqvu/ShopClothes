using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult ProductDetail()
        {
            return View();
        }
    }
}
