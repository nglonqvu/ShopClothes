using Microsoft.EntityFrameworkCore;

namespace OnlineShop.DataAccess.DAO
{
    public class ProductDAO
    {
        public IEnumerable<Product> GetProductList()
        {
            var products = new List<Product>();
            try
            {
                using var context = new PRN211_BL5Context();
                products = context.Products.Include(p => p.Cate).OrderBy(p => p.ProductId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return products;
        } 
    }
}
