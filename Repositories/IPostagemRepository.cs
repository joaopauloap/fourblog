using FourBlog.Models;

namespace FourBlog.Repositories
{
    public interface IPostagemRepository
    {
        public void Cadastrar(Postagem postagem);
        public void Atualizar(Postagem postagem);
        public void Remover(Postagem postagem);
        public Postagem BuscarPorId(int id);
        public List<Postagem> Listar(string filtro);
        public List<Postagem> ListarPorTagId(int id);
        public void Salvar();
    }
}
