using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgramAcad.Domain.Entities;

namespace ProgramAcad.Infra.Data.Mappings
{
    public class TurmaUsuarioTypeConfiguration : IEntityTypeConfiguration<TurmaUsuario>
    {
        public void Configure(EntityTypeBuilder<TurmaUsuario> builder)
        {
            builder.ToTable("TB_USUARIO_PERTENCE_TURMA");

            builder.HasKey(x => new { x.IdUsuario, x.IdTurma });

            builder.Property(x => x.IdTurma).HasColumnName("id_turma");
            builder.Property(x => x.IdUsuario).HasColumnName("id_usuario");
            builder.Property(x => x.PontosUsuario).HasColumnName("pontos_usuario");
            builder.Property(x => x.DataIngresso).HasColumnName("data_ingresso");
            builder.Property(x => x.Aceito).HasColumnName("aceito");

            builder.HasOne(x => x.Turma)
                .WithMany(x => x.UsuariosInscritos)
                .HasForeignKey(x => x.IdTurma);

            builder.HasOne(x => x.Estudante)
                .WithMany(x => x.TurmasInscritas)
                .HasForeignKey(x => x.IdUsuario)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
