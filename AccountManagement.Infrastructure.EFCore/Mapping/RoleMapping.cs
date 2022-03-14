using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EFCore.Mapping
{
    public class RoleMapping : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasMany(r => r.Accounts)
                .WithOne(a => a.Role)
                .HasForeignKey(a => a.RoleId);

            builder.OwnsMany(x => x.RolePermissions, navigationBuilder => 
            {
                navigationBuilder.ToTable("RolePermissions");
                navigationBuilder.HasKey(r => r.Id);
                //navigationBuilder.Property(r => r.Name).HasMaxLength(150).IsRequired();
                navigationBuilder.Ignore(r => r.Name);
                navigationBuilder.Property(r => r.Code).IsRequired();
                navigationBuilder.WithOwner(r => r.Role).HasForeignKey(r => r.RoleId);
            });
        }
    }
}
