using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PackIt.Persistance.EF.ItemTypes.ReadModels;

namespace PackIt.Persistance.EF.ItemTypes
{
    internal class ItemTypeReadConfiguration : IEntityTypeConfiguration<ItemTypeReadModel>
    {
        public ItemTypeReadConfiguration()
        {
        }

        public void Configure(EntityTypeBuilder<ItemTypeReadModel> builder)
        {
            builder.ToTable("ItemTypes");
            builder.HasKey(k=>k.Id);
            builder.Property(k=>k.Id).HasColumnName("Id").HasColumnType("BIGINT").IsRequired();
            builder.Property(p => p.Name).HasColumnName("Name").HasColumnType("NVARCHAR(100)");
            builder.Property(p => p.Code).HasColumnName("Code").HasColumnType("VARCHAR(30)");
        }
    }
}