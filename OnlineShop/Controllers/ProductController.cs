using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using System.Dynamic;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly PRN211_BL5Context db;

        public ProductController(PRN211_BL5Context _db)
        {
            db = _db;
        }

        public IActionResult ProductDetail()
        {
            int productId = 5;
            try
            {   
                var product = db.Products.FirstOrDefault(p => p.ProductId == productId);
                var firstColorId = db.ProductDetails.FirstOrDefault()?.ColorId;
                var productDetail = db.ProductDetails.FirstOrDefault(p => p.ProductId == productId && p.ColorId == firstColorId);               
                if (productDetail == null)
                {
                    return Redirect("Index");
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
                    dynamic viewModel = new ExpandoObject();
                    viewModel.ProductDetail = productDetail;
                    viewModel.Colors = colors;
                    viewModel.Sizes = sizes;
                    viewModel.Product = product;
                    viewModel.Thumbnails = thumbnails; 

                    return View(viewModel);
                }
            }
            catch
            {
                return RedirectToAction("Index", "Home");
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
                    ThumbnailImageUrl = newThumbnailImageUrl
                };
                return Json(result);
            }

            return Json(null);
        }
    }
}
