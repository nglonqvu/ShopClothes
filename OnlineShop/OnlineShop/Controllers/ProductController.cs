using Microsoft.AspNetCore.Mvc;
using OnlineShop.DataAccess;
using OnlineShop.DataAccess.DAO;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly PRN211_BL5Context db;
        private readonly UserDAO userDAO;
        private readonly ILogger<HomeController> _logger;

        public ProductController(ILogger<HomeController> logger)
        {
            db = new PRN211_BL5Context();
            userDAO = new UserDAO();
            _logger = logger;
        }

        public async Task<IActionResult> ProductDetail(int productId)
        {
            User user = await GetCurrentLoggedInUser();
            bool isLoggedIn = (user != null);
            ViewBag.IsLoggedIn = isLoggedIn;
            if (!isLoggedIn)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
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
                try
                {

                    var product = db.Products.FirstOrDefault(p => p.ProductId == productId);
                    var firstColorId = db.ProductDetails.Where(e => e.ProductId == productId).FirstOrDefault()?.ColorId;
                    var productDetail = db.ProductDetails.FirstOrDefault(p => p.ProductId == productId && p.ColorId == firstColorId);
                    if (productDetail == null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        var colorIds = db.ProductDetails
                            .Where(p => p.ProductId == productId)
                            .Select(p => p.ColorId)
                            .ToList();
                        var colors = db.Colors.Where(c => colorIds.Contains(c.Id)).ToList();
                        var sizeIds = db.ProductDetails
                                .Where(p => p.ProductId == productId)
                                .Select(p => p.SizeId)
                                .ToList();
                        var sizes = db.Sizes.Where(p => sizeIds.Contains(p.SizeId)).ToList();
                        var thumbnailIds = db.ProductDetails
                            .Where(p => p.ProductId == productId && p.ColorId == firstColorId)
                            .Select(p => p.ThumbnailId)
                            .ToList();
                        var thumbnails = db.Thumbnails
                            .Where(t => thumbnailIds.Contains(t.Id))
                            .ToList();
                        ViewBag.ProfileViewModel = model;
                        var viewModel = new ProductDetailModel
                        {
                            Product = product,
                            ProductDetail = productDetail,
                            Colors = colors,
                            Sizes = sizes,
                            Thumbnails = thumbnails
                        };                   
                        return View(viewModel);
                    }
                }
                catch
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }

            [HttpPost]
            public IActionResult ChangeImageByColor(int productId, int colorId)
            {
                var ColorProduct = db.ProductDetails.FirstOrDefault(c => c.ColorId == colorId);

                if (ColorProduct != null)
                {
                    var thumbnailIds = db.ProductDetails
                            .Where(p => p.ProductId == productId && p.ColorId == colorId)
                            .Select(p => p.ThumbnailId)
                            .ToList();
                    var thumbnails = db.Thumbnails
                            .Where(t => thumbnailIds.Contains(t.Id))
                            .ToList();
                    string newImageUrl = ColorProduct.Image;
                    string newThumbnailImageUrl = "";
                    foreach (var thumbnail in thumbnails)
                    {
                        newThumbnailImageUrl = thumbnail.Thumbnail1;
                    }
                    var result = new
                    {
                        MainImageUrl = newImageUrl,
                        ThumbnailImageUrl = newThumbnailImageUrl,
                        colorId = colorId
                    };
                    return Json(result);
                }

                return Json(null);
            }

            [HttpPost]
            public IActionResult GetQuantityByColorAndSize(int productId, int colorId, int sizeId)
            {
                var quantity = db.ProductDetails
                    .Where(p => p.ProductId == productId && p.ColorId == colorId && p.SizeId == sizeId)
                    .Select(p => p.Quantity)
                    .FirstOrDefault();

                return Json(quantity);
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
