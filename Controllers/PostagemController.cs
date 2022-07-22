using FourBlog.Areas.Identity.Data;
using FourBlog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoFourBlog.ViewModels;

namespace FourBlog.Controllers
{
    public class PostagemController : Controller
    {
        private FourBlogContext _context;
        private UserManager<Usuario> _userManager;
        public PostagemController(FourBlogContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index(string tag)
        {
            var Postagens = _context.Postagens.Include(p => p.Usuario).Include(p => p.Tag).Where(p => p.Tag.Nome.Contains(tag) || tag == null).OrderByDescending(p => p.DataCriacao).ToList();
            List<Tag> ListaTags = _context.Tags.ToList();
            ViewBag.Tags = new SelectList(ListaTags, "Nome", "Nome");
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
            Postagem? postagem = _context.Postagens.Where(p => p.PostagemId == id)
                .Include(p => p.Usuario)
                .Include(p => p.Tag)
                .FirstOrDefault();

            if (postagem == null) return NotFound();

            PostagemViewModel viewModel = new PostagemViewModel()
            {
                Postagem = postagem,
                Comentarios = _context.Comentarios.Where(c => c.PostagemId == id).Include(p => p.Usuario).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Cadastrar(Postagem postagem)
        {
            postagem.UsuarioId = postagem.UsuarioId = _userManager.GetUserId(User); ;
            _context.Postagens.Add(postagem);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Comentar(Comentario comentario)
        {
            comentario.UsuarioId = _userManager.GetUserId(User);
            _context.Comentarios.Add(comentario);
            _context.SaveChanges();
            return RedirectToAction("Visualizar", new { id = comentario.PostagemId });
        }
    }
}

//TODO Adicionar Telas Tag