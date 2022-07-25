using FourBlog.Areas.Identity.Data;
using FourBlog.Models;
using Microsoft.EntityFrameworkCore;

namespace FourBlog.Repositories
{
    public class PostagemRepository : IPostagemRepository
    {
        private FourBlogContext _context;
        public PostagemRepository(FourBlogContext context)
        {
            _context = context;
        }

        public void Atualizar(Postagem postagem)
        {
            _context.Postagens.Update(postagem);
        }

        public Postagem BuscarPorId(int id)
        {
            return _context.Postagens.Where(p => p.PostagemId == id).Include(p => p.Usuario).Include(p => p.Tag).Include(p => p.Comentarios).ThenInclude(p => p.Usuario).FirstOrDefault();
        }

        public void Cadastrar(Postagem postagem)
        {
            _context.Postagens.Add(postagem);
        }

        public List<Postagem> Listar(string filtro)
        {
            return _context.Postagens.Include(p => p.Usuario).Include(p => p.Tag).Where(p => p.Tag.Nome.Contains(filtro) || filtro == null).OrderByDescending(p => p.DataCriacao).ToList();
        }

        public List<Postagem> ListarPorTagId(int id)
        {
            return _context.Postagens.Where(t => t.TagId == id).ToList();
        }

        public void Remover(Postagem postagem)
        {
            _context.Postagens.Remove(postagem);
        }

        public void Salvar()
        {
            _context.SaveChanges();
        }
    }
}
