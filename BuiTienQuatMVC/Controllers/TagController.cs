using BuiTienQuatMVC.Models;
using BuiTienQuatMVC.Repositories;
using BuiTienQuatMVC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuiTienQuatMVC.Controllers
{
    public class TagController : Controller
    {
        // GET: TagController
        private readonly TagService _tagService;

        public TagController(TagService tagService)
        {
            _tagService = tagService;
        }

        public IActionResult Index()
        {
            var tags = _tagService.GetAllTags();
            return View(tags);
        }
        [HttpGet]
        public IActionResult GetAllTags()
        {
            var tags = _tagService.GetAllTags();
            return Json(tags);
        }

        [HttpPost]
        public IActionResult Create(Tag tag)
        {
            if (ModelState.IsValid)
            {
                _tagService.AddTag(tag);
                return RedirectToAction("Index");
            }
            return View(tag);
        }

        [HttpPost]
        public IActionResult Edit(int id, [FromForm] Tag updatedTag)
        {
            var existingTag = _tagService.getTagById(id);
            if (existingTag == null)
            {
                return Json(new { success = false, message = "Tag not found" });
            }

            if (ModelState.IsValid)
            {
                existingTag.TagName = updatedTag.TagName;
                existingTag.Note = updatedTag.Note;
                _tagService.UpdateTag(existingTag);
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Invalid data" });
        }
        [HttpGet]
        public IActionResult GetTagById(int id)
        {
            var tag = _tagService.getTagById(id);
            if (tag == null)
            {
                return NotFound();
            }
            return Json(tag);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                _tagService.DeleteTag(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
