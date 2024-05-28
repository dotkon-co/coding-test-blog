using Blog.Core.Utilitarios.Conversores;
using Blog.Domain.Entidades.ControleAcesso;
using Blog.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infra.Data.Mapeamentos.ControleAcesso
{
    public class PerfilMapeamento : IEntityTypeConfiguration<Perfil>
    {
        public void Configure(EntityTypeBuilder<Perfil> builder)
        {
            builder.ToTable(nameof(Perfil));

            builder.Property(x => x.Name)
                .HasColumnName("NomePerfil");

            builder.Property(x => x.NormalizedName)
                .HasColumnName("NomePerfilNormalizado");

            builder.HasMany(u => u.UsuarioPerfis)
                .WithOne(up => up.Perfil)
                .HasForeignKey(up => up.RoleId)
                .IsRequired();

            builder.HasMany(p => p.PerfilPermissoes)
                .WithOne(pp => pp.Perfil)
                .HasForeignKey(up => up.RoleId)
                .IsRequired();

            SeedPerfil(builder);
        }

        private static void SeedPerfil(EntityTypeBuilder<Perfil> builder)
        {
            builder.HasData(
                new Perfil
                {
                    Id = Guid.Parse("50df15be-8daf-4c13-9da4-2e39a2a11292"),
                    Name = UsuarioPerfilEnum.Administrador.ObterDescricaoEnum(),
                    NormalizedName = UsuarioPerfilEnum.Administrador.ObterDescricaoEnum().ToUpper()
                },
                new Perfil
                {
                    Id = Guid.Parse("9c95be4c-e5a2-4648-a0c2-6afad240053f"),
                    Name = UsuarioPerfilEnum.Usuario.ObterDescricaoEnum(),
                    NormalizedName = UsuarioPerfilEnum.Usuario.ObterDescricaoEnum().ToUpper()
                }
            );
        }
    }
}
