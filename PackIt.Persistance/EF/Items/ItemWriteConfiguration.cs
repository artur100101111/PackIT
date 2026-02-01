using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PackIT.Domain.Items;
using PackIT.Domain.ItemTypes;

namespace PackIt.Persistance.EF.Items
{
    internal class ItemWriteConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("Items");
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id)
                .HasConversion(id => id.Value, id => new ItemId(id)).HasColumnName("Id").HasColumnType("BIGINT");

            builder.Property(p => p.Name) // mogę to zrobić bo ... property nie jest prywatna. gdyby była prywatna -> 
            .HasConversion(n => n.Value, n => new ItemName(n)).HasColumnName("Name").HasColumnType("NVARCHAR(100)")
            .IsRequired();

            #region valueConverter as param
            ////check these options
            ////1)
            //var itemNameConverter = new ValueConverter<ItemName, string>(pln => pln.Value, pln => new ItemName(pln));
            //builder.Property<ItemName>("_name").HasConversion(itemNameConverter);
            ////2)
            //builder.Property<ItemName>("_name").HasConversion(v => v.Value, v => new ItemName(v));
            #endregion

           // builder.Property(p => p.TypeID).HasConversion(t=>t.Value, t=>new ItemTypeId(t));
            builder.HasOne(p => p.Type)
                .WithMany()
                .HasForeignKey(fk=>fk.TypeID)//shadow key // HasForeignKey("TypeId");
                .IsRequired().OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.Code)
                .HasConversion(c => c.Value, c => new ItemCode(c)).HasColumnName("Code").HasColumnType("VARCHAR(30)")
                .IsRequired();

            builder.HasIndex(p => p.Code).HasDatabaseName("IX_Item_Code").IsUnique();


            builder.Property(v => v.Version).IsConcurrencyToken().HasColumnName("Version");
            builder.Ignore(o => o.Events);
        }
    }
}
