using BuiTienQuatMVC.Repositories;
using BuiTienQuatMVC.Models;
namespace BuiTienQuatMVC.Services
{
    public class TagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public IEnumerable<Tag> GetAllTags()
        {
            return _tagRepository.GetAllTags();
        }
        public Tag getTagById(int id)
        {
            return _tagRepository.getTagById(id);
        }
        public void AddTag(Tag tag)
        {
            _tagRepository.AddTag(tag);
        }
        public void UpdateTag(Tag tag)
        {
            _tagRepository.UpdateTag(tag);
        }
        public void DeleteTag(int id)
        {
            _tagRepository.DeleteTag(id);
        }
        public IEnumerable<Tag> Search(string keyword)
        {
            return _tagRepository.Search(keyword);
        }
    }
}
