using Blog.Domain.Entidades.ControleAcesso;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infra.Data.Mapeamentos.ControleAcesso
{
    public class UsuarioPerfilMapeamento : IEntityTypeConfiguration<UsuarioPerfil>
    {
        public void Configure(EntityTypeBuilder<UsuarioPerfil> builder)
        {
            builder.ToTable(nameof(UsuarioPerfil));

            builder.HasKey(x => new { x.UserId, x.RoleId });

            builder.Property(x => x.RoleId)
                .HasColumnName("PerfilId");

            builder.Property(x => x.UserId)
                .HasColumnName("UsuarioId");
        }
    }
}
