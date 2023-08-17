namespace OnlineShop.DataAccess.DAO
{
    public class CategoryDAO
    {
        public IEnumerable<Category> GetCategoryList()
        {
            var categoryList = new List<Category>();
            try
            {
                using var context = new PRN211_BL5Context();
                categoryList = context.Categories.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return categoryList;

        }

        public Category GetCategoryById(int id)
        {
            Category category;
            try
            {
                using var context = new PRN211_BL5Context();
                category = context.Categories.FirstOrDefault(c => c.CategoryId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return category;
        }
    }
}
