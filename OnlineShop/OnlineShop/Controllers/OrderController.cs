using Microsoft.AspNetCore.Mvc;
using OnlineShop.DataAccess;
using OnlineShop.DataAccess.DAO;
using OnlineShop.Models;
using System.Diagnostics;

namespace OnlineShop.Controllers
{
    public class OrderController : Controller
    {
        private PRN211_BL5Context _context = new PRN211_BL5Context();
        private readonly UserDAO userDAO;
        private readonly ProductDAO productDAO;
        private readonly CategoryDAO categoryDAO;

        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            userDAO = new UserDAO();
            productDAO = new ProductDAO();
            categoryDAO = new CategoryDAO();
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            User user = await GetCurrentLoggedInUser();
            bool isLoggedIn = (user != null);
            ViewBag.IsLoggedIn = isLoggedIn;
            List<Order> orders = _context.Orders.ToList();
            ViewBag.list = orders.OrderByDescending(x => x.OrderId);
            return View();
        }
        private async Task<User> GetCurrentLoggedInUser()
        {
            string email = HttpContext.Session.GetString("Email");
            if (!string.IsNullOrEmpty(email))
            {
                return await userDAO.GetUser(email);
            }
            return null;
        }
    }
}
