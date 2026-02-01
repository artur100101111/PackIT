using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PackIt.Persistance.EF.Locations.ReadModels;
using PackIT.Domain.Locations;

namespace PackIt.Persistance.EF.Locations
{
    internal class LocationReadConfiguration : IEntityTypeConfiguration<LocationReadModel>
    {
        public LocationReadConfiguration()
        {
        }

        public void Configure(EntityTypeBuilder<LocationReadModel> builder)
        {
            builder.ToTable("Locations");
            builder.HasKey("Id");
            builder.Property("Id").HasColumnName("Id").HasColumnType("BIGINT");
            builder.Property(p => p.Name).HasColumnName("Name").HasColumnType("NVARCHAR(100)").IsRequired();
            builder.Property(p => p.Code).HasColumnName("Code").HasColumnType("VARCHAR(30)").IsRequired();
            builder.Property(p => p.Description).HasColumnName("Description").HasColumnType("VARCHAR(100)");
            builder.Property(p => p.Type).HasColumnName("Type").HasColumnType("VARCHAR(20)")
                .HasConversion(p=>p.ToString(), p=> (LocationTypeEnum)Enum.Parse(typeof(LocationTypeEnum), p))
                .IsRequired();

            builder.HasOne(p => p.Parent)
                .WithMany(d=>d.Sublocations)
                .HasForeignKey(p => p.ParentId);

            builder.Property(v => v.Version).HasColumnName("Version");
        }
    }
}