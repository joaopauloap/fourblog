using FourBlog.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FourBlog.ViewModels
{
    public class PostagemViewModel
    {
        public Postagem Postagem{ get; set; }
        public List<Postagem> Postagens { get; set; }
        public SelectList Tags { get; set; }
        public Comentario Comentario { get; set; }
        public List<Comentario> Comentarios { get; set; }
    }
}
