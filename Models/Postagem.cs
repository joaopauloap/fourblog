using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FourBlog.Models
{
    [Table("tb_postagem")]
    public class Postagem
    {
        public Postagem()
        {
            DataCriacao = DateTime.Now;
        }

        [Column("Id"), Key]
        public int PostagemId { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Texto { get; set; }

        public DateTime DataCriacao { get; set; }

        //Relacionamentos
        public int UsuarioId { get; set; }
        public int TagId { get; set; }
    }
}
