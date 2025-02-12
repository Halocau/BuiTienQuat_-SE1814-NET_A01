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

        public ActionResult Manager()
        {
            var newsList = _newsArticleService.GetAllNewsArticles();
            return View(newsList);
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

        // GET: NewsArticleController/Detail/5
        [HttpGet]
        public JsonResult Detail(string id)
        {
            if (id.IsNullOrEmpty())
            {
                return Json(new { success = false, message = "ID is required." });
            }

            var newsArticle = _newsArticleService.GetNewsArticleById(id);
            if (newsArticle == null)
            {
                return Json(new { success = false, message = "News article not found." });
            }

            return Json(new
            {
                success = true,
                newsArticleId = newsArticle.NewsArticleId,
                newsTitle = newsArticle.NewsTitle,
                headline = newsArticle.Headline,
                newsContent = newsArticle.NewsContent,
                newsSource = newsArticle.NewsSource,
                categoryId = newsArticle.CategoryId,
                newsStatus = newsArticle.NewsStatus,
                tagIds = newsArticle.Tags // Giả sử bạn có trường TagIds trong model
            });
        }

        // GET: NewsArticleController/Create
        public ActionResult Create()
        {
            ViewBag.Categories = _categoryService.GetCategoriesActive(); // Đảm bảo không null
            return View();
        }

        // POST: NewsArticleController/Create
        // POST: NewsArticleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NewsArticle newsArticle)
        {
            short userID = (short)HttpContext.Session.GetInt32("UserId");
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(newsArticle.NewsArticleId))
                {
                    newsArticle.NewsArticleId = Guid.NewGuid().ToString();
                }
                newsArticle.UpdatedById = null;
                newsArticle.ModifiedDate = null;
                newsArticle.CreatedDate = DateTime.Now;
                newsArticle.NewsStatus = true;
                newsArticle.CreatedById = userID;
                _newsArticleService.AddNewsArticle(newsArticle);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = _categoryService.GetCategoriesActive();
            return View(newsArticle);
        }

        // GET: NewsArticleController/Edit/5
        public IActionResult Edit(string id)
        {
            var newsArticle = _newsArticleService.GetNewsArticleById(id);
            if (newsArticle == null)
            {
                return NotFound();
            }
            ViewBag.Categories = _categoryService.GetAllCategories();
            return View(newsArticle);
        }

         [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, NewsArticle newsArticle)
        {
            short userID = (short)HttpContext.Session.GetInt32("UserId");
            if (id != newsArticle.NewsArticleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                newsArticle.ModifiedDate = DateTime.Now;
                newsArticle.NewsStatus = true;
                newsArticle.UpdatedById = userID;
                _newsArticleService.UpdateNewsArticle(newsArticle);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = _categoryService.GetCategoriesActive();
            return View(newsArticle);
        }

        // GET: NewsArticleController/Delete/5
        // Xóa bài viết
        public IActionResult Delete(string id)
        {
            var newsArticle = _newsArticleService.GetNewsArticleById(id);
            if (newsArticle == null)
            {
                return NotFound();
            }

            _newsArticleService.DeleteNewsArticle(id);
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
        [HttpGet]
        public IActionResult ReportStatistic()
        {
            var news = _newsArticleService.GetAllNewsArticles();
            return View(news);
        }

        [HttpPost]
        public IActionResult GetNewsByDate(DateTime? startDate, DateTime? endDate)
        {
            var news = _newsArticleService.GetAllNewsArticles().AsQueryable();

            if (startDate.HasValue)
            {
                news = news.Where(n => n.CreatedDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                endDate = endDate.Value.Date.AddDays(1).AddTicks(-1);
                news = news.Where(n => n.CreatedDate <= endDate.Value);
            }

            var filteredNews = news.OrderByDescending(n => n.CreatedDate).ToList();
            return Json(filteredNews);
        }
    }
}
