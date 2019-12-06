using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgramAcad.Domain.Entities;

namespace ProgramAcad.Infra.Data.Mappings
{
    public class AlgoritmoLinguagemTypeConfiguration : IEntityTypeConfiguration<AlgoritmoLinguagemDisponivel>
    {
        public void Configure(EntityTypeBuilder<AlgoritmoLinguagemDisponivel> builder)
        {
            builder.ToTable("TB_ALGORITMO_LINGUAGEM_DISPONIVEL");

            builder.HasKey(x => new { x.IdAlgoritmo, x.IdLinguagem });

            builder.Property(x => x.IdLinguagem).HasColumnName("id_linguagem_programacao");
            builder.Property(x => x.IdAlgoritmo).HasColumnName("id_algoritmo");

            builder.HasOne(x => x.Algoritmo)
                .WithMany(x => x.LinguagensPermitidas)
                .HasForeignKey(x => x.IdAlgoritmo);
        }
    }
}
