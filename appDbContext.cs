using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;
using PI_3.Models;

namespace PI_3
{
  public class appDbContext : DbContext
  {
    public DbSet<Aluno> Aluno { get; set; }
    public DbSet<Arquivo> Arquivo { get; set; }
    public DbSet<Comentario> MyProperty { get; set; }
    public DbSet<Curso> Curso { get; set; }
    public DbSet<Pergunta> Pergunta { get; set; }
    public DbSet<Professor> Professor { get; set; }
    public DbSet<Turma> Turma { get; set; }
    public DbSet<Usuario> Usuario { get; set; }

    public appDbContext(){

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseMySQL("server=localhost;database=projeto;user=root;password=root");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

    //   modelBuilder.Entity<Publisher>(entity =>
    //   {
    //     entity.HasKey(e => e.ID);
    //     entity.Property(e => e.Name).IsRequired();
    //   });

    //   modelBuilder.Entity<Book>(entity =>
    //   {
    //     entity.HasKey(e => e.ISBN);
    //     entity.Property(e => e.Title).IsRequired();
    //     entity.HasOne(d => d.Publisher)
    //       .WithMany(p => p.Books);
    //   });
    }
  }
}