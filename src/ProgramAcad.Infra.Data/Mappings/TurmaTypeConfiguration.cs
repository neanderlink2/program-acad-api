using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgramAcad.Domain.Entities;

namespace ProgramAcad.Infra.Data.Mappings
{
    public class TurmaTypeConfiguration : IEntityTypeConfiguration<Turma>
    {
        public void Configure(EntityTypeBuilder<Turma> builder)
        {
            builder.ToTable("TB_TURMA");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.IdInstrutor).HasColumnName("id_instrutor");
            builder.Property(x => x.Nome).HasColumnName("nome").HasMaxLength(75);
            builder.Property(x => x.CapacidadeAlunos).HasColumnName("capacidade_alunos");
            builder.Property(x => x.UrlImagemTurma).HasColumnName("url_imagem_turma").HasMaxLength(500);
            builder.Property(x => x.DataCriacao).HasColumnName("data_criacao");
            builder.Property(x => x.DataTermino).HasColumnName("data_termino");

            builder.HasOne(x => x.Instrutor)
                .WithMany(x => x.TurmasCriadas)
                .HasForeignKey(x => x.IdInstrutor);

            builder.HasMany(x => x.AlgoritmosDisponiveis)
                .WithOne(x => x.TurmaPertencente)
                .HasForeignKey(x => x.IdTurma);

            builder.HasMany(x => x.UsuariosInscritos)
                .WithOne(x => x.Turma)
                .HasForeignKey(x => x.IdTurma);
        }
    }
}
