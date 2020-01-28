using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using ProgramAcad.Domain.Entities;
using System.Collections.Generic;

namespace ProgramAcad.Infra.Data.Mappings
{
    public class CasoTesteTypeConfiguration : IEntityTypeConfiguration<CasoTeste>
    {
        public void Configure(EntityTypeBuilder<CasoTeste> builder)
        {
            builder.ToTable("TB_CASO_TESTE");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.EntradaEsperada)
                .HasColumnName("entrada_esperada")
                .HasConversion(
                    c => JsonConvert.SerializeObject(c),
                    c => JsonConvert.DeserializeObject<IEnumerable<string>>(c)
                );
            builder.Property(x => x.SaidaEsperada).HasColumnName("saida_esperada")
                .HasConversion(
                    c => JsonConvert.SerializeObject(c),
                    c => JsonConvert.DeserializeObject<IEnumerable<string>>(c)
                );
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
