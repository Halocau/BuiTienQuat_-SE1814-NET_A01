using BuiTienQuatMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace BuiTienQuatMVC.Repositories
{
    public class NewsArticleRepository : INewsArticleRepository
    {
        private readonly FunewsManagementContext _context;

        public NewsArticleRepository(FunewsManagementContext context)
        {
            _context = context;
        }

        public void AddNewsArticle(NewsArticle newsArticle)
        {
            _context.NewsArticles.Add(newsArticle);
            _context.SaveChanges();
        }

        public void DeleteNewsArticle(string id)
        {
            var newsArticle = _context.NewsArticles.Find(id);
            if (newsArticle != null)
            {
                _context.NewsArticles.Remove(newsArticle);
                _context.SaveChanges();
            }
        }

        public NewsArticle GetNewsArticleById(string id)
        {
            return _context.NewsArticles.FirstOrDefault(n => n.NewsArticleId == id);
        }


        public bool NewsArticleExists(string id)
        {
            return _context.NewsArticles.Any(e => e.NewsArticleId == id);
        }

        public void UpdateNewsArticle(NewsArticle newsArticle)
        {
            _context.NewsArticles.Update(newsArticle);
            _context.SaveChanges();
        }

        public IEnumerable<NewsArticle> GetNewsAllArticles()
        {
            return _context.NewsArticles.ToList();
        }

        public IEnumerable<NewsArticle> SearchNewsArticles(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return _context.NewsArticles.ToList();
            }

            keyword = keyword.ToLower(); // Chuyển keyword về chữ thường
            return _context.NewsArticles
                .Where(a => a.Headline.ToLower().Contains(keyword) || a.NewsSource.ToLower().Contains(keyword))
                .ToList();
        }


        public IEnumerable<NewsArticle> GetNewsArticlesByCategory(short categoryId)
        {
            return _context.NewsArticles.Where(na => na.CategoryId == categoryId).ToList();
        }

        public List<string> GetTagsByNewsArticleId(string newsArticleId)
        {

            var article = _context.NewsArticles
                .Where(n => n.NewsArticleId == newsArticleId)
                .Select(n => n.Tags.Select(t => t.TagName).ToList()) // Lấy danh sách tên tag
                .FirstOrDefault();

            return article ?? new List<string>(); // Trả về danh sách rỗng nếu không có tag
        }

        public void main()
        {
            var tags = GetTagsByNewsArticleId("1");
            foreach (var tag in tags)
            {
                System.Console.WriteLine(tag);
            }
        }

        public string GetCreatedByName(string newsArticleId)
        {
            var newsArticle = _context.NewsArticles
                .Include(na => na.CreatedBy) // Bao gồm thông tin về người tạo
                .FirstOrDefault(na => na.NewsArticleId == newsArticleId);
            // Nếu bài viết tồn tại và có thông tin người tạo, trả về tên người tạo
            if (newsArticle != null && newsArticle.CreatedBy != null)
            {
                return newsArticle.CreatedBy.AccountName;
            }

            // Trả về "Unknown" nếu không tìm thấy hoặc không có thông tin người tạo
            return "Unknown";
        }
    }
}
