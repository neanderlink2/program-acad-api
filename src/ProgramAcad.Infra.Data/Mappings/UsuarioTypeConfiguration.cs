using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgramAcad.Domain.Entities;

namespace ProgramAcad.Infra.Data.Mappings
{
    public class UsuarioTypeConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("TB_USUARIO");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.NomeCompleto).HasColumnName("nome_completo").HasMaxLength(256);
            builder.Property(x => x.Nickname).HasColumnName("nickname").HasMaxLength(32);
            builder.Property(x => x.Cpf).HasColumnName("cpf").HasMaxLength(15);
            builder.Property(x => x.Cep).HasColumnName("cep").HasMaxLength(12).IsRequired(false);
            builder.Property(x => x.Email).HasColumnName("email").HasMaxLength(256);
            builder.Property(x => x.Sexo).HasColumnName("sexo").HasMaxLength(1);
            builder.Property(x => x.Role).HasColumnName("role").HasMaxLength(50).HasDefaultValue("ESTUDANTE");

            builder.HasMany(x => x.AlgoritmosResolvidos)
                .WithOne(x => x.Usuario)
                .HasForeignKey(x => x.IdUsuario);

            builder.HasMany(x => x.TestesExecutados)
                .WithOne(x => x.UsuarioExecutou)
                .HasForeignKey(x => x.IdUsuario);

            builder.HasMany(x => x.TurmasCriadas)
                .WithOne(x => x.Instrutor)
                .HasForeignKey(x => x.IdInstrutor);

            builder.HasMany(x => x.TurmasInscritas)
                .WithOne(x => x.Estudante)
                .HasForeignKey(x => x.IdUsuario);
        }
    }
}