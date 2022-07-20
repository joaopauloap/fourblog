using FourBlog.Areas.Identity.Data;
using FourBlog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FourBlog.Areas.Identity.Data;

public class FourBlogContext : IdentityDbContext<Usuario>
{
    public DbSet<Postagem> Postagens { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Comentario> Comentarios { get; set; }

    public FourBlogContext(DbContextOptions<FourBlogContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
