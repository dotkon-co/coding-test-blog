using Blog.Domain.Entidades.ControleAcesso;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infra.Data.Mapeamentos.ControleAcesso
{
    public class UsuarioMapeamento : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable(nameof(Usuario));

            builder.Property(x => x.UserName)
                .IsRequired()
                .HasColumnName("NomeUsuario");

            builder.Property(x => x.Nome)
                .HasColumnType("varchar(250)")
                .IsRequired();

            builder.Property(x => x.NormalizedUserName)
                .HasColumnName("NomeUsuarioNormalizado");

            builder.Property(x => x.NormalizedEmail)
                .HasColumnName("EmailNormalizado");

            builder.Property(x => x.EmailConfirmed)
                .HasColumnName("EmailConfirmado");

            builder.Property(x => x.PasswordHash)
                .IsRequired()
                .HasColumnName("SenhaHash");

            builder.Property(x => x.PhoneNumber)
                .HasColumnName("Celular");

            builder.Property(x => x.PhoneNumberConfirmed)
                .HasColumnName("CelularConfirmado");

            builder.Property(x => x.TwoFactorEnabled)
                .HasColumnName("DoisFatoresAtivo");

            builder.Property(x => x.LockoutEnd)
                .HasColumnName("BloqueioFinal");

            builder.Property(x => x.LockoutEnabled)
                .HasColumnName("BloqueioAtivo");

            builder.Property(x => x.AccessFailedCount)
                .HasColumnName("AcessoNegadoContador");

            builder.HasMany(u => u.UsuarioPerfis)
                .WithOne(up => up.Usuario)
                .HasForeignKey(up => up.UserId)
                .IsRequired();

            builder.HasMany(u => u.UsuarioPermissoes)
                .WithOne(up => up.Usuario)
                .HasForeignKey(up => up.UserId)
                .IsRequired();
        }
    }
}
