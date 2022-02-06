using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CommentManagement.Domain.CommentAgg;

namespace CommentManagement.Infrastructure.EFCore.Mapping
{
    public class CommentMapping : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasMaxLength(500).IsRequired();
            builder.Property(c => c.Email).HasMaxLength(500).IsRequired();
            builder.Property(c => c.Website).HasMaxLength(500);
            builder.Property(c => c.Message).HasMaxLength(1000).IsRequired();
        }
    }
}
