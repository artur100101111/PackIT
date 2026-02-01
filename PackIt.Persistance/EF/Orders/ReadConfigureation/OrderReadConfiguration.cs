using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PackIt.Persistance.EF.Orders.ReadModels;
using PackIT.Domain.Orders.States;

namespace PackIt.Persistance.EF.Orders.ReadConfigureation
{
    internal class OrderReadConfiguration : IEntityTypeConfiguration<OrderReadModel>
    {
        public void Configure(EntityTypeBuilder<OrderReadModel> builder)
        {
            builder.ToTable("Orders");

            builder.Property(p => p.DeliveryDate).HasColumnName("DeliveryTime").HasColumnType("DATETIME2");//zmienić na DeliveryDate
            builder.Property(p => p.State).HasConversion(s => s.ToString(), s=> (OrderStateEnum)Enum.Parse(typeof(OrderStateEnum), s))
                .HasColumnName("Order_State");

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


            builder.OwnsMany(e => e.StateChangesHistory, sChange =>
            {
                sChange.ToTable("OrderStateChangedHistory");
                sChange.WithOwner().HasForeignKey("OrderId");

                sChange.Property<Guid>("Id");
                sChange.HasKey("Id");

                sChange.Property(p => p.EventTime).HasColumnName("EventTime").IsRequired();
                sChange.Property(p => p.PreviousState).HasColumnName("PreviousState")
                    .HasConversion(s => s.ToString(), s => (OrderStateEnum)Enum.Parse(typeof(OrderStateEnum), s));
                sChange.Property(p => p.CurrentState).HasColumnName("CurrentState")
                    .HasConversion(s => s.ToString(), s => (OrderStateEnum)Enum.Parse(typeof(OrderStateEnum), s));
            }
          );

        }
    }
}
