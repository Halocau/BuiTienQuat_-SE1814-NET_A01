using BuiTienQuatMVC.Models;
using BuiTienQuatMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BuiTienQuatMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // Action để hiển thị danh sách danh mục
        public ActionResult Index()
        {
            var categories = _categoryService.GetAllCategories();
            //var categories = _categoryService.GetCategoriesActive();
            return View(categories);  
        }

        // Hiển thị form tạo mới danh mục
        public ActionResult Create()
        {
            // Truyền danh sách danh mục cho form tạo mới
            ViewData["Categories"] = _categoryService.GetAllCategories();
            return PartialView("_CreateCategoryForm");
        }

        // Xử lý tạo mới danh mục
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.AddCategory(category);
                var newCategoryId = category.CategoryId;
                category.ParentCategoryId = newCategoryId;

                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // Action để lấy thông tin category theo ID
        public ActionResult GetCategory(short id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Json(category);  // Trả về dữ liệu category dưới dạng JSON
        }

        public ActionResult Edit(short id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            // Truyền danh sách danh mục cho form chỉnh sửa
            ViewData["Categories"] = _categoryService.GetAllCategories();
            return PartialView("_EditCategoryForm", category);
        }

        // Xử lý cập nhật thông tin danh mục
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(short id, Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _categoryService.UpdateCategory(category);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_categoryService.CategoryExists(category.CategoryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // Xóa danh mục
        public ActionResult Delete(short id)
        {
            _categoryService.DeleteCategory(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
