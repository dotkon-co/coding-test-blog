using CodingBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodingBlog.Infrastructure.EntityFramework.ModelConfiguration;

public class UserModelConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Username).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(150);
        builder.Property(u => u.Password).IsRequired().HasMaxLength(150);
        builder.Property(u => u.Role).IsRequired().HasMaxLength(50);

        builder.HasMany(u => u.Posts).WithOne(p => p.User).HasForeignKey(p => p.UserId);
    }
}