namespace OrderService.Infrastructure.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
              .HasConversion(
                  id => id.Value,
                  value => CustomerId.Of(value)
              )
              .HasColumnName("Id")
              .ValueGeneratedNever();

            builder.Property(c => c.FirstName)
               .IsRequired()
               .HasMaxLength(100);

            builder.Property(c => c.LastName)
               .IsRequired()
               .HasMaxLength(100);

            builder.Property(c => c.Email)
              .IsRequired()
              .HasMaxLength(256);

            builder.Property(c => c.CreatedAt)
              .IsRequired();

            builder.Property(c => c.LastModifiedAt)
               .IsRequired();

            builder.Property(c => c.CreatedBy)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.LastModifiedBy)
                   .IsRequired()
                   .HasMaxLength(100);
        }
    }
}