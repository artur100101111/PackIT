using Microsoft.EntityFrameworkCore;
using PackIt.Persistance.EF.Items;
using PackIt.Persistance.EF.Items.ReadModels;
using PackIt.Persistance.EF.ItemTypes;
using PackIt.Persistance.EF.ItemTypes.ReadModels;
using PackIt.Persistance.EF.Locations;
using PackIt.Persistance.EF.Locations.ReadModels;
using PackIt.Persistance.EF.Orders.ReadConfigureation;
using PackIt.Persistance.EF.Orders.ReadModels;
using PackIt.Persistance.EF.Shared;

namespace PackIt.Persistance.EF.Contexts
{
    internal sealed  class ReadDbContext: DbContext
    {

        public DbSet<OrderBaseReadModel> OrderBase { get; set; }
        public DbSet<OrderReadModel> Orders { get; set; }
        public DbSet<OrderTemplateReadModel> OrderTemplates { get; set; }
        public DbSet<ItemReadModel> Items { get; set; }
        public DbSet<ItemTypeReadModel> ItemTypes { get; set; }
        public DbSet<LocationReadModel> Locations { get; set; }


        public ReadDbContext(DbContextOptions<ReadDbContext> dbContextOptions ): base( dbContextOptions )
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("packing");

            modelBuilder.Entity<BoolResult>().HasNoKey();

            var itemReadConfiguration = new ItemReadConfiguration();
            var itemTypeReadConfiguration = new ItemTypeReadConfiguration();
            var locationReadConfiguration = new LocationReadConfiguration();

            var orderBaseReadConfiguration = new OrderBaseReadConfiguration();
            var orderReadConfiguration = new OrderReadConfiguration();
            var orderTemplateReadCOnfiguration = new OrderTemplateReadConfiguration();

            modelBuilder.ApplyConfiguration<ItemReadModel>(itemReadConfiguration);
            modelBuilder.ApplyConfiguration<ItemTypeReadModel>(itemTypeReadConfiguration);
            modelBuilder.ApplyConfiguration<LocationReadModel>(locationReadConfiguration);
            modelBuilder.ApplyConfiguration<OrderBaseReadModel>(orderBaseReadConfiguration);
            modelBuilder.ApplyConfiguration<OrderReadModel>(orderReadConfiguration);
            modelBuilder.ApplyConfiguration<OrderTemplateReadModel>(orderTemplateReadCOnfiguration);

        }

    }
}
