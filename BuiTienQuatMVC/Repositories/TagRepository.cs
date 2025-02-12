using BuiTienQuatMVC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BuiTienQuatMVC.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly FunewsManagementContext _context;

        public TagRepository(FunewsManagementContext context)
        {
            _context = context;
        }

        public void AddTag(Tag tag)
        {
            int maxId = _context.Tags.Max(a => (int?)a.TagId) ?? 0;
            // Tăng giá trị ID lên 1
            tag.TagId = (int)(maxId + 1);
            if (tag == null)
                throw new ArgumentNullException(nameof(tag));

            _context.Tags.Add(tag);
            _context.SaveChanges();
        }

        public void DeleteTag(int id)
        {
            var tag = _context.Tags.Find(id);
            if (tag == null)
                throw new KeyNotFoundException($"Tag with ID {id} not found.");

            _context.Tags.Remove(tag);
            _context.SaveChanges();
        }

        public IEnumerable<Tag> GetAllTags()
        {
            return _context.Tags.ToList();
        }

        public Tag getTagById(int id)
        {
            var tag = _context.Tags.Find(id);
            if (tag == null)
                throw new KeyNotFoundException($"Tag with ID {id} not found.");

            return tag;
        }

        public IEnumerable<Tag> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return new List<Tag>();

            return _context.Tags.Where(t => t.TagName.Contains(keyword)).ToList();
        }

        public void UpdateTag(Tag tag)
        {
            if (tag == null)
                throw new ArgumentNullException(nameof(tag));

            var existingTag = _context.Tags.Find(tag.TagId);
            if (existingTag == null)
                throw new KeyNotFoundException($"Tag with ID {tag.TagId} not found.");

            existingTag.TagName = tag.TagName;
            _context.Tags.Update(existingTag);
            _context.SaveChanges();
        }
    }
}
