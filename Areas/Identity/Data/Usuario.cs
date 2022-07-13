using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FourBlog.Models;
using Microsoft.AspNetCore.Identity;

namespace FourBlog.Areas.Identity.Data;

// Add profile data for application users by adding properties to the Usuario class
public class Usuario : IdentityUser
{
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }

    //Relacionamentos
    public List<Postagem>? Postagens { get; set; }
    public List<Comentario>? Comentarios { get; set; }

}

