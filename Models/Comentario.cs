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
    }
}
