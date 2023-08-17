using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.Controllers
{
    public class LogoutController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Session.Remove("Email");
            return RedirectToAction("Index", "Home");
        }
    }
}
