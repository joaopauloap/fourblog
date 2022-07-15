using FourBlog.Areas.Identity.Data;
using FourBlog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FourBlog.Controllers
{
    public class PostagemController : Controller
    {
        private FourBlogContext _context;
        public PostagemController(FourBlogContext context)
        {
            _context = context;
        }
    
        public IActionResult Index()
        {
            var Postagens = _context.Postagens.Include(p=>p.Usuario).Include(p=>p.Tag).ToList();
            return View(Postagens);
        }

        [Authorize]
        public IActionResult Cadastrar()
        {
            List<Tag> tags = _context.Tags.ToList();
            ViewBag.Tags = new SelectList(tags, "TagId", "Nome");
            return View();
        }

        public IActionResult Visualizar(int id)
        {
            Postagem postagem = _context.Postagens.Where(p=>p.PostagemId == id).Include(p=>p.Usuario).Include(p=>p.Tag).FirstOrDefault();
            return View(postagem);
        }

        [HttpPost]
        public IActionResult Cadastrar(Postagem postagem)
        {

            _context.Postagens.Add(postagem);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
