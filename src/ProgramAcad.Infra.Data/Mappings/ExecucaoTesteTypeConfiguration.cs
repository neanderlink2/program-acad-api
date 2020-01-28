using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgramAcad.Domain.Entities;

namespace ProgramAcad.Infra.Data.Mappings
{
    public class ExecucaoTesteTypeConfiguration : IEntityTypeConfiguration<ExecucaoTeste>
    {
        public void Configure(EntityTypeBuilder<ExecucaoTeste> builder)
        {
            builder.ToTable("TB_TESTE_PRIMEIRA_EXECUCAO");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.IdCasoTeste).HasColumnName("id_caso_teste");
            builder.Property(x => x.IdUsuario).HasColumnName("id_usuario");
            builder.Property(x => x.IdLinguagem).HasColumnName("id_linguagem_programacao");
            builder.Property(x => x.Sucesso).HasColumnName("sucesso");
            builder.Property(x => x.TempoExecucao).HasColumnName("tempo_execucao");

            builder.HasOne(x => x.CasoTeste)
                .WithMany(x => x.ExecucoesTeste)
                .HasForeignKey(x => x.IdCasoTeste);

            builder.HasOne(x => x.UsuarioExecutou)
                .WithMany(x => x.TestesExecutados)
                .HasForeignKey(x => x.IdUsuario);
        }
    }
}
