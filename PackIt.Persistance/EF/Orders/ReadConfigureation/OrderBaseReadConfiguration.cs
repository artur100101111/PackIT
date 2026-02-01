using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PackIt.Persistance.EF.Orders.ReadModels;

namespace PackIt.Persistance.EF.Orders.ReadConfigureation
{
    internal class OrderBaseReadConfiguration : 
        IEntityTypeConfiguration<OrderBaseReadModel>
    {
        public OrderBaseReadConfiguration()
        {
        }

        public void Configure(EntityTypeBuilder<OrderBaseReadModel> builder)
        {
            //or rather flat data and conversion to concreet order by descriminator?
            //no reason for now- as long as EF is dealing well with that.
            builder.ToTable("Orders");
            builder.HasDiscriminator<string>("Order_Type")
            .HasValue<OrderReadModel>("Order")
            .HasValue<OrderTemplateReadModel>("OrderTemplate");
            builder.Property("Order_Type").HasColumnType("VARCHAR(20)");
                
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).HasColumnName("Id").HasColumnType("BIGINT");
            builder.Property(P => P.CreationDate).HasColumnName("CreationDate").HasColumnType("DATETIME2").IsRequired();

            builder.OwnsOne(o => o.RequestedDeliveryLocation, locVO =>
            {
                locVO.Property(p => p.Name).HasColumnName("Requested_Location_Name").HasColumnType("NVARCHAR(100)").IsRequired();
                locVO.Property(p => p.Code).HasColumnName("Requested_Location_Code").HasColumnType("varchar(30)").IsRequired();
                locVO.Property(p => p.Type).HasColumnName("Requested_Location_Type").HasColumnName("Requested_Location_Type").HasColumnType("VARCHAR(30)").IsRequired();

            });

            builder.OwnsMany(oi => oi.OrderItems, oItems =>
            {
                oItems.ToTable("OrderItems");
                oItems.WithOwner().HasForeignKey("Order_Id");

                oItems.Property<Guid>("Id");
                oItems.HasKey("Id");

                oItems.Property(p => p.Quantity).HasColumnName("Quantity").HasColumnType("int");
                oItems.OwnsOne(p => p.ItemVO, iVO =>
                {
                    iVO.Property(i => i.Name).HasColumnName("Item_Name").HasColumnType("NVARCHAR(100)").IsRequired();
                    iVO.Property(i => i.Code).HasColumnName("Item_Code").HasColumnType("VARCHAR(30)").IsRequired();
                    iVO.Property(i => i.TypeName).HasColumnName("Item_TypeName").HasColumnType("NVARCHAR(100)").IsRequired();
                    iVO.Property(i => i.TypeCode).HasColumnName("Item_TypeCode").HasColumnType("NVARCHAR(30)").IsRequired();
                });
            });
        }
    }
}