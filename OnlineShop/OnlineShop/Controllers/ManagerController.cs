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
            int? userRole = user?.Role;
            if (!isLoggedIn /*|| (userRole.HasValue && userRole.Value == 2)*/)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (userRole.HasValue && userRole.Value == 2)
            {
                return RedirectToAction("Index", "NotFound");
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

        public async Task<ActionResult> Index2(int id, string newColor, string newSize, string newThumbnail)
        {
            User user = await GetCurrentLoggedInUser();
            bool isLoggedIn = (user != null);
            ViewBag.IsLoggedIn = isLoggedIn;
            int? userRole = user?.Role;
            if (!isLoggedIn)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (userRole.HasValue && userRole.Value == 2)
            {
                return RedirectToAction("Index", "NotFound");
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
                        Color colorToAdd = new Color
                        {
                            Name = newColor,
                            ProductId = product.ProductId,
                        };
                        context.Colors.Add(colorToAdd);
                        await context.SaveChangesAsync();
                    }

                    // Apply size filter if sizeId is specified
                    if (sizeId.HasValue)
                    {
                        Size sizeToAdd = new Size
                        {
                            Name = newSize,
                            ProductId = product.ProductId,
                        };
                        context.Sizes.Add(sizeToAdd);
                        await context.SaveChangesAsync();
                    }
                    ViewBag.Colors = context.Colors.ToList();
                    ViewBag.Sizes = context.Sizes.ToList();

                    if (!string.IsNullOrEmpty(newThumbnail))
                    {
                        Thumbnail ThumbnailToAdd = new Thumbnail
                        {
                            Thumbnail1 = newThumbnail
                        };
                        context.Thumbnails.Add(ThumbnailToAdd);
                        await context.SaveChangesAsync();
                    }

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
        public async Task<ActionResult> Create()
        {
            User user = await GetCurrentLoggedInUser();
            bool isLoggedIn = (user != null);
            ViewBag.IsLoggedIn = isLoggedIn;
            int? userRole = user?.Role;
            if (!isLoggedIn)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (userRole.HasValue && userRole.Value == 2)
            {
                return RedirectToAction("Index", "NotFound");
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
            int? userRole = user?.Role;
            if (!isLoggedIn)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (userRole.HasValue && userRole.Value == 2)
            {
                return RedirectToAction("Index", "NotFound");
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
            int? userRole = user?.Role;
            if (!isLoggedIn)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (userRole.HasValue && userRole.Value == 2)
            {
                return RedirectToAction("Index", "NotFound");
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create2(ProductDetail prdetail, string thumbnailName)
        {
            User user = await GetCurrentLoggedInUser();
            int flag = 0;
            bool isLoggedIn = (user != null);
            ViewBag.IsLoggedIn = isLoggedIn;
            int? userRole = user?.Role;
            if (!isLoggedIn)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (userRole.HasValue && userRole.Value == 2)
            {
                return RedirectToAction("Index", "NotFound");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    using (PRN211_BL5Context context = new PRN211_BL5Context())
                    {
                        List<Thumbnail> lsthumb = context.Thumbnails.ToList();
                        foreach (Thumbnail t in lsthumb)
                        {
                            if (t.Thumbnail1.Equals(thumbnailName))
                            {
                                prdetail.ThumbnailId = t.Id;
                                context.ProductDetails.Add(prdetail);
                                context.SaveChanges();
                                flag = 1;
                                break;
                            }
                        }

                        if (flag == 0)
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
            int? userRole = user?.Role;
            if (!isLoggedIn)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (userRole.HasValue && userRole.Value == 2)
            {
                return RedirectToAction("Index", "NotFound");
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
            int? userRole = user?.Role;
            if (!isLoggedIn)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (userRole.HasValue && userRole.Value == 2)
            {
                return RedirectToAction("Index", "NotFound");
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
                    context.SaveChanges();
                }
                return RedirectToAction("Index", "Manager");
            }

        }
        [HttpGet]
        public async Task<ActionResult> Edit2(int id)
        {
            User user = await GetCurrentLoggedInUser();
            bool isLoggedIn = (user != null);
            ViewBag.IsLoggedIn = isLoggedIn;
            int? userRole = user?.Role;
            if (!isLoggedIn)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (userRole.HasValue && userRole.Value == 2)
            {
                return RedirectToAction("Index", "NotFound");
            }
            else
            {
                using (PRN211_BL5Context context = new PRN211_BL5Context())
                {
                    var product = context.ProductDetails.FirstOrDefault(p => p.ProductDetailId == id);
                    if (product == null)
                    {
                        return NotFound();
                    }

                    var productname = context.Products.ToList();
                    ViewBag.Products = new SelectList(productname, "ProductId", "Name");

                    var colorname = context.Colors.ToList();
                    ViewBag.Colors = new SelectList(colorname, "Id", "Name");

                    var sizename = context.Sizes.ToList();
                    ViewBag.Sizes = new SelectList(sizename, "SizeId", "Name");

                   
                    return View(product);
                }
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit2(int id , ProductDetail product)
        {

            User user = await GetCurrentLoggedInUser();
            bool isLoggedIn = (user != null);
            ViewBag.IsLoggedIn = isLoggedIn;
            int? userRole = user?.Role;
            if (!isLoggedIn)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (userRole.HasValue && userRole.Value == 2)
            {
                return RedirectToAction("Index", "NotFound");
            }
            else
            {
                using (PRN211_BL5Context context = new PRN211_BL5Context())
                {
                    var existingProduct = context.ProductDetails.FirstOrDefault(p => p.ProductDetailId == id);

                    if (existingProduct == null)
                    {
                        return NotFound();
                    }
                    
                    existingProduct.ProductId = product.ProductId;
                    existingProduct.ColorId = product.ColorId;
                    existingProduct.SizeId=product.SizeId;
                    existingProduct.Image=product.Image;
                    existingProduct.Quantity=product.Quantity;
                    context.SaveChanges();
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
                return RedirectToAction("Index", "Manager");
            }
            catch
            {
                return View();
            }
        }

    }
}
