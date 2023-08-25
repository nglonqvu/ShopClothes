using Microsoft.AspNetCore.Mvc;
using OnlineShop.DataAccess;

namespace OnlineShop.Controllers
{
    public class OrderDetailController : Controller
    {
        private PRN211_BL5Context _context = new PRN211_BL5Context();
        public IActionResult Index(int? id)
        {
            List<OrderDetail> orderDetails = _context.OrderDetails.Where(x => x.OrderId == id).ToList();
            foreach (OrderDetail orderDetail in orderDetails)
            {
                orderDetail.ProductDetail = _context.ProductDetails.Where(x => x.ProductDetailId == orderDetail.ProductDetailId).SingleOrDefault();
            }
            ViewBag.list = orderDetails;
            return View();
        }
    }
}
