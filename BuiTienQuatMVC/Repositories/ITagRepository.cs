using BuiTienQuatMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuiTienQuatMVC.Repositories
{
    public interface ITagRepository 
    {
        public IEnumerable<Tag> GetAllTags();
        public Tag getTagById(int id);
        public void AddTag(Tag tag);
        public void UpdateTag(Tag tag);
        public void DeleteTag(int id);

        public IEnumerable<Tag> Search(string keyword);

    }
}
