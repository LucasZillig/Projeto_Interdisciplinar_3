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
            // explicita o relacionamento das tabelas
            // relacionamento entre usuário e professor/aluno
            modelBuilder.Entity<Aluno>()
                .HasOne(u => u.Usuario)
                .WithMany(a => a.Alunos)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Professor>()
                .HasOne(u => u.Usuario)
                .WithMany(p => p.Professors)
                .OnDelete(DeleteBehavior.Cascade);

            // relacionamento entre professor e curso
            modelBuilder.Entity<Curso>()
                .HasOne(p => p.Professor)
                .WithMany(c => c.Cursos)
                .OnDelete(DeleteBehavior.Cascade);

            // relacionamento entre curso e turma
            modelBuilder.Entity<Turma>()
                .HasOne(t => t.Curso)
                .WithMany(c => c.Turmas)
                .OnDelete(DeleteBehavior.Cascade);

            // relacionamento entre aluno e turma
            modelBuilder.Entity<Turma>()
                .HasOne(t => t.Aluno)
                .WithMany(a => a.Turmas)
                .OnDelete(DeleteBehavior.Cascade);

            // relacionamento entre pergunta e turma
            modelBuilder.Entity<Pergunta>()
                .HasOne(p => p.Turma)
                .WithMany(t => t.Perguntas)
                .OnDelete(DeleteBehavior.Cascade);

            // relacionamento entre pergunta e comentario
            modelBuilder.Entity<Comentario>()
                .HasOne(c => c.Pergunta)
                .WithMany(p => p.Comentarios)
                .OnDelete(DeleteBehavior.Cascade);

            // relacionamento entre pergunta e anexo
            modelBuilder.Entity<Anexo>()
                .HasOne(a => a.Pergunta)
                .WithMany(p => p.Anexos)
                .OnDelete(DeleteBehavior.Cascade);

            // relacionamento entre arquivo e anexo
            modelBuilder.Entity<Anexo>()
                .HasOne(a => a.Arquivo)
                .WithMany(b => b.Anexos)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
