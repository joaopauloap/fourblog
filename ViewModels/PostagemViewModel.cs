using FourBlog.Models;

namespace FourBlog.ViewModels
{
    public class PostagemViewModel
    {
        public Postagem Postagem{ get; set; }
        public Comentario Comentario { get; set; }
        public List<Comentario> Comentarios { get; set; }
    }
}
