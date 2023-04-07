using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure;

public class ProductsConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.Id).IsRequired();
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Description).IsRequired();
        builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
        builder.Property(p => p.ImgUrl).IsRequired();
        builder.HasOne(b => b.ProductBrand).WithMany()
               .HasForeignKey(p => p.ProductBrandId);
        builder.HasOne(t => t.ProductType).WithMany()
               .HasForeignKey(p => p.ProductTypeId);
    }
}
