using BuiTienQuatMVC.Models;
using BuiTienQuatMVC.Repositories;

namespace BuiTienQuatMVC.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _categoryRepository.GetCategories();
        }

        public Category GetCategoryById(short id)
        {
            return _categoryRepository.GetCategoryById(id);
        }

        public void AddCategory(Category category)
        {
            _categoryRepository.AddCategory(category);
        }

        public void UpdateCategory(Category category)
        {
            _categoryRepository.UpdateCategory(category);
        }
        public void DeleteCategory(short id)
        {
            _categoryRepository.DeleteCategory(id);
        }

        public bool CategoryExists(short id)
        {
            return _categoryRepository.CategoryExists(id);
        }
        public IEnumerable<Category> GetCategoriesActive()
        {
            return _categoryRepository.GetCategoriesActive();
        }

        internal object GetCategoryById(int value)
        {
            throw new NotImplementedException();
        }
    }
}
