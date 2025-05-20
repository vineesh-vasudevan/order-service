namespace OrderService.Infrastructure.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItem");

            builder.HasKey(oi => oi.Id);

            builder.Property(oi => oi.Id)
                .HasConversion(
                    id => id.Value,
                    value => OrderItemId.Of(value)
                )
                .HasColumnName("Id");

            builder.Property(oi => oi.OrderId)
                .HasConversion(
                   id => id.Value,
                   value => new OrderId(value)
                )
                .HasColumnName("OrderId")
                .IsRequired();

            builder.HasOne<Product>()
               .WithMany()
               .HasPrincipalKey(p => p.Code)
               .HasForeignKey(oi => oi.ProductCode)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);

            builder.Property(oi => oi.Quantity)
                   .IsRequired();

            builder.Property(oi => oi.UnitPrice)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(oi => oi.TotalPrice)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(oi => oi.CreatedAt)
                   .IsRequired();

            builder.Property(oi => oi.LastModifiedAt)
                   .IsRequired();

            builder.Property(oi => oi.CreatedBy)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(oi => oi.LastModifiedBy)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(o => o.Status)
              .IsRequired()
              .HasConversion(
                  status => status.Name,
                  name => OrderItemStatus.FromName(name, true)
              )
              .HasMaxLength(50)
              .HasColumnName("Status");
        }
    }
}