using FourBlog.Areas.Identity.Data;
using FourBlog.Models;

namespace FourBlog.Repositories
{
    public class TagRepository : ITagRepository
    {
        private FourBlogContext _context;
        public TagRepository(FourBlogContext context)
        {
            _context = context;
        }

        public void Atualizar(Tag tag)
        {
            _context.Tags.Update(tag);
        }

        public Tag BuscarPorId(int id)
        {
            return _context.Tags.Find(id);
        }

        public void Cadastrar(Tag tag)
        {
            _context.Tags.Add(tag);
        }

        public List<Tag> Listar()
        {
            return _context.Tags.ToList();
        }

        public void Remover(Tag tag)
        {
            _context.Tags.Remove(tag);
        }

        public void Salvar()
        {
            _context.SaveChanges();
        }
    }
}
