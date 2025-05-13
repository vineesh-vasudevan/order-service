using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.Enums;

namespace OrderService.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .HasConversion(id => id.Value, value => OrderId.Of(value))
                .HasColumnName("Id")
                .ValueGeneratedNever();

            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(o => o.CustomerId)
                .IsRequired();

            builder.Navigation(b => b.Items)
              .UsePropertyAccessMode(PropertyAccessMode.Field)
              .HasField("_items");

            builder.HasMany(b => b.Items)
              .WithOne()
              .HasForeignKey(bi => bi.OrderId)
              .OnDelete(DeleteBehavior.Cascade);

            builder.ComplexProperty(
                o => o.OrderName, nameBuilder =>
                {
                    nameBuilder.Property(n => n.Value)
                        .HasColumnName(nameof(Order.OrderName))
                        .HasMaxLength(100)
                        .IsRequired();
                });

            builder.ComplexProperty(
                o => o.ShippingAddress, sa =>
                {
                    sa.Property(a => a.FirstName)
                        .HasColumnName("ShippingFirstName")
                        .HasMaxLength(50)
                        .IsRequired();

                    sa.Property(a => a.LastName)
                       .HasColumnName("ShippingLastName")
                       .HasMaxLength(50)
                       .IsRequired();

                    sa.Property(a => a.Street)
                        .HasColumnName("ShippingStreet")
                        .HasMaxLength(50)
                        .IsRequired();

                    sa.Property(a => a.City)
                        .HasColumnName("ShippingCity")
                       .HasMaxLength(50)
                       .IsRequired();

                    sa.Property(a => a.State)
                        .HasColumnName("ShippingState")
                       .HasMaxLength(50);

                    sa.Property(a => a.PostalCode)
                        .HasColumnName("ShippingPostalCode")
                       .HasMaxLength(50)
                       .IsRequired();

                    sa.Property(a => a.Country)
                       .HasColumnName("ShippingCountry")
                      .HasMaxLength(2)
                      .IsRequired();
                });

            builder.ComplexProperty(
                o => o.BillingAddress, ba =>
                {
                    ba.Property(a => a.FirstName)
                        .HasColumnName("BillingFirstName")
                        .HasMaxLength(50)
                        .IsRequired();

                    ba.Property(a => a.LastName)
                       .HasColumnName("BillingLastName")
                       .HasMaxLength(50)
                       .IsRequired();

                    ba.Property(a => a.Street)
                        .HasColumnName("BillingStreet")
                        .HasMaxLength(50)
                        .IsRequired();

                    ba.Property(a => a.City)
                        .HasColumnName("BillingCity")
                       .HasMaxLength(50)
                       .IsRequired();

                    ba.Property(a => a.State)
                        .HasColumnName("BillingState")
                       .HasMaxLength(50);

                    ba.Property(a => a.PostalCode)
                        .HasColumnName("BillingPostalCode")
                       .HasMaxLength(50)
                       .IsRequired();

                    ba.Property(a => a.Country)
                       .HasColumnName("BillingCountry")
                      .HasMaxLength(2)
                      .IsRequired();
                });

            builder.ComplexProperty(
                o => o.Payment, pb =>
                {
                    pb.Property(p => p.Amount)
                        .HasColumnName("Amount")
                        .HasColumnType("decimal(18,2)")
                        .IsRequired();

                    pb.Property(p => p.Currency)
                        .HasColumnName("Currency")
                        .HasConversion(currency => currency.Code, code => Currency.Of(code))
                        .HasMaxLength(10)
                        .IsRequired();

                    pb.Property(p => p.PaidAt)
                        .HasColumnName("PaidAt")
                       .IsRequired();

                    pb.Property(p => p.PaymentMethod)
                        .HasColumnName("PaymentMethod")
                        .HasMaxLength(50)
                        .IsRequired();

                    pb.Property(p => p.IsSuccessful)
                        .HasColumnName("IsSuccessful")
                        .IsRequired();

                    pb.Property(p => p.TransactionId)
                       .HasColumnName("TransactionId")
                       .HasMaxLength(100)
                       .HasConversion(
                           id => id.Value,
                           value => TransactionId.Of(value)
                       )
                       .IsRequired();
                });

            builder.Property(o => o.Status)
               .IsRequired()
               .HasConversion(
                   status => status.Name,
                   name => OrderStatus.FromName(name, true)
               )
               .HasMaxLength(50)
               .HasColumnName("Status");

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
        }
    }
}