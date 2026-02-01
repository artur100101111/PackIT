using PackIt.Application.Items.DTO;
using PackIt.Persistance.EF.Items.ReadModels;
using PackIt.Persistance.EF.ItemTypes.Queries;
using PackIT.Domain.Items;

namespace PackIt.Persistance.EF.Items.Queries
{
    internal static class Extensions
    {
        public static ItemDto AsDto(this ItemReadModel itemReadModel)
        => new()
        {
            Id = itemReadModel.Id,
            Name = itemReadModel.Name,
            Code = itemReadModel.Code,
            Type = itemReadModel.Type.AsDto()
        };
    }
}
