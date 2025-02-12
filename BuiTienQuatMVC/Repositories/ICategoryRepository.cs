using BuiTienQuatMVC.Models;

namespace BuiTienQuatMVC.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories();
        Category GetCategoryById(short id);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(short id);
        public bool CategoryExists(short id);

        //show all categories active
        IEnumerable<Category> GetCategoriesActive();
    }
}
