using BuiTienQuatMVC.Models;
using BuiTienQuatMVC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BuiTienQuatMVC.Services
{
    public class NewsArticleServicecs
    {
        private readonly INewsArticleRepository _newsArticleRepository;

        public NewsArticleServicecs(INewsArticleRepository newsArticleRepository)
        {
            _newsArticleRepository = newsArticleRepository;
        }

        public void AddNewsArticle(NewsArticle newsArticle)
        {
            _newsArticleRepository.AddNewsArticle(newsArticle);
        }

        public void UpdateNewsArticle(NewsArticle newsArticle)
        {
            _newsArticleRepository.UpdateNewsArticle(newsArticle);
        }

        public void DeleteNewsArticle(string id)
        {
            _newsArticleRepository.DeleteNewsArticle(id);
        }

        public NewsArticle GetNewsArticleById(string id)
        {
            return _newsArticleRepository.GetNewsArticleById(id);
        }

        public IEnumerable<NewsArticle> GetAllNewsArticles()
        {
            return _newsArticleRepository.GetNewsAllArticles();
        }

        public bool NewsArticleExists(string id)
        {
            return _newsArticleRepository.NewsArticleExists(id);
        }

        public IEnumerable<NewsArticle> Search(string? keyword)
        {
            return _newsArticleRepository.SearchNewsArticles(keyword);
        }

        public IEnumerable<NewsArticle> GetNewsArticlesByCategory(short categoryId)
        {
            return _newsArticleRepository.GetNewsArticlesByCategory(categoryId);
        }

        public List<string> GetTagsByNewsArticleId(string newsArticleId)
        {
            return _newsArticleRepository.GetTagsByNewsArticleId(newsArticleId);
        }
        public string GetCreatedByName(string newsArticleId)
        {
            return _newsArticleRepository.GetCreatedByName(newsArticleId);
        }
    }
}
