using Blog.Domain.Entidades.ControleAcesso;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infra.Data.Mapeamentos.ControleAcesso
{
    public class PerfilPermissaoMapeamento : IEntityTypeConfiguration<PerfilPermissao>
    {
        public void Configure(EntityTypeBuilder<PerfilPermissao> builder)
        {
            builder.ToTable(nameof(PerfilPermissao));
        }
    }
}
