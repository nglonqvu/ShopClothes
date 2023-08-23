using Microsoft.EntityFrameworkCore;

namespace OnlineShop.DataAccess.DAO
{
    public class CategoryDAO
    {
        public async Task<IEnumerable<Category>> GetCategoryList()
        {
            var categoryList = new List<Category>();
            try
            {
                using (var context = new PRN211_BL5Context())
                {
                    categoryList = await context.Categories.ToListAsync();
                    return categoryList;
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Category> GetCategoryById(int id)
        {
            Category category;
            try
            {
                using (var context = new PRN211_BL5Context())
                {
                    category = await context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
                    return category;
                } 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
    }
}
