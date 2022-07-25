using FourBlog.Models;

namespace FourBlog.Repositories
{
    public interface IComentarioRepository
    {
        public List<Comentario> BuscarComentariosPorPostagemId(int id);
        public Comentario BuscarPorId(int id);
        public void Cadastrar(Comentario comentario);
        public void Remover(Comentario comentario);
        public void Salvar();
    }
}
