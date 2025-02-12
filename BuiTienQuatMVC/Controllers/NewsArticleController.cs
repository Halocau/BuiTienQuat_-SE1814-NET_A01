using BuiTienQuatMVC.Models;
using BuiTienQuatMVC.Services;
using BuiTienQuatMVC.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BuiTienQuatMVC.Controllers
{

    public class NewsArticleController : Controller
    {
        private readonly NewsArticleServicecs _newsArticleService;
        private readonly CategoryService _categoryService;

        public NewsArticleController(NewsArticleServicecs newsArticleService, CategoryService categoryService)
        {
            _newsArticleService = newsArticleService;
            _categoryService = categoryService;
        }


        // GET: NewsArticleController
        public ActionResult Index()
        {
            // Lấy tất cả bài viết
            var newsArticles = _newsArticleService.GetAllNewsArticles();

            // Lấy danh sách các danh mục
            var categories = _categoryService.GetAllCategories();

            // Đưa danh sách danh mục vào ViewData
            ViewData["Categories"] = categories;

            return View(newsArticles);
        }


        //search
        public IActionResult Search(string? keyword)
        {
            var result = _newsArticleService.Search(keyword);

            // Lấy danh sách danh mục để tránh null
            var categories = _categoryService.GetAllCategories();
            ViewData["Categories"] = categories;

            return View("Index", result);
        }
        // GET: NewsArticleController/Details/5
        //[AllowAnonymous] 

        public ActionResult Detail(string id)
        {
          
            if (id.IsNullOrEmpty())
            {
                return NotFound();
            }
            var newsArticle = _newsArticleService.GetNewsArticleById(id);
            if (newsArticle == null)
            {
                return NotFound();
            }
            return View(newsArticle);
        }

        // GET: NewsArticleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NewsArticleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NewsArticle newsArticle)
        {
            if (ModelState.IsValid)
            {
                _newsArticleService.AddNewsArticle(newsArticle);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(newsArticle);
            }
        }
        // GET: NewsArticleController/Edit/5
        public ActionResult Edit(string id)
        {
            if (id.IsNullOrEmpty())
            {
                return NotFound();
            }
            var newsArticleEdit = _newsArticleService.GetNewsArticleById(id);
            if (newsArticleEdit == null)
            {
                return NotFound();
            }
            return View(newsArticleEdit);
        }

        // POST: NewsArticleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, NewsArticle newsArticle)
        {
            if (id != newsArticle.NewsArticleId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _newsArticleService.UpdateNewsArticle(newsArticle);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_newsArticleService.NewsArticleExists(newsArticle.NewsArticleId))
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
            return View(newsArticle);
        }

        // GET: NewsArticleController/Delete/5
        public ActionResult Delete(int id)
        {
            _newsArticleService.DeleteNewsArticle(id.ToString());
            return RedirectToAction(nameof(Index));
        }


        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            var newsArticle = _newsArticleService.GetNewsArticleById(id);
            if (newsArticle == null)
            {
                return NotFound();
            }

            //get tag
            var tags= _newsArticleService.GetTagsByNewsArticleId(id);

            var systemAccountName = _newsArticleService.GetCreatedByName(id);

            // Get category name
            // Khai báo categoryName trước
            string categoryName = null;

            // Get category name
            short? categoryId = newsArticle.CategoryId;
            if (categoryId.HasValue)
            {
                var category = _categoryService.GetCategoryById(categoryId.Value);
                categoryName = category?.CategoryName; // Kiểm tra nếu category không null
            }

            var model = new NewsArticleDetailViewModel
            {
                NewsTitle = newsArticle.NewsTitle,
                CreatedDate = newsArticle.CreatedDate,
                NewsContent = newsArticle.NewsContent,
                NewsSource = newsArticle.NewsSource,
                CategoryName = categoryName,
                Tags = tags,
                CreatedByName = systemAccountName
            };
            return View(model);
        }

        public IActionResult SearchCategory(short? loai)
        {
            if (!loai.HasValue)
            {
                return RedirectToAction(nameof(Index));  // Nếu không có giá trị category, quay lại trang Index
            }

            var result = _newsArticleService.GetNewsArticlesByCategory(loai.Value);

            // Đưa danh sách danh mục vào ViewData để hiển thị trong giao diện
            var categories = _categoryService.GetCategoriesActive();
            ViewData["Categories"] = categories;

            return View("Index", result);  // Trả về kết quả tìm kiếm cho trang Index
        }

    }
}
