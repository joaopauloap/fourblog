using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FourBlog.Models
{
    [Table("tb_tag")]
    public class Tag
    {
        [Column("Id"),Key]
        public int TagId { get; set; }
        
        [Required]
        public string Nome { get; set; }

        //Relacionamentos
        public List<Postagem> Postagens { get; set; }
    }
}
