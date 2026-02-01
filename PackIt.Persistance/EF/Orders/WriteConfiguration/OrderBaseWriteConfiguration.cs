using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PackIT.Domain.Orders;

namespace PackIt.Persistance.EF.Orders.WriteConfiguration
{
    internal sealed class OrderBaseWriteConfiguration : IEntityTypeConfiguration<OrderBase>
    {
        public void Configure(EntityTypeBuilder<OrderBase> builder)
        {
            builder.ToTable("Orders");
            builder.UseTphMappingStrategy()
               .HasDiscriminator<string>("Order_Type")
               .HasValue<Order>("Order")
               .HasValue<OrderTemplate>("OrderTemplate");
            builder.Property("Order_Type").HasColumnType("VARCHAR(20)");

            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).HasColumnName("Id").HasColumnType("BIGINT")
                .HasConversion(id => id.Value, id => new OrderId(id));

            builder.Property(p => p.CreationDate)
                .HasColumnName("CreationDate").HasColumnType("DATETIME2")
                .IsRequired();



            builder.OwnsOne(o => o.RequestedDeliveryLocation, locVO =>
            {

                locVO.Property(p => p.Name)
                     .HasColumnName("Requested_Location_Name")
                     .HasColumnType("NVARCHAR(100)")
                     .IsRequired();

                locVO.Property(p => p.Code)
                     .HasColumnName("Requested_Location_Code")
                     .HasColumnType("VARCHAR(30)")
                     .IsRequired();


                locVO.Property(p => p.Type)
                 .HasColumnName("Requested_Location_Type")
                 .HasColumnType("VARCHAR(30)")
                 .IsRequired();

                locVO.HasIndex(p => p.Code).HasDatabaseName("IX_Orders_Requested_Location_Code");
            });




            //OrderItems
            builder.OwnsMany(oi => oi.OrderItems, oItems =>
            {
                oItems.ToTable("OrderItems");
                oItems.WithOwner().HasForeignKey("Order_Id");

                oItems.Property<Guid>("Id");
                oItems.HasKey("Id");

                oItems.Property(q => q.Quantity).HasColumnType("int").HasColumnName("Quantity").IsRequired();


                oItems.OwnsOne(p => p.ItemVO, iVO =>
                {
                    iVO.Property(i => i.Name).HasColumnName("Item_Name").HasColumnType("NVARCHAR(100)").IsRequired();
                    iVO.Property(i => i.Code).HasColumnName("Item_Code").HasColumnType("VARCHAR(30)").IsRequired();
                    iVO.Property(i => i.TypeName).HasColumnName("Item_TypeName").HasColumnType("NVARCHAR(100)").IsRequired();
                    iVO.Property(i => i.TypeCode).HasColumnName("Item_TypeCode").HasColumnType("NVARCHAR(30)").IsRequired();

                    iVO.HasIndex(i => i.Code).HasDatabaseName("IX_OrderItems_ItemVO_Code");
                }
                );


            }
            );

            builder.Property(v => v.Version).IsConcurrencyToken().HasColumnName("Version");
            builder.Ignore(o => o.Events);
        }
      
    }
}
