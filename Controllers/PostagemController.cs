using FourBlog.Areas.Identity.Data;
using FourBlog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FourBlog.ViewModels;
using FourBlog.Repositories;

namespace FourBlog.Controllers
{
    public class PostagemController : Controller
    {
        private UserManager<Usuario> _userManager;
        private ITagRepository _tagRepository;
        private IPostagemRepository _postagemRepository;
        private IComentarioRepository _comentarioRepository;

        public PostagemController(UserManager<Usuario> userManager,ITagRepository tagRepository,IPostagemRepository postagemRepository,IComentarioRepository comentarioRepository)
        {
            _userManager = userManager;
            _tagRepository = tagRepository;
            _postagemRepository = postagemRepository;
            _comentarioRepository = comentarioRepository;   
        }

        public IActionResult Index(string tag)
        {
            List<Tag> tags = _tagRepository.Listar();
            PostagemViewModel viewModel = new()
            {
                Postagens = _postagemRepository.Listar(tag),
                Tags = new SelectList(tags, "Nome", "Nome")
            };
            return View(viewModel);
        }

        [Authorize]
        public IActionResult Cadastrar()
        {
            List<Tag> tags = _tagRepository.Listar();
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
            _postagemRepository.Cadastrar(postagem);
            _postagemRepository.Salvar();
            TempData["msg"] = $"Postagem {postagem.Titulo} cadastrada com sucesso!";
            return RedirectToAction("Index");
        }

        public IActionResult Visualizar(int id)
        {
            Postagem? postagem = _postagemRepository.BuscarPorId(id);

            if (postagem == null) return NotFound();

            PostagemViewModel viewModel = new PostagemViewModel()
            {
                Postagem = postagem,
                Comentarios = _comentarioRepository.BuscarComentariosPorPostagemId(id)
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Comentar(Comentario comentario)
        {
            comentario.UsuarioId = _userManager.GetUserId(User);
            _comentarioRepository.Cadastrar(comentario);
            _comentarioRepository.Salvar();
            TempData["msg"] = "Comentário postado com sucesso!";
            return RedirectToAction("Visualizar", new { id = comentario.PostagemId });
        }

        public IActionResult ApagarComentario(int id)
        {
            Comentario comentario = _comentarioRepository.BuscarPorId(id);
            _comentarioRepository.Remover(comentario);
            _comentarioRepository.Salvar();
            TempData["msg"] = "Comentário removido com sucesso!";
            return RedirectToAction("Visualizar", new { id = comentario.PostagemId });
        }

        public IActionResult ApagarPostagem(int id)
        {
            Postagem postagem = _postagemRepository.BuscarPorId(id);
            _postagemRepository.Remover(postagem);
            _postagemRepository.Salvar();
            TempData["msg"] = "Postagem removida com sucesso!";
            return RedirectToAction("Index");
        }
    }
}
