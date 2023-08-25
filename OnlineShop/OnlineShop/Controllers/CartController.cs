﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.DataAccess;
using OnlineShop.DataAccess.DAO;
using OnlineShop.Models;
using System.Drawing;

namespace OnlineShop.Controllers
{
    public class CartController : Controller
    {
        private readonly PRN211_BL5Context _context;
        private readonly UserDAO userDAO;
        private readonly ILogger<HomeController> _logger;

        public CartController(ILogger<HomeController> logger)
        {
            _context = new PRN211_BL5Context();
            userDAO = new UserDAO();
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddToCart(int productId, int colorId, int quantity, int sizeId)
        {
            // Lấy thông tin sản phẩm
            var productdetail = _context.ProductDetails
                .Include(pd => pd.Product)
                .Include(pd => pd.Color)
                .Include(pd => pd.Thumbnail)
                .Include(pd => pd.Size)
                .FirstOrDefault(pd => pd.ProductId == productId && pd.ColorId == colorId && pd.SizeId == sizeId);

            // Lấy thông tin người dùng đang đăng nhập
            User user = await GetCurrentLoggedInUser();

            // Kiểm tra xem người dùng đã đăng nhập hay chưa
            bool isLoggedIn = (user != null);
            ViewBag.IsLoggedIn = isLoggedIn;

            if (!isLoggedIn)
            {
                // Nếu người dùng chưa đăng nhập, chuyển hướng đến trang đăng nhập
                return RedirectToAction("Index", "Login");
            }
            else
            {
                var existingCartItem = _context.Carts
                .FirstOrDefault(c => c.UserId == user.UserId && c.ProductDetailId == productdetail.ProductDetailId);

                if (existingCartItem != null && _context.Carts.ToList().Count() != 0)
                {
                    // Nếu sản phẩm đã có trong giỏ hàng, tăng số lượng sản phẩm
                    existingCartItem.Quantity += quantity;
                    existingCartItem.Total = (int)productdetail.Product.Price * existingCartItem.Quantity;
                }
                else
                {
                    // Tạo đối tượng Cart để lưu vào cơ sở dữ liệu
                    var cartDatabaseItem = new Cart
                    {
                        UserId = user.UserId,
                        ProductDetailId = productdetail.ProductDetailId,
                        Quantity = quantity,
                        Total = (int)productdetail.Product.Price * quantity
                    };

                    // Thêm đối tượng Cart vào cơ sở dữ liệu
                    _context.Carts.Add(cartDatabaseItem);
                }
                await _context.SaveChangesAsync();

                // Lấy tất cả các sản phẩm trong giỏ hàng của người dùng từ cơ sở dữ liệu
                var userCartItems = _context.Carts
                    .Where(c => c.UserId == user.UserId)
                    .ToList();

                // Tạo danh sách CartElement để biểu diễn thông tin sản phẩm trong giỏ hàng
                var cartElements = new List<CartElement>();

                foreach (var cartItem in userCartItems)
                {
                    var productDetail = _context.ProductDetails
                        .Include(pd => pd.Product)
                        .Include(pd => pd.Color)
                        .Include(pd => pd.Thumbnail)
                        .Include(pd => pd.Size)
                        .FirstOrDefault(pd => pd.ProductDetailId == cartItem.ProductDetailId);

                    if (productDetail != null)
                    {
                        var cartElement = new CartElement
                        {
                            ProductDetailId = productDetail.ProductDetailId,
                            ProductName = productDetail.Product.Name,
                            Color = productDetail.Color.Name,
                            Thumbnail = productDetail.Thumbnail.Thumbnail1,
                            Price = productDetail.Product.Price,
                            Quantity = (int)cartItem.Quantity,
                            Size = productDetail.Size.Name
                        };

                        cartElements.Add(cartElement);
                    }
                }

                // Gán danh sách CartElement vào ViewBag.Cart để hiển thị trong view
                ViewBag.Cart = cartElements;

                // Tạo model cho view
                ProfileModel model = new ProfileModel
                {
                    FullName = user.FullName,
                    Email = user.Email,
                    Avatar = user.Avatar,
                    Address = user.Address,
                    Birthday = user.Dob,
                    Gender = user.Gender,
                    PhoneNumber = user.Phone,
                    RoleName = user.RoleNavigation?.RoleName
                };

                return View("Index", model);
            }
        }



        public async Task<IActionResult> DelCart(int id)
        {
            // Lấy thông tin người dùng đang đăng nhập
            User user = await GetCurrentLoggedInUser();

            // Kiểm tra xem người dùng đã đăng nhập hay chưa
            bool isLoggedIn = (user != null);
            ViewBag.IsLoggedIn = isLoggedIn;

            if (!isLoggedIn)
            {
                // Nếu người dùng chưa đăng nhập, chuyển hướng đến trang đăng nhập
                return RedirectToAction("Index", "Login");
            }
            else
            {
                var productdelete = _context.Carts.FirstOrDefault(c => c.UserId == user.UserId && c.ProductDetailId == id);

                _context.Carts.Remove(productdelete);
            }
            await _context.SaveChangesAsync();

            // Lấy tất cả các sản phẩm trong giỏ hàng của người dùng từ cơ sở dữ liệu
            var userCartItems = _context.Carts
                .Where(c => c.UserId == user.UserId)
                .ToList();

            // Tạo danh sách CartElement để biểu diễn thông tin sản phẩm trong giỏ hàng
            var cartElements = new List<CartElement>();

            foreach (var cartItem in userCartItems)
            {
                var productDetail = _context.ProductDetails
                    .Include(pd => pd.Product)
                    .Include(pd => pd.Color)
                    .Include(pd => pd.Thumbnail)
                    .Include(pd => pd.Size)
                    .FirstOrDefault(pd => pd.ProductDetailId == cartItem.ProductDetailId);

                if (productDetail != null)
                {
                    var cartElement = new CartElement
                    {
                        ProductDetailId = productDetail.ProductDetailId,
                        ProductName = productDetail.Product.Name,
                        Color = productDetail.Color.Name,
                        Thumbnail = productDetail.Thumbnail.Thumbnail1,
                        Price = productDetail.Product.Price,
                        Quantity = (int)cartItem.Quantity,
                        Size = productDetail.Size.Name
                    };

                    cartElements.Add(cartElement);
                }
            }

            // Gán danh sách CartElement vào ViewBag.Cart để hiển thị trong view
            ViewBag.Cart = cartElements;

            // Tạo model cho view
            ProfileModel model = new ProfileModel
            {
                FullName = user.FullName,
                Email = user.Email,
                Avatar = user.Avatar,
                Address = user.Address,
                Birthday = user.Dob,
                Gender = user.Gender,
                PhoneNumber = user.Phone,
                RoleName = user.RoleNavigation?.RoleName
            };

            return View("Index", model);
        }

        public async Task<IActionResult> ListCartItems()
        {
            User user = await GetCurrentLoggedInUser();

            // Kiểm tra xem người dùng đã đăng nhập hay chưa
            bool isLoggedIn = (user != null);
            ViewBag.IsLoggedIn = isLoggedIn;

            if (!isLoggedIn)
            {
                // Nếu người dùng chưa đăng nhập, chuyển hướng đến trang đăng nhập
                return RedirectToAction("Index", "Login");
            }
            else
            {
                // Lấy tất cả các sản phẩm trong giỏ hàng của người dùng từ cơ sở dữ liệu
                var userCartItems = _context.Carts
                    .Where(c => c.UserId == user.UserId)
                    .ToList();

                // Tạo danh sách CartElement để biểu diễn thông tin sản phẩm trong giỏ hàng
                var cartElements = new List<CartElement>();

                foreach (var cartItem in userCartItems)
                {
                    var productDetail = _context.ProductDetails
                        .Include(pd => pd.Product)
                        .Include(pd => pd.Color)
                        .Include(pd => pd.Thumbnail)
                        .Include(pd => pd.Size)
                        .FirstOrDefault(pd => pd.ProductDetailId == cartItem.ProductDetailId);

                    if (productDetail != null)
                    {
                        var cartElement = new CartElement
                        {
                            ProductDetailId = productDetail.ProductDetailId,
                            ProductName = productDetail.Product.Name,
                            Color = productDetail.Color.Name,
                            Thumbnail = productDetail.Thumbnail.Thumbnail1,
                            Price = productDetail.Product.Price,
                            Quantity = (int)cartItem.Quantity,
                            Size = productDetail.Size.Name
                        };

                        cartElements.Add(cartElement);
                    }
                    ViewBag.Cart = cartElements;
                }
            }
            ProfileModel model = new ProfileModel
            {
                FullName = user.FullName,
                Email = user.Email,
                Avatar = user.Avatar,
                Address = user.Address,
                Birthday = user.Dob,
                Gender = user.Gender,
                PhoneNumber = user.Phone,
                RoleName = user.RoleNavigation?.RoleName
            };

            return View("Index", model);
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
        public IActionResult Checkout(int? id)
        {
            User uid = _context.Users.Where(x => x.UserId == id).FirstOrDefault();
            if (uid != null)
            {
                ViewBag.id = uid.UserId;
                ViewBag.name = uid.FullName;
                ViewBag.phone = uid.Phone;
                ViewBag.add = uid.Address;
            }

            return View();
        }

        [HttpPost]
        public IActionResult Checkout()
        {

            string uid = HttpContext.Session.GetString("id");
            string sum1 = HttpContext.Request.Form["sum"];
            Order order = new Order();
            order.UserId = int.Parse(uid);
            order.OrderDate = DateTime.Now;
            order.Status = 1;
            decimal sumDecimal = decimal.Parse(sum1);
            int sumAsInt = (int)sumDecimal;
            order.Total = sumAsInt;
            _context.Add(order);
            _context.SaveChanges();
            Dictionary<int, CartElement> cid = HttpContext.Session.Get<Dictionary<int, CartElement>>("cart");
            var oid = _context.Orders.AsQueryable().OrderByDescending(o => o.OrderId).FirstOrDefault()?.OrderId;
            if (oid != null && oid != 0)
            {
                foreach (KeyValuePair<int, CartElement> i in cid)
                {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.OrderId = oid;
                    orderDetail.ProductDetailId = i.Key;
                    orderDetail.Quantity = i.Value.Quantity;
                    orderDetail.Total = (int)i.Value.Price;
                    ProductDetail product = _context.ProductDetails.Where(x => x.ProductDetailId == i.Key).FirstOrDefault();
                    product.Quantity -= i.Value.Quantity;
                    _context.OrderDetails.Add(orderDetail);
                    _context.ProductDetails.Update(product);
                    _context.SaveChanges();
                }
            }


            IDictionary<int, CartElement> cart = new Dictionary<int, CartElement>();


            // sau khi thanh toan xong can xoa het cac sp trong cart
            cart = new Dictionary<int, CartElement>();
            string json = System.Text.Json.JsonSerializer.Serialize(cart);
            HttpContext.Session.SetString("cart", json);


            return RedirectToAction("Index");
        }
    }



    public class CartElement
    {
        public int ProductDetailId { get; set; }
        public string ProductName { get; set; }
        public string Color { get; set; }
        public decimal? Price { get; set; }
        public string Thumbnail { get; set; }
        public int Quantity { get; set; }

        public string Size { get; set; }
    }
}
