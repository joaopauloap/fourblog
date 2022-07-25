using FourBlog.Areas.Identity.Data;
using FourBlog.Models;
using FourBlog.Repositories;
using FourBlog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FourBlog.Controllers
{
    [Authorize]
    public class TagController : Controller
    {
        private ITagRepository _tagRepository;
        private IPostagemRepository _postagemRepository;
        public TagController(ITagRepository tagRepository, IPostagemRepository postagemRepository)
        {
            _tagRepository = tagRepository;
            _postagemRepository = postagemRepository;
        }

        public IActionResult Index()
        {
            TagViewModel viewModel = new()
            {
                Tags = _tagRepository.Listar()
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Cadastrar(Tag tag)
        {
           _tagRepository.Cadastrar(tag);
            _tagRepository.Salvar();
            TempData["msg"] = $"Tag {tag.Nome} cadastrada com sucesso!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            Tag tag = _tagRepository.BuscarPorId(id);

            if (tag == null)
                return NotFound();

            return View(tag);
        }

        [HttpPost]
        public IActionResult Editar(Tag tag)
        {
            _tagRepository.Atualizar(tag);
            _tagRepository.Salvar();
            TempData["msg"] = $"Tag {tag.Nome} editada com sucesso!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Remover(Tag tag)
        {
            if (tag.TagId == 1) {
                TempData["erro"] = $"Esta tag não pode ser removida";
                return RedirectToAction("Index");
            }

            List<Postagem> posts = _postagemRepository.ListarPorTagId(tag.TagId);
            foreach (Postagem postagem in posts)
            {
                postagem.TagId = 1;
                _postagemRepository.Atualizar(postagem);
            }

            _tagRepository.Remover(tag);
            _tagRepository.Salvar();
            TempData["msg"] = $"Tag removida com sucesso!";
            return RedirectToAction("Index");
        }
    }
}
