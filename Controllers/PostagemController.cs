using FourBlog.Areas.Identity.Data;
using FourBlog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FourBlog.ViewModels;

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
            List<Tag> tags = _context.Tags.ToList();
            PostagemViewModel viewModel = new()
            {
                Postagens = _context.Postagens.Include(p => p.Usuario).Include(p => p.Tag).Where(p => p.Tag.Nome.Contains(tag) || tag == null).OrderByDescending(p => p.DataCriacao).ToList(),
                Tags = new SelectList(tags, "Nome", "Nome")
            };
            return View(viewModel);
        }

        [Authorize]
        public IActionResult Cadastrar()
        {
            List<Tag> tags = _context.Tags.ToList();
            PostagemViewModel viewModel = new()
            {
                Tags = new SelectList(tags, "TagId", "Nome")
            };
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Cadastrar(Postagem postagem)
        {
            postagem.UsuarioId = postagem.UsuarioId = _userManager.GetUserId(User); ;
            _context.Postagens.Add(postagem);
            _context.SaveChanges();
            TempData["msg"] = $"Postagem {postagem.Titulo} cadastrada com sucesso!";
            return RedirectToAction("Index");
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

        [Authorize]
        [HttpPost]
        public IActionResult Comentar(Comentario comentario)
        {
            comentario.UsuarioId = _userManager.GetUserId(User);
            _context.Comentarios.Add(comentario);
            _context.SaveChanges();
            TempData["msg"] = "Comentário postado com sucesso!";
            return RedirectToAction("Visualizar", new { id = comentario.PostagemId });
        }

        public IActionResult ApagarComentario(int id)
        {
            Comentario comentario = _context.Comentarios.Find(id);
            _context.Comentarios.Remove(comentario);
            _context.SaveChanges();
            TempData["msg"] = "Comentário removido com sucesso!";
            return RedirectToAction("Visualizar", new { id = comentario.PostagemId });
        }

        public IActionResult ApagarPostagem(int id)
        {
            Postagem postagem = _context.Postagens.Where(p => p.PostagemId == id).Include(p => p.Comentarios).FirstOrDefault();
            _context.Postagens.Remove(postagem);
            _context.SaveChanges();
            TempData["msg"] = "Postagem removida com sucesso!";
            return RedirectToAction("Index");
        }
    }
}
