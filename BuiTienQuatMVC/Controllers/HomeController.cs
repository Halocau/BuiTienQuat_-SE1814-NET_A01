using System.Diagnostics;
using BuiTienQuatMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BuiTienQuatMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        //public IActionResult Privacy()
        //{
        //    return View();
        //}
        public IActionResult Category()
        {
            return Redirect("/Category");
        }
        public IActionResult SystemAccount()
        {
            return Redirect("/SystemAccount");
        }
        public IActionResult NewsArticle()
        {
            return Redirect("/NewsArticle");
        }
        public IActionResult Tag()
        {
            return Redirect("/Tag"); 
        }
        public IActionResult ManageNewArticle()
        {
            return Redirect("/NewsArticle/Manager");
        }
        public IActionResult ReportStatistic()
        {
            return Redirect("/NewsArticle/ReportStatistic");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
