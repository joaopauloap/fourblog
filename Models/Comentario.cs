using FourBlog.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FourBlog.Models
{
    [Table("tb_comentario")]
    public class Comentario
    {
        public Comentario()
        {
            DataHora = DateTime.Now;
        }

        [Column("Id"), Key]
        public int ComentarioId { get; set; }

        [Required]
        public string Texto { get; set; }

        public DateTime DataHora { get; set; }

        //Relacionamentos

        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public int PostagemId { get; set; }
        public Postagem Postagem { get; set; }

    }
}
