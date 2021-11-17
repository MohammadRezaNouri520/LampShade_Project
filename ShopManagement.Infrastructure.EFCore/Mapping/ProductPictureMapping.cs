using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Infrastructure.EFCore.Mapping
{
    public class ProductPictureMapping : IEntityTypeConfiguration<ProductPicture>
    {
        public void Configure(EntityTypeBuilder<ProductPicture> builder)
        {
            builder.ToTable("ProductPictures");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Picture).HasMaxLength(1000).IsRequired();
            builder.Property(p => p.PicutreAlt).HasMaxLength(500).IsRequired();
            builder.Property(p => p.PictureTitle).HasMaxLength(500).IsRequired();
            builder.Property(p => p.ProductId).IsRequired();
            builder.Property(p => p.IsRemoved);
            builder.Property(p => p.CreationDate).IsRequired();

            builder.HasOne<Product>(p => p.Product)
                .WithMany(x => x.ProductPictures)
                .HasForeignKey(p => p.ProductId);
        }
    }
}
