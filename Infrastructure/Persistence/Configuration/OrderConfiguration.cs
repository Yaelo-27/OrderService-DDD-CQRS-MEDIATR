using Domain.Orders;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasConversion(
                    id => id.Value,
                    value => new OrderId(value)
            );
            builder.OwnsOne(o => o.ShippingContact, sc =>
            {
                sc.Property(c => c.Name).HasColumnName("ShippingContactName").IsRequired().HasMaxLength(50);
                sc.Property(c => c.Email).HasColumnName("ShippingContactEmail").IsRequired().HasMaxLength(20);
                sc.Property(c => c.PhoneNumber).HasConversion(
                    phone => phone.Value,
                    value => PhoneNumber.Create(value)!
                ).HasColumnName("ShippingContactPhone").IsRequired().HasMaxLength(12);
                
            });
            builder.OwnsOne(o => o.ShippingAddress, sa =>
            {
                sa.Property(a => a.Street).HasColumnName("ShippingAddressStreet").IsRequired().HasMaxLength(100);
                sa.Property(a => a.City).HasColumnName("ShippingAddressCity").IsRequired().HasMaxLength(50);
                sa.Property(a => a.State).HasColumnName("ShippingAddressState").IsRequired().HasMaxLength(50);
                sa.Property(a => a.PostalCode).HasColumnName("ShippingAddressZipCode").IsRequired().HasMaxLength(10);
                sa.Property(a => a.Country).HasColumnName("ShippingAddressCountry").IsRequired().HasMaxLength(50);
            }); 
            
            
            builder.OwnsMany(o => o.Items, item =>
            {
                item.HasKey("OrderId", "ProductId");
                item.Property(i => i.ProductId)
                    .HasColumnName("ProductId");
                item.Property(i => i.ProductName)
                    .HasColumnName("ProductName")
                    .HasMaxLength(200);
                item.Property(i => i.UnitPrice)
                    .HasColumnName("UnitPrice")
                    .HasColumnType("decimal(18,2)");
                item.Property(i => i.Quantity)
                    .HasColumnName("Quantity");
            });
              builder.Navigation(o => o.Items)
                .HasField("_items");
            builder.Property(o => o.Status).HasColumnName("Status").HasConversion<string>().IsRequired();
            builder.Ignore(o => o.TotalAmount);
        }
    }
}