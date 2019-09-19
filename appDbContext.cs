using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;
using MySql.Data.EntityFrameworkCore;
using PI_3.Models;

namespace PI_3
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

        public DbSet<Aluno> Aluno { get; set; }
        public DbSet<Arquivo> Arquivo { get; set; }
        public DbSet<Comentario> Comentario { get; set; }
        public DbSet<Curso> Curso { get; set; }
        public DbSet<Pergunta> Pergunta { get; set; }
        public DbSet<Professor> Professor { get; set; }
        public DbSet<CursoAluno> CursoAluno { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;port=3306;database=nossoDB;user=root;password=root");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuario");
                entity.HasKey(e => e.UsuarioId);
                //entity.Property(e => e.Id).HasColumnName("id_usuario").ValueGeneratedNever().IsRequired(); // SEM AUTO INCREMENTO
                entity.Property(e => e.UsuarioId).HasColumnName("id_usuario").ValueGeneratedOnAdd().IsRequired(); // COM AUTO INCREMENTO
                entity.Property(e => e.UsuarioNome).HasColumnName("nome_usuario").HasColumnType("VARCHAR(100)").IsRequired();
                entity.Property(e => e.UsuarioSobrenome).HasColumnName("sobrenome_usuario").HasColumnType("VARCHAR(100)").IsRequired();
                entity.Property(e => e.UsuarioEmail).HasColumnName("email_usuario").HasColumnType("VARCHAR(100)");
                entity.Property(e => e.UsuarioSenha).HasColumnName("senha_usuario").HasColumnType("VARCHAR(25)").IsRequired();

            });

            modelBuilder.Entity<Aluno>(entity =>
            {
                entity.ToTable("aluno");
                entity.HasKey(e => e.AlunoId);
                entity.Property(e => e.AlunoId).HasColumnName("id_aluno").ValueGeneratedOnAdd().IsRequired(); // COM AUTO INCREMENTO
                entity.Property(e => e.UsuarioId).HasColumnName("id_usuario").IsRequired();
                entity.HasOne(a => a.Usuario).WithOne(b => b.Aluno).HasForeignKey<Aluno>(b => b.UsuarioId);
            });

            modelBuilder.Entity<Professor>(entity =>
            {
                entity.ToTable("professor");
                entity.HasKey(e => e.ProfessorId);
                entity.Property(e => e.ProfessorId).HasColumnName("id_professor").ValueGeneratedOnAdd().IsRequired();
                entity.Property(e => e.UsuarioId).HasColumnName("id_usuario").IsRequired();
                entity.HasOne(a => a.Usuario).WithOne(b => b.Professor).HasForeignKey<Professor>(b => b.UsuarioId);
            });

            modelBuilder.Entity<Curso>(entity =>
            {
                entity.ToTable("curso");
                entity.HasKey(e => e.CursoId);
                entity.Property(e => e.CursoId).HasColumnName("id_curso").ValueGeneratedOnAdd().IsRequired();
                entity.Property(e => e.CursoNome).HasColumnName("nome_curso").HasColumnType("VARCHAR(45)").IsRequired();
                entity.Property(e => e.CursoDesc).HasColumnName("desc_curso").HasColumnType("VARCHAR(300)");
                entity.Property(e => e.ProfessorId).HasColumnName("id_professor").IsRequired();
                entity.HasOne(a => a.Professor).WithMany(b => b.Cursos).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<CursoAluno>(entity =>
            {
                entity.ToTable("curso_aluno");
                entity.HasKey(e => e.CursoAlunoId);
                entity.Property(e => e.CursoAlunoId).HasColumnName("id_cursoAluno").ValueGeneratedOnAdd().IsRequired();
                entity.Property(e => e.AlunoId).HasColumnName("id_aluno").IsRequired();
                entity.Property(e => e.CursoId).HasColumnName("id_curso").IsRequired();
                entity.Property(e => e.statusInvite).HasColumnName("statusinvite_cursoAluno");
                entity.HasOne(a => a.Aluno).WithMany(b => b.CursoAluno).HasForeignKey(at => at.AlunoId);
                entity.HasOne(a => a.Curso).WithMany(b => b.CursoAluno).HasForeignKey(at => at.CursoId); 

            });

            modelBuilder.Entity<Pergunta>(entity => {

                entity.ToTable("pergunta");
                entity.HasKey(e => e.PerguntaId);
                entity.Property(e => e.PerguntaId).HasColumnName("id_pergunta").ValueGeneratedOnAdd().IsRequired();
                entity.Property(e => e.PerguntaNome).HasColumnName("nome_pergunta").HasColumnType("VARCHAR(45)").IsRequired();
                entity.Property(e => e.PerguntaDesc).HasColumnName("desc_pergunta").HasColumnType("VARCHAR(45)");
                entity.Property(e => e.PerguntaData).HasColumnName("data_pergunta").HasColumnType("Date").IsRequired();
                entity.Property(e => e.Arquivado).HasColumnName("arquivado_pergunta").HasColumnType("int").IsRequired();
                entity.Property(e => e.CursoAlunoId).HasColumnName("id_turma").IsRequired();
                entity.HasOne(a => a.CursoAluno).WithMany(b => b.Perguntas);
            });

            modelBuilder.Entity<Comentario>(entity =>
            {
                entity.ToTable("comentario");
                entity.HasKey(e => e.ComentarioId);
                entity.Property(e => e.ComentarioId).HasColumnName("id_comentario").ValueGeneratedOnAdd().IsRequired();
                entity.Property(e => e.ComentarioConteudo).HasColumnName("conteudo_comentario").HasColumnType("VARCHAR(100)").IsRequired();
                entity.Property(e => e.ComentarioData).HasColumnName("data_comentario").HasColumnType("VARCHAR(45)").IsRequired();
                entity.Property(e => e.PerguntaId).HasColumnName("id_pergunta").IsRequired();
                entity.HasOne(a => a.Pergunta).WithMany(b => b.Comentarios).OnDelete(DeleteBehavior.Cascade);
            }); 

            modelBuilder.Entity<Arquivo>(entity =>
            {
                entity.ToTable("arquivo");
                entity.HasKey(e => e.ArquivoId);
                entity.Property(e => e.ArquivoId).HasColumnName("id_arquivo").ValueGeneratedOnAdd().IsRequired();
                entity.Property(e => e.ArquivoTipo).HasColumnName("tipo_arquivo").HasColumnType("INT").IsRequired();
                entity.Property(e => e.ArquivoUrl).HasColumnName("url_arquivo").HasColumnType("VARCHAR(20)").IsRequired();
            }); 

            modelBuilder.Entity<PerguntaArquivo>(entity =>
            {
                entity.ToTable("pergunta_arquivo");
                entity.HasKey(ab => new { ab.ArquivoId, ab.PerguntaId }); 
                entity.Property(e => e.ArquivoId).HasColumnName("id_arquivo").IsRequired();
                entity.Property(e => e.PerguntaId).HasColumnName("id_pergunta").IsRequired();
                entity.HasOne(a => a.Pergunta).WithMany(b => b.ArquivosPergunta).HasForeignKey(ab => ab.PerguntaId); 
                entity.HasOne(a => a.Arquivo).WithMany(b => b.ArquivosPergunta).HasForeignKey(ab => ab.ArquivoId);
            }); 
        }
    }
}