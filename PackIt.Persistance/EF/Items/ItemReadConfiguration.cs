using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders; 
using PackIt.Persistance.EF.Items.ReadModels;

namespace PackIt.Persistance.EF.Items
{
    internal class ItemReadConfiguration : IEntityTypeConfiguration<ItemReadModel>
    {
        public ItemReadConfiguration()
        {
        }

        public void Configure(EntityTypeBuilder<ItemReadModel> builder)
        {
            builder.ToTable("Items");
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).HasColumnName("Id").HasColumnType("BIGINT");
            builder.Property(p => p.Name).HasColumnName("Name").HasColumnType("NVARCHAR(100)").IsRequired();
            builder.Property(p => p.Code).HasColumnName("Code").HasColumnType("VARCHAR(30)").IsRequired();

            builder.HasOne(t => t.Type)
                .WithMany()
                .HasForeignKey(t => t.TypeId)
                .IsRequired();
        }
    }
}