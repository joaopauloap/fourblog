using FourBlog.Models;

namespace ProjetoFourBlog.ViewModels
{
    public class PostagemViewModel
    {
        public Postagem Postagem{ get; set; }

        public List<Comentario> Comentarios { get; set; }
    }
}
