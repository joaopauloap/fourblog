using FourBlog.Models;

namespace FourBlog.Repositories
{
    public interface ITagRepository
    {
        public List<Tag> Listar();
        public void Cadastrar(Tag tag);
        public void Atualizar(Tag tag);
        public void Remover(Tag tag);
        public Tag BuscarPorId(int id);
        public void Salvar();
    }
}
