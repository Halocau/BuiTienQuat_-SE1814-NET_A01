using BuiTienQuatMVC.Models;

namespace BuiTienQuatMVC.Repositories
{
    public interface INewsArticleRepository
    {
        IEnumerable<NewsArticle> GetNewsAllArticles();
        NewsArticle GetNewsArticleById(string id);
        void AddNewsArticle(NewsArticle newsArticle);
        void UpdateNewsArticle(NewsArticle newsArticle);
        void DeleteNewsArticle(string id);
        public bool NewsArticleExists(string id);

        IEnumerable<NewsArticle> SearchNewsArticles(string keyword);

        public IEnumerable<NewsArticle> GetNewsArticlesByCategory(short categoryId);

        //truy xuất tag theo id bài viết
        public List<string> GetTagsByNewsArticleId(string newsArticleId);
        public string GetCreatedByName(string newsArticleId);


    }
}
