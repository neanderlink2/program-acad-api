using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgramAcad.Domain.Entities;

namespace ProgramAcad.Infra.Data.Mappings
{
    public class AlgoritmoResolvidoTypeConfiguration : IEntityTypeConfiguration<AlgoritmoResolvido>
    {
        public void Configure(EntityTypeBuilder<AlgoritmoResolvido> builder)
        {
            builder.ToTable("TB_USUARIO_RESOLVE_ALGORITMO");

            builder.HasKey(x => new { x.IdAlgoritmo, x.IdUsuario });

            builder.Property(x => x.IdUsuario).HasColumnName("id_usuario");
            builder.Property(x => x.IdAlgoritmo).HasColumnName("id_algoritmo");
            builder.Property(x => x.IdLinguagem).HasColumnName("id_linguagem_usada");
            builder.Property(x => x.DataConclusao).HasColumnName("data_conclusao");

            builder.HasOne(x => x.Usuario)
                .WithMany(x => x.AlgoritmosResolvidos)
                .HasForeignKey(x => x.IdUsuario);

            builder.HasOne(x => x.Algoritmo)
                .WithMany(x => x.AlgoritmosResolvidos)
                .HasForeignKey(x => x.IdAlgoritmo);
        }
    }
}
