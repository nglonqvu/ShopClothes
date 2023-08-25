using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShop.DataAccess;
using OnlineShop.DataAccess.DAO;
using OnlineShop.Models;
using X.PagedList;

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

        public async Task<IActionResult> ProductList(string searchName, int? searchCategory, string priceOrder, int? page)
        {
            IQueryable<Product> productsQuery = db.Products.Include(p => p.Cate).Where(p => p.Status == 1);

            if (!string.IsNullOrEmpty(searchName))
            {
                productsQuery = productsQuery.Where(p => p.Name.Contains(searchName));
            }

            if (searchCategory.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.CateId == searchCategory.Value);
            }

            if (priceOrder == "asc")
            {
                productsQuery = productsQuery.OrderBy(p => p.Price);
            }
            else if (priceOrder == "desc")
            {
                productsQuery = productsQuery.OrderByDescending(p => p.Price);
            }

            int pageSize = 8;
            int pageNumber = page ?? 1;
            var products = await productsQuery.ToPagedListAsync(pageNumber, pageSize);

            ViewBag.SearchName = searchName;

            List<Category> categories = await db.Categories.ToListAsync();
            SelectList categoryList = new SelectList(categories, "CategoryId", "Name", searchCategory);
            ViewBag.CategoryList = categoryList;
            ViewBag.Categories = new SelectList(db.Categories, "CategoryId", "Name");

            List<SelectListItem> priceOrderList = new List<SelectListItem>
    {
        new SelectListItem { Text = "Tăng dần", Value = "asc" },
        new SelectListItem { Text = "Giảm dần", Value = "desc" }
    };
            ViewBag.PriceOrderList = priceOrderList;

            ViewBag.products = products;

            User user = await GetCurrentLoggedInUser();
            bool isLoggedIn = (user != null);
            ViewBag.IsLoggedIn = isLoggedIn;
            if (isLoggedIn)
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
                ViewBag.SearchName = searchName;
                ViewBag.CategoryList = categoryList;
                ViewBag.Categories = new SelectList(db.Categories, "CategoryId", "Name");
                ViewBag.PriceOrderList = priceOrderList;
                ViewBag.products = products;
                return View(model);
            }

            return View();
        }

        public async Task<IActionResult> ProductDetail(int productId)
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
                var viewModel = new ProductDetailModel
                {
                    Product = product,
                    ProductDetail = productDetail,
                    Colors = colors,
                    Sizes = sizes,
                    Thumbnails = thumbnails
                };
                ViewBag.ProductDetail = viewModel;

                User user = await GetCurrentLoggedInUser();
                bool isLoggedIn = (user != null);
                ViewBag.IsLoggedIn = isLoggedIn;
                if (isLoggedIn)
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
                    ViewBag.ProductDetail = viewModel;
                    return View(model);
                }

                return View();
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
