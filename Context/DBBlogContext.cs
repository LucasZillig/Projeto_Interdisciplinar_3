using System;
using Microsoft.EntityFrameworkCore;
using MVC_with_EF.Context.Models;

namespace MVC_with_EF.Context
{
    public class DBBlogContext : DbContext
    {
        public DBBlogContext(DbContextOptions<DBBlogContext> options)
            : base(options) { }

        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<Professor> Professor { get; set; }

        public DbSet<Aluno> Aluno { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //explicita o relacionamento das tabelass
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Professor)
                .WithMany(p => p.Usuarios)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Aluno)
                .WithMany(a => a.Usuarios)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
