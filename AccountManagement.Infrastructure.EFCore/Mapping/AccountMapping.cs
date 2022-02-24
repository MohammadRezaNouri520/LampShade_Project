using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EFCore.Mapping
{
    public class AccountMapping : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");
            builder.HasKey(a => a.Id);

            builder.Property(a => a.FullName).HasMaxLength(100).IsRequired();
            builder.Property(a => a.UserName).HasMaxLength(100).IsRequired();
            builder.Property(a => a.Mobile).HasMaxLength(20).IsRequired();
            builder.Property(a => a.Password).HasMaxLength(1000).IsRequired();
            builder.Property(a => a.ProfilePhoto).HasMaxLength(500);

            builder.HasOne(a => a.Role)
                .WithMany(r => r.Accounts)
                .HasForeignKey(a => a.RoleId);
        }
    }
}
