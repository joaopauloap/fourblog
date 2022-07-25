﻿using FourBlog.Areas.Identity.Data;
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

        public PostagemController(UserManager<Usuario> userManager, ITagRepository tagRepository, IPostagemRepository postagemRepository, IComentarioRepository comentarioRepository)
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
            List<Tag> tags = _tagRepository.Listar();
            PostagemViewModel viewModel = new()
            {
                Tags = new SelectList(tags, "TagId", "Nome")
            };

            if (postagem.Titulo.Length == null)
            {
                ModelState.AddModelError("Postagem.Titulo", "O titulo é obrigatório!");
                return View(viewModel);
            }

            if (postagem.Texto.Length == null)
            {
                ModelState.AddModelError("Postagem.Texto", "O texto é obrigatório");
                return View(viewModel);
            }


            if (postagem.Titulo.Length <= 2)
            {
                ModelState.AddModelError("Postagem.Titulo", "O titulo deve ter mais de 2 caracteres!");
            }

            if (postagem.Titulo.Length > 99)
            {
                ModelState.AddModelError("Postagem.Titulo", "O titulo é limitado a 100 caracteres!");
            }

            if (postagem.Texto.Length <= 2)
            {
                ModelState.AddModelError("Postagem.Texto", "O texto deve ter mais de 2 caracteres!");
            }

            if (ModelState.IsValid)
            {
                postagem.UsuarioId = postagem.UsuarioId = _userManager.GetUserId(User);
                postagem.DataCriacao = DateTime.Now;
                _postagemRepository.Cadastrar(postagem);
                _postagemRepository.Salvar();
                TempData["msg"] = $"Postagem '{postagem.Titulo}' cadastrada com sucesso!";
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        public IActionResult Visualizar(int id)
        {
            Postagem? postagem = _postagemRepository.BuscarPorId(id);

            if (postagem == null) return NotFound();

            return View(postagem);
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

        public IActionResult Remover(int id)
        {
            Postagem postagem = _postagemRepository.BuscarPorId(id);
            TempData["msg"] = $"Postagem '{postagem.Titulo}' removida com sucesso!";
            _postagemRepository.Remover(postagem);
            _postagemRepository.Salvar();
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Editar(int id)
        {
            List<Tag> tags = _tagRepository.Listar();
            PostagemViewModel viewModel = new()
            {
                Postagem = _postagemRepository.BuscarPorId(id),
                Tags = new SelectList(tags, "TagId", "Nome")
            };
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Editar(Postagem postagem)
        {
            List<Tag> tags = _tagRepository.Listar();
            PostagemViewModel viewModel = new()
            {
                Tags = new SelectList(tags, "TagId", "Nome")
            };

            if (postagem.Titulo.Length == null)
            {
                ModelState.AddModelError("Postagem.Titulo", "O titulo é obrigatório!");
                return View(viewModel);
            }

            if (postagem.Texto.Length == null)
            {
                ModelState.AddModelError("Postagem.Texto", "O texto é obrigatório");
                return View(viewModel);
            }

            if (postagem.Titulo.Length <= 2)
            {
                ModelState.AddModelError("Titulo", "O titulo deve ter mais de 2 caracteres!");
            }

            if (postagem.Titulo.Length > 99)
            {
                ModelState.AddModelError("Titulo", "O titulo é limitado a 100 caracteres!");
            }

            if (postagem.Texto.Length <= 2)
            {
                ModelState.AddModelError("Texto", "O texto deve ter mais de 2 caracteres!");
            }

            if (ModelState.IsValid)
            {
                postagem.Texto += $"\r\n (Editado às {DateTime.Now})";
                _postagemRepository.Atualizar(postagem);
                _postagemRepository.Salvar();
                TempData["msg"] = $"Postagem '{postagem.Titulo}' editada com sucesso!";
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

    }
}
