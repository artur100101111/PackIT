using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PackIT.Domain.Locations;

namespace PackIt.Persistance.EF.Locations
{
    internal sealed class LocationWriteConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {


            builder.ToTable("Locations");
            builder.ToTable(t =>
                            t.HasCheckConstraint("CK_Location_Type_Enum", 
                                                    "Type IN ('Factory', 'Area', 'Line', 'Warehouse')")
            );//LocationTypeEnum -> Enum seems not to be a good idea as a type in this case... in any case. 
                
            builder.HasKey(x => x.Id);
            builder.Property(k => k.Id).HasConversion(v => v.Value, v => new LocationId(v))
                .HasColumnName("Id").HasColumnType("bigint");

            builder.Property(p => p.Name).HasConversion(v => v.Value, v => new LocationName(v))
                .HasColumnName("Name").HasColumnType("NVARCHAR(100)").IsRequired();

            builder.Property(p => p.Code).HasConversion(v => v.Value, v => new LocationCode(v))
                .HasColumnName("Code").HasColumnType("VARCHAR(30)").IsRequired();

            builder.Property(p => p.Description).HasColumnName("Description").HasColumnType("VARCHAR(100)")
                .HasConversion(v=>v.Value, v => v == null ? null! : new LocationDescription(v)).IsRequired(false);

            builder.Property(p=>p.Type).HasColumnName("Type").HasColumnType("VARCHAR(20)")
                .HasConversion(t=>t.Value.ToString(), t=> new LocationType((LocationTypeEnum)Enum.Parse(typeof(LocationTypeEnum), t)))
                .IsRequired(); 

            builder.HasOne(p => p.ParentLocation)
                .WithMany(d => d.Sublocations)
                .HasForeignKey(p=>p.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(p => p.Code).HasDatabaseName("IX_Locations_Code").IsUnique();

            builder.Property(v => v.Version).IsConcurrencyToken().HasColumnName("Version");
            builder.Ignore(o => o.Events);


        }
    }
}
