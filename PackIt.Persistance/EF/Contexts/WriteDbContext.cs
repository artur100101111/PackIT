using Microsoft.EntityFrameworkCore;
using PackIt.Persistance.EF.Items;
using PackIt.Persistance.EF.ItemTypes;
using PackIt.Persistance.EF.Locations;
using PackIt.Persistance.EF.Orders.WriteConfiguration;
using PackIT.Domain.Items;
using PackIT.Domain.ItemTypes;
using PackIT.Domain.Locations;
using PackIT.Domain.Orders;

namespace PackIt.Persistance.EF.Contexts
{
    internal sealed class WriteDbContext : DbContext
    {
        public DbSet<OrderBase> Orders { get; set; }

        public DbSet<Item> Items { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<Location> Locations { get; set; }
        public WriteDbContext()
        {
            
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)//log in never applied as it is configured in Service registrationExtension
        //    {
        //        //optionsBuilder
        //        //    .LogTo(Console.WriteLine, LogLevel.Information)
        //        //    .EnableSensitiveDataLogging(); // optional, development only
        //    }
        //}

        public WriteDbContext(DbContextOptions<WriteDbContext> dbContextOptions) : base(dbContextOptions) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("packing");

            var orderWriteConfiguration = new OrderBaseWriteConfiguration();
            var orderConfiguration = new OrderWriteConfiguration();
            var orderTemplateConfiguration = new OrderTemplateWriteConfiguration();
            var itemWriteConfiguration = new ItemWriteConfiguration();
            var itemTypeWriteConfiguration = new ItemTypeWriteConfiguration();
            var locationWriteConfiguration = new LocationWriteConfiguration();

            modelBuilder.ApplyConfiguration<OrderBase>(orderWriteConfiguration);
            modelBuilder.ApplyConfiguration<Order>(orderConfiguration);
            modelBuilder.ApplyConfiguration<OrderTemplate>(orderTemplateConfiguration);

            modelBuilder.ApplyConfiguration(itemWriteConfiguration);
            modelBuilder.ApplyConfiguration(itemTypeWriteConfiguration);
            modelBuilder.ApplyConfiguration(locationWriteConfiguration);
        }
    }
}