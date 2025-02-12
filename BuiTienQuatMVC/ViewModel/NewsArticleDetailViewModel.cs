
namespace BuiTienQuatMVC.ViewModel
{
    public class NewsArticleDetailViewModel
    {
        public string NewsTitle { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string NewsContent { get; set; }
        public string NewsSource { get; set; }
        public string CategoryName { get; set; }
        public string CreatedByName { get; set; }
        public List<string> Tags { get; set; }  // Danh sách tên tag
    }
}
