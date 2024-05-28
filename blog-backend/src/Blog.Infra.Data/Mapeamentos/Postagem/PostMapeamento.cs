using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Blog.Domain.Entidades.Postagem;

namespace Blog.Infra.Data.Mapeamentos.Postagem
{
    public class PostMapeamento : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable(nameof(Post));

            builder.Property(x => x.Titulo)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(x => x.Conteudo)
                .HasColumnType("varchar(300)")
                .IsRequired();

            builder.Property(x => x.DataCriacao)
                .IsRequired();

            builder.Property(x => x.DataAtualizacao);

            builder.HasOne(p => p.Autor)
                .WithMany(a => a.UsuarioPosts)
                .HasForeignKey(p => p.AutorId);
        }
    }
}
