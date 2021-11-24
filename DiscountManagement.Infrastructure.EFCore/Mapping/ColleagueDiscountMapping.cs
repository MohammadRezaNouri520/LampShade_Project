using DiscountManagement.Domain.ColleagueDiscountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscountManagement.Infrastructure.EFCore.Mapping
{
    public class ColleagueDiscountMapping : IEntityTypeConfiguration<ColleagueDiscount>
    {
        public void Configure(EntityTypeBuilder<ColleagueDiscount> builder)
        {
            builder.ToTable("ColleagueDiscounts");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.ProductId).IsRequired();
            builder.Property(c => c.DiscountRate).HasMaxLength(3).IsRequired();
            builder.Property(c => c.IsRemoved).IsRequired();
            builder.Property(c => c.CreationDate).IsRequired();
        }
    }
}
