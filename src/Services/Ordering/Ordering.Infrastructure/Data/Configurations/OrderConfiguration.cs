﻿namespace Ordering.Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(cId => cId.Value, dbId => OrderId.Of(dbId));
        builder.HasMany(x => x.OrderItems).WithOne().HasForeignKey(x => x.OrderId);
        builder.HasOne<Custumer>().WithMany().HasForeignKey(x => x.Custumerid).IsRequired();

        builder.ComplexProperty(
            o=>o.OrderName,nameBuilder=>
            {
                nameBuilder.Property(n=>n.Value)
                .HasColumnName(nameof(Order.OrderName))
                .HasMaxLength(100)
                .IsRequired();
            });
        builder.ComplexProperty(
           o => o.ShippingAddress, addressBuilder =>
           {
               addressBuilder.Property(a => a.FirstName)
                   .HasMaxLength(50)
                   .IsRequired();

               addressBuilder.Property(a => a.LastName)
                    .HasMaxLength(50)
                    .IsRequired();

               addressBuilder.Property(a => a.EmailAddress)
                   .HasMaxLength(50);

               addressBuilder.Property(a => a.AddressLine)
                   .HasMaxLength(180)
                   .IsRequired();

               addressBuilder.Property(a => a.Country)
                   .HasMaxLength(50);

               addressBuilder.Property(a => a.State)
                   .HasMaxLength(50);

               addressBuilder.Property(a => a.ZipCode)
                   .HasMaxLength(5)
                   .IsRequired();
           });

        builder.ComplexProperty(
          o => o.BillingAddress, addressBuilder =>
          {
              addressBuilder.Property(a => a.FirstName)
                   .HasMaxLength(50)
                   .IsRequired();

              addressBuilder.Property(a => a.LastName)
                   .HasMaxLength(50)
                   .IsRequired();

              addressBuilder.Property(a => a.EmailAddress)
                  .HasMaxLength(50);

              addressBuilder.Property(a => a.AddressLine)
                  .HasMaxLength(180)
                  .IsRequired();

              addressBuilder.Property(a => a.Country)
                  .HasMaxLength(50);

              addressBuilder.Property(a => a.State)
                  .HasMaxLength(50);

              addressBuilder.Property(a => a.ZipCode)
                  .HasMaxLength(5)
                  .IsRequired();
          });

        builder.ComplexProperty(
               o => o.Payment, paymentBuilder =>
               {
                   paymentBuilder.Property(p => p.CardName)
                       .HasMaxLength(50);

                   paymentBuilder.Property(p => p.CardNumber)
                       .HasMaxLength(24)
                       .IsRequired();

                   paymentBuilder.Property(p => p.Expiration)
                       .HasMaxLength(10);

                   paymentBuilder.Property(p => p.Cvv)
                       .HasMaxLength(3);

                   paymentBuilder.Property(p => p.PaymentMethod);
               });

        builder.Property(e=>e.OrderStatus)
            .HasDefaultValue(OrderStatus.Draft)
            .HasConversion(
                s=>s.ToString(),
                dbStatus=>(OrderStatus)Enum.Parse(typeof(OrderStatus),dbStatus)
            );
        builder.Property(o=>o.TotalPrice );
    }
}