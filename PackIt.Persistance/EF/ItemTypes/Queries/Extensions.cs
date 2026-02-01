using PackIt.Application.Items.DTO;
using PackIt.Persistance.EF.ItemTypes.ReadModels;

namespace PackIt.Persistance.EF.ItemTypes.Queries
{
    internal static class Extensions
    {
        public static ItemTypeDto AsDto(this ItemTypeReadModel itemTypeReadModel)
            => new() { Id = itemTypeReadModel.Id, Name = itemTypeReadModel.Name, Code = itemTypeReadModel.Code };
    }
}
