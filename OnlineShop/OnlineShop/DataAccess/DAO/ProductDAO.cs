﻿using Microsoft.EntityFrameworkCore;

namespace OnlineShop.DataAccess.DAO
{
    public class ProductDAO
    {
        public async Task<IEnumerable<Product>> GetProductList()
        {
            var products = new List<Product>();
            try
            {
                using (var context = new PRN211_BL5Context())
                {
                    products = await context.Products.Include(p => p.Cate).OrderByDescending(p => p.ProductId).Take(8).ToListAsync();
                    return products;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
