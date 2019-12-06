using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgramAcad.Domain.Entities;

namespace ProgramAcad.Infra.Data.Mappings
{
    public class AlgoritmoTypeConfiguration : IEntityTypeConfiguration<Algoritmo>
    {
        public void Configure(EntityTypeBuilder<Algoritmo> builder)
        {
            builder.ToTable("TB_ALGORITMO");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.IdTurma).HasColumnName("id_turma");
            builder.Property(x => x.Titulo).HasColumnName("titulo").HasMaxLength(100);
            builder.Property(x => x.HtmlDescricao).HasColumnName("html_descricao");
            builder.Property(x => x.DataCriacao).HasColumnName("data_criacao");
            builder.Property(x => x.IdNivelDificuldade).HasColumnName("id_nivel_dificuldade");

            builder.HasOne(x => x.NivelDificuldade)
                .WithMany(x => x.AlgoritmosDesseNivel)
                .HasForeignKey(x => x.IdNivelDificuldade);

            builder.HasOne(x => x.TurmaPertencente)
                .WithMany(x => x.AlgoritmosDisponiveis)
                .HasForeignKey(x => x.IdTurma);

            builder.HasMany(x => x.CasosDeTeste)
                .WithOne(x => x.AlgoritmoVinculado)
                .HasForeignKey(x => x.IdAlgoritmo);

            builder.HasMany(x => x.LinguagensPermitidas)
                .WithOne(x => x.Algoritmo)
                .HasForeignKey(x => x.IdAlgoritmo);

            builder.HasMany(x => x.AlgoritmosResolvidos)
                .WithOne(x => x.Algoritmo)
                .HasForeignKey(x => x.IdAlgoritmo);
        }
    }
}
