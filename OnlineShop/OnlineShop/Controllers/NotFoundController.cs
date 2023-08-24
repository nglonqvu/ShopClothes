using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.Controllers
{
    public class NotFoundController : Controller
    {
        public IActionResult Index()
        {
            Response.StatusCode = 404;
            return View();
        }
    }
}
