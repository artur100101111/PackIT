using PackIt.Persistance.EF.ItemTypes;
using PackIt.Persistance.EF.ItemTypes.ReadModels;

namespace PackIt.Persistance.EF.Items.ReadModels
{
    internal class ItemReadModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public ItemTypeReadModel Type { get; set; }
        public long TypeId { get; set; }
        public int Version { get; set; }
    }
}