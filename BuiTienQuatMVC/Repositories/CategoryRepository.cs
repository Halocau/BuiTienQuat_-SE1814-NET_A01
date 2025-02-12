using BuiTienQuatMVC.Models;

namespace BuiTienQuatMVC.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly FunewsManagementContext _context;

        public CategoryRepository(FunewsManagementContext context)
        {
            _context = context;
        }

        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void DeleteCategory(short id)
        {
            // Kiểm tra xem CategoryId có liên kết với bảng NewsArticle hay không
            bool isCategoryLinked = _context.NewsArticles.Any(na => na.CategoryId == id);
            if (isCategoryLinked)
            {
                // Nếu có liên kết, không thực hiện xóa và có thể trả về thông báo hoặc xử lý khác
                throw new InvalidOperationException("Không thể xóa danh mục này vì nó đang được sử dụng trong bài viết.");
            }
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategoryById(short id)
        {
            return _context.Categories.FirstOrDefault(c => c.CategoryId == id);
        }

        public void UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }

        public bool CategoryExists(short id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }

        public IEnumerable<Category> GetCategoriesActive()
        {
            return _context.Categories.Where(c => c.IsActive == true).ToList();
        }
    }
}
