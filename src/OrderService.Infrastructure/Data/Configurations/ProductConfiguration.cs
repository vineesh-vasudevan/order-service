using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderService.Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
               .HasConversion(
                   id => id.Value,
                   value => ProductId.Of(value)
               )
               .HasColumnName("Id")
               .ValueGeneratedNever();

            builder.Property(p => p.Name)
              .IsRequired()
              .HasMaxLength(200);

            builder.Property(p => p.Price)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Description)
                   .HasMaxLength(1000);

            builder.Property(p => p.Code)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasIndex(p => p.Code).IsUnique();
        }
    }
}
