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
       
        public ManagerController(IWebHostEnvironment webHostEnvironment)
        {
           
            _userDAO = new UserDAO();
        }
        public async Task<IActionResult> Index()
        {
            User user = await GetCurrentLoggedInUser();
            bool isLoggedIn = (user != null);
            ViewBag.IsLoggedIn = isLoggedIn;
            if (!isLoggedIn )
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                using (PRN211_BL5Context context = new PRN211_BL5Context())
                {
                    var products = await context.Products
                        .Include(pr => pr.Cate)
                        .ToListAsync();
                    return View(products);
                }
            }

        }

        public async Task<ActionResult> Index2(int id, int? colorId, int? sizeId)
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



        // GET: ManagerController/Create
        public async Task<ActionResult> Create()
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


                using (PRN211_BL5Context context = new PRN211_BL5Context())
                {
                    var categories = context.Categories.ToList();
                    ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");

                }

                return View();
            }
        }

        // POST: ManagerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product)
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


                using (PRN211_BL5Context context = new PRN211_BL5Context())
                {
                    context.Products.Add(product);
                    context.SaveChanges();
                }
                return RedirectToAction("Index", "Manager");
            }
           
           
        }

        public async Task<ActionResult> Create2()
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

                using (PRN211_BL5Context context = new PRN211_BL5Context())
                {
                    var categories = context.Categories.ToList();
                    ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");

                    var productname = context.Products.ToList();
                    ViewBag.Products = new SelectList(productname, "ProductId", "Name");

                    var colorname = context.Colors.ToList();
                    ViewBag.Colors = new SelectList(colorname, "Id", "Name");

                    var sizename = context.Sizes.ToList();
                    ViewBag.Sizes = new SelectList(sizename, "SizeId", "Name");

                    var thumbnails = context.Thumbnails.ToList();
                    ViewBag.Thumbnails = new SelectList(thumbnails, "Thumbnail1", "Thumbnail1");

                }
                return View();
            }
        }

        // POST: ProductManager/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create2(ProductDetail prdetail, string thumbnailName)
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
        }

        // GET: ManagerController/Edit/5
        public async Task<ActionResult> Edit(int id)
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
        }

        // POST: ManagerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Product product)
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

            }
            
        }

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
