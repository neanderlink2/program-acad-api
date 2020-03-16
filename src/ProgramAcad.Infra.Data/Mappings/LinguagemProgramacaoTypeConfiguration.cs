using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgramAcad.Domain.Entities;

namespace ProgramAcad.Infra.Data.Mappings
{
    public class LinguagemProgramacaoTypeConfiguration : IEntityTypeConfiguration<LinguagemProgramacao>
    {
        public void Configure(EntityTypeBuilder<LinguagemProgramacao> builder)
        {
            builder.ToTable("TB_LINGUAGEM_PROGRAMACAO");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Name).HasColumnName("nome").HasMaxLength(20);
            builder.Property(x => x.Descricao).HasColumnName("descricao").HasMaxLength(200);
            builder.Property(x => x.NumCompilador).HasColumnName("num_tipo_compilador");
            builder.Property(x => x.ApiIdentifier).HasColumnName("api_id");

            builder.HasMany(x => x.ExecucoesDessaLinguagem)
                .WithOne(x => x.LinguagemProgramacao)
                .HasForeignKey(x => x.IdLinguagem);

            builder.HasMany(x => x.AlgoritmosDessaLinguagem)
                .WithOne(x => x.LinguagemProgramacao)
                .HasForeignKey(x => x.IdLinguagem);

            builder.HasMany(x => x.AlgoritmosResolvidosDessaLinguagem)
                .WithOne(x => x.LinguagemProgramacao)
                .HasForeignKey(x => x.IdLinguagem);
        }
    }
}
