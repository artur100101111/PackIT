using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PackIT.Domain.ItemTypes;

namespace PackIt.Persistance.EF.ItemTypes
{
    internal class ItemTypeWriteConfiguration : IEntityTypeConfiguration<ItemType>
    {
        public void Configure(EntityTypeBuilder<ItemType> builder)
        {
            builder.ToTable("ItemTypes");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("bigint").HasConversion(id=>id.Value, v=> new ItemTypeId(v));
            builder.Property(x => x.Name).HasColumnType("NVARCHAR(100)").IsRequired();
            builder.Property(x => x.Code).HasColumnType("VARCHAR(30)").IsRequired();
            builder.Property(v => v.Version).IsConcurrencyToken().HasColumnName("Version").IsRequired();

            builder.HasIndex(x => x.Code).HasDatabaseName("IX_ItemTypes_Code").IsUnique();

            builder.Ignore(o => o.Events);
        }
    }
}
