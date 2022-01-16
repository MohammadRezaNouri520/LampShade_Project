using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Infrastructure.EFCore.Mapping
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasMaxLength(255).IsRequired();
            builder.Property(p => p.Code).HasMaxLength(15).IsRequired();
            builder.Property(p => p.ShortDescription).HasMaxLength(500).IsRequired();
            builder.Property(p => p.Description);
            builder.Property(p => p.Picture).HasMaxLength(1000);
            builder.Property(p => p.PictureAlt).HasMaxLength(255);
            builder.Property(p => p.PictureTitle).HasMaxLength(500);
            builder.Property(p => p.Keywords).HasMaxLength(100).IsRequired();
            builder.Property(p => p.MetaDesctiption).HasMaxLength(150).IsRequired();
            builder.Property(p => p.Slug).HasMaxLength(500).IsRequired();
            builder.Property(p => p.CategoryId).IsRequired();

            builder.HasOne<ProductCategory>(p => p.Category)
                .WithMany(pc => pc.Products)
                .HasForeignKey(p => p.CategoryId);

            builder.HasMany<ProductPicture>(p => p.ProductPictures)
                .WithOne(x => x.Product)
                .HasForeignKey(p => p.ProductId);

            builder.HasMany(p => p.Comments)
                .WithOne(c => c.Product)
                .HasForeignKey(c => c.ProductId);
        }
    }
}
