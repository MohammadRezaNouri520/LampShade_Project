using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Infrastructure.EFCore.Mapping
{
    public class ProductCategoryMapping : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("ProductCategories");
            builder.HasKey(pc => pc.Id);
            builder.Property(pc => pc.Name).HasMaxLength(255).IsRequired();
            builder.Property(pc => pc.Description).HasMaxLength(500);
            builder.Property(pc => pc.Picture).HasMaxLength(1000);
            builder.Property(pc => pc.PictureAlt).HasMaxLength(255);
            builder.Property(pc => pc.PictureTitle).HasMaxLength(500);
            builder.Property(pc => pc.Keywords).HasMaxLength(80).IsRequired();
            builder.Property(pc => pc.MetaDescription).HasMaxLength(150).IsRequired();
            builder.Property(pc => pc.Slug).HasMaxLength(300).IsRequired();

            builder.HasMany<Product>(pc => pc.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);
        }
    }
}
