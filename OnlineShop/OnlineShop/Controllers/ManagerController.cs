using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.DataAccess;
using System.Threading.Tasks;
using OnlineShop.DataAccess.DAO;
using WebClothes.ultils;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineShop.Controllers
{
    public class ManagerController : Controller
    {
        private readonly UserDAO _userDAO;
        private readonly AuthenticateUser authenticateUser;
        private readonly Mailer mailer;
        private readonly Md5 md5;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ManagerController(IWebHostEnvironment webHostEnvironment)
        {
            authenticateUser = new AuthenticateUser();
            md5 = new Md5();
            mailer = new Mailer();
            _userDAO = new UserDAO();
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            User user = await GetCurrentLoggedInUser();
            bool isLoggedIn = (user != null);
            ViewBag.IsLoggedIn = isLoggedIn;
            //if (!isLoggedIn)
            //{
            //    return RedirectToAction("Index", "Login");
            //}
            //else
            //{
            //    using (PRN211_BL5Context context = new PRN211_BL5Context())
            //    {
            //        var products = await context.Products
            //            .Include(pr => pr.Cate)

            //            .ToListAsync();
            //        return View(products);
            //    }
            //}
            using (PRN211_BL5Context context = new PRN211_BL5Context())
            {
                var products = await context.Products
                    .Include(pr => pr.Cate)

                    .ToListAsync();
                return View(products);
            }
        }

        public ActionResult Index2(int id, int? colorId, int? sizeId)
        {
            using (PRN211_BL5Context context = new PRN211_BL5Context())
            {
                var product = context.Products
                    .Include(p => p.ProductDetails)
                        .ThenInclude(pd => pd.Color)
                    .Include(p => p.ProductDetails)
                        .ThenInclude(pd => pd.Size)
                    .Include(p => p.ProductDetails)
                        .ThenInclude(pd => pd.Thumbnail)
                    .FirstOrDefault(p => p.ProductId == id);

                if (product == null)
                {
                    return NotFound();
                }

                var productDetails = product.ProductDetails.AsQueryable();

                // Apply color filter if colorId is specified
                if (colorId.HasValue)
                {
                    productDetails = productDetails.Where(pd => pd.ColorId == colorId.Value);
                }

                // Apply size filter if sizeId is specified
                if (sizeId.HasValue)
                {
                    productDetails = productDetails.Where(pd => pd.SizeId == sizeId.Value);
                }
                ViewBag.Colors = context.Colors.ToList();
                ViewBag.Sizes = context.Sizes.ToList();

                return View(productDetails.ToList());
            }
        }

        private async Task<User> GetCurrentLoggedInUser()
        {
            string email = HttpContext.Session.GetString("Email");
            if (!string.IsNullOrEmpty(email))
            {
                return await _userDAO.GetUser(email);
            }
            return null;
        }


        // GET: ManagerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        // GET: ManagerController/Create
        public ActionResult Create()
        {
            using (PRN211_BL5Context context = new PRN211_BL5Context())
            {
                var categories = context.Categories.ToList();
                ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");
                
            }

            return View();
        }

        // POST: ManagerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            
                using (PRN211_BL5Context context = new PRN211_BL5Context())
                {
                    context.Products.Add(product);
                    context.SaveChanges();
                }
                return RedirectToAction("Index" , "Manager");
            
           
           
        }

        public ActionResult Create2()
        {
            using (PRN211_BL5Context context = new PRN211_BL5Context())
            {
                var categories = context.Categories.ToList();
                ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");

                var productname = context.Products.ToList();
                ViewBag.Products = new SelectList(productname, "ProductId", "Name");

                var colorname = context.Colors.ToList();
                ViewBag.Colors = new SelectList(colorname, "Id","Name");

                var sizename = context.Sizes.ToList();
                ViewBag.Sizes = new SelectList(sizename, "SizeId", "Name");

                var thumbnails = context.Thumbnails.ToList();
                ViewBag.Thumbnails = new SelectList(thumbnails, "Thumbnail1", "Thumbnail1");

            }
            return View();
        }

        // POST: ProductManager/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create2(ProductDetail prdetail, string thumbnailName)
        {
            if (ModelState.IsValid)
            {
                using (PRN211_BL5Context context = new PRN211_BL5Context())
                {
                    // Tạo một thumbnail mới
                    Thumbnail newThumbnail = new Thumbnail
                    {
                        Thumbnail1 = thumbnailName
                    };
                    context.Thumbnails.Add(newThumbnail);
                    context.SaveChanges();

                    // Gán ThumbnailId cho sản phẩm mới
                    prdetail.ThumbnailId = newThumbnail.Id;
                    context.ProductDetails.Add(prdetail);
                    context.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }

            // Nếu ModelState không hợp lệ, cần load lại danh sách categories và trả về View
            using (PRN211_BL5Context context = new PRN211_BL5Context())
            {
                var categories = context.Categories.ToList();
                ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");
            }

            return View(prdetail);
        }

        // GET: ManagerController/Edit/5
        public ActionResult Edit(int id)
        {
            using (PRN211_BL5Context context = new PRN211_BL5Context())
            {
                var product = context.Products.FirstOrDefault(p => p.ProductId == id);
                if (product == null)
                {
                    return NotFound();
                }

                var categories = context.Categories.ToList();

                ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");
                return View(product);
            }
        }

        // POST: ManagerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product product)
        {
            
                using (PRN211_BL5Context context = new PRN211_BL5Context())
                {
                    var existingProduct = context.Products.FirstOrDefault(p => p.ProductId == id);

                    if (existingProduct == null)
                    {
                        return NotFound();
                    }

                    // Update existing product properties
                    existingProduct.Name = product.Name;
                    existingProduct.CateId = product.CateId;
                    existingProduct.Price = product.Price;
                    existingProduct.Image = product.Image;
                    existingProduct.Description = product.Description;
                    existingProduct.Status = product.Status;

                   

                    context.SaveChanges(); // Save changes to update the product
                }

                return RedirectToAction("Index", "Manager");
            

            // If ModelState is not valid, reload categories and thumbnails and return the view
            using (PRN211_BL5Context context = new PRN211_BL5Context())
            {
                var categories = context.Categories.ToList();

                ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");
            }

            return View(product);
        }

        // GET: ManagerController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                using (PRN211_BL5Context context = new PRN211_BL5Context())
                {
                    var product = context.Products.FirstOrDefault(p => p.ProductId == id);

                    if (product == null)
                    {
                        return NotFound();
                    }

                    context.Products.Remove(product);
                    context.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
       
        public ActionResult Delete2(int id)
        {
            try
            {
                using (PRN211_BL5Context context = new PRN211_BL5Context())
                {
                    var pdetail = context.ProductDetails.FirstOrDefault(p => p.ProductDetailId == id);

                    if (pdetail == null)
                    {
                        return NotFound();
                    }

                    context.ProductDetails.Remove(pdetail);
                    context.SaveChanges();
                }

                return RedirectToAction("Index" , "Manager" );
            }
            catch
            {
                return View();
            }
        }

    }
}
