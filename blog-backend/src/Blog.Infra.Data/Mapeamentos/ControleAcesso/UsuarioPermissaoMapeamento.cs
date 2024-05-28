using Blog.Domain.Entidades.ControleAcesso;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infra.Data.Mapeamentos.ControleAcesso
{
    public class UsuarioPermissaoMapeamento : IEntityTypeConfiguration<UsuarioPermissao>
    {
        public void Configure(EntityTypeBuilder<UsuarioPermissao> builder)
        {
            builder.ToTable(nameof(UsuarioPermissao));
        }
    }
}
