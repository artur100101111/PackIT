using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PackIT.Domain.Orders;

namespace PackIt.Persistance.EF.Orders.WriteConfiguration
{
    internal class OrderTemplateWriteConfiguration: IEntityTypeConfiguration<OrderTemplate>
    {
        public void Configure(EntityTypeBuilder<OrderTemplate> builder)
        {
            builder.ToTable("Orders");

            builder.Property(p => p.Name).HasColumnType("NVARCHAR(100)").IsRequired();
        }
    }
}
