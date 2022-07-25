using FourBlog.Areas.Identity.Data;
using FourBlog.Models;
using Microsoft.EntityFrameworkCore;

namespace FourBlog.Repositories
{
    public class ComentarioRepository : IComentarioRepository
    {
        private FourBlogContext _context;
        public ComentarioRepository(FourBlogContext context)
        {
            _context = context;
        }

        public List<Comentario> BuscarComentariosPorPostagemId(int id)
        {
            return _context.Comentarios.Where(c => c.PostagemId == id).Include(p => p.Usuario).ToList();
        }

        public Comentario BuscarPorId(int id)
        {
            return _context.Comentarios.Find(id);
        }

        public void Cadastrar(Comentario comentario)
        {
            _context.Comentarios.Add(comentario);
        }

        public void Remover(Comentario comentario)
        {
            _context.Comentarios.Remove(comentario);
        }

        public void Salvar()
        {
            _context.SaveChanges();
        }
    }
}
