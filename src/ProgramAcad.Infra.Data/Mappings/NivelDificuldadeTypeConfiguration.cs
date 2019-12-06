using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgramAcad.Domain.Entities;

namespace ProgramAcad.Infra.Data.Mappings
{
    public class NivelDificuldadeTypeConfiguration : IEntityTypeConfiguration<NivelDificuldade>
    {
        public void Configure(EntityTypeBuilder<NivelDificuldade> builder)
        {
            builder.ToTable("TB_NIVEL_DIFICULDADE");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Nivel).HasColumnName("nivel");
            builder.Property(x => x.Descricao).HasColumnName("descricao").HasMaxLength(50);
            builder.Property(x => x.PontosReceber).HasColumnName("pontos_a_receber");

            builder.HasMany(x => x.AlgoritmosDesseNivel)
                .WithOne(x => x.NivelDificuldade)
                .HasForeignKey(x => x.IdNivelDificuldade);
        }
    }
}
