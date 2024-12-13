namespace CodingBlog.Infrastructure.EntityFramework.ModelConfiguration;

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PostModelConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.UserId)
            .IsRequired();
        
        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(p => p.Content)
            .IsRequired();
        
        builder.Property(x => x.CreatedAt).HasColumnType("timestamp");
    }
}