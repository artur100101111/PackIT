using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PackIT.Domain.Orders;
using PackIT.Domain.Orders.States;
using PackIT.Domain.Orders.ValueObjects;

namespace PackIt.Persistance.EF.Orders.WriteConfiguration
{
    internal class OrderWriteConfiguration: IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.Property(p => p.State)
                .HasConversion(s => s.ToString(), s => (OrderStateEnum)Enum.Parse(typeof(OrderStateEnum), s))
                .HasColumnName("Order_State")
                .HasColumnType("NVARCHAR(50)").IsRequired();

            builder.Property(p => p.RequestedDeliveryTime).HasColumnName("RequestedDeliveryTime").HasColumnType("DATETIME2").IsRequired();
            builder.Property(p => p.DeliveryTime).HasColumnName("DeliveryTime").HasColumnType("DATETIME2").IsRequired(false);

            builder.OwnsOne(o => o.DeliveryLocation, locVO =>
            {
                locVO.Property(p => p.Name)
                     .HasColumnName("Delivery_Location_Name")
                     .HasColumnType("NVARCHAR(100)");

                locVO.Property(p => p.Code)
                     .HasColumnName("Delivery_Location_Code")
                     .HasColumnType("VARCHAR(30)");

                locVO.Property(p => p.Type)
                 .HasColumnName("Delivery_Location_Type")
                 .HasColumnType("VARCHAR(30)");
            });

            builder.Navigation(o => o.DeliveryLocation)
            .IsRequired(false);


            
            builder.OwnsMany(p=>p.StateChangesHistory, sChange =>
            {
                sChange.ToTable("OrderStateChangedHistory");
                sChange.WithOwner().HasForeignKey("OrderId");

                sChange.Property<Guid>("Id");
                sChange.HasKey("Id");

                sChange.Property(p => p.EventTime).HasColumnName("EventTime").HasColumnType("DATETIME").IsRequired();
                sChange.Property(p => p.PreviousState).HasColumnName("PreviousState")
                    .HasConversion(s => s.ToString(), s => (OrderStateEnum)Enum.Parse(typeof(OrderStateEnum), s)).IsRequired();
                sChange.Property(p => p.CurrentState).HasColumnName("CurrentState")
                       .HasConversion(s => s.ToString(), s => (OrderStateEnum)Enum.Parse(typeof(OrderStateEnum), s)).IsRequired();

            }
            );
            builder.Navigation(p=>p.StateChangesHistory)
                .HasField("_orderStateChangesHistory")
                      .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
