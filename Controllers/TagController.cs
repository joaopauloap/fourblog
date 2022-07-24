using FourBlog.Areas.Identity.Data;
using FourBlog.Models;
using FourBlog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FourBlog.Controllers
{
    [Authorize]
    public class TagController : Controller
    {
        private FourBlogContext _context;
        public TagController(FourBlogContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            TagViewModel viewModel = new()
            {
                Tags = _context.Tags.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Cadastrar(Tag tag)
        {
            _context.Tags.Add(tag);
            _context.SaveChanges();
            TempData["msg"] = $"Tag {tag.Nome} cadastrada com sucesso!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            Tag tag = _context.Tags.Find(id);

            if (tag == null)
                return NotFound();

            return View(tag);
        }

        [HttpPost]
        public IActionResult Editar(Tag tag)
        {
            _context.Tags.Update(tag);
            _context.SaveChanges();
            TempData["msg"] = $"Tag {tag.Nome} editada com sucesso!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Remover(Tag tag)
        {
            _context.Tags.Remove(tag);
            _context.SaveChanges();
            TempData["msg"] = $"Tag removida com sucesso!";
            return RedirectToAction("Index");
        }
    }
}
