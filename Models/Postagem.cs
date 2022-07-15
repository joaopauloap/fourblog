using FourBlog.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FourBlog.Models
{
    [Table("tb_postagem")]
    public class Postagem
    {
        public Postagem()
        {
            IHttpContextAccessor accessor = new HttpContextAccessor();
            UsuarioId = accessor.HttpContext.User.Claims.First().Value;
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
        public Usuario Usuario { get; set; }
        public string UsuarioId { get; set; }
        public Tag Tag { get; set; }
        public int TagId { get; set; }
    }
}
