using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgramAcad.Domain.Entities;

namespace ProgramAcad.Infra.Data.Mappings
{
    public class CasoTesteTypeConfiguration : IEntityTypeConfiguration<CasoTeste>
    {
        public void Configure(EntityTypeBuilder<CasoTeste> builder)
        {
            builder.ToTable("TB_CASO_TESTE");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.EntradaEsperada).HasColumnName("entrada_esperada").HasMaxLength(100);
            builder.Property(x => x.SaidaEsperada).HasColumnName("saida_esperada").HasMaxLength(100);
            builder.Property(x => x.TempoMaximoDeExecucao).HasColumnName("tempo_maximo_execucao");
            builder.Property(x => x.IdAlgoritmo).HasColumnName("id_algoritmo");

            builder.HasOne(x => x.AlgoritmoVinculado)
                .WithMany(x => x.CasosDeTeste)
                .HasForeignKey(x => x.IdAlgoritmo);

            builder.HasMany(x => x.ExecucoesTeste)
                .WithOne(x => x.CasoTeste)
                .HasForeignKey(x => x.IdCasoTeste);
        }
    }
}
