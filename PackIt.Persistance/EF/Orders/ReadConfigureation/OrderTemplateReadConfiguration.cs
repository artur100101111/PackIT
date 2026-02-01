using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PackIt.Persistance.EF.Orders.ReadModels;

namespace PackIt.Persistance.EF.Orders.ReadConfigureation
{
    internal class OrderTemplateReadConfiguration : IEntityTypeConfiguration<OrderTemplateReadModel>
    {
        public void Configure(EntityTypeBuilder<OrderTemplateReadModel> builder)
        {
            builder.ToTable("Orders");

            builder.Property(p => p.Name).HasColumnType("NVARCHAR(100)").IsRequired();
        }
    }
}
