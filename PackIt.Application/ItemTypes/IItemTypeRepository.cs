using PackIt.Shared.Abstractions.Persistance;
using PackIt.Shared.Abstractions.Shared;
using PackIT.Domain.ItemTypes;

namespace PackIt.Application.ItemTypes
{
    public interface IItemTypeRepository:IWriteRepository<ItemType, ItemTypeId>
    {
        Task<T?> GetItemTypeBySpecyfictionAsync<T>(ISpecyfication<T> filterSpecylfication, CancellationToken cancellationToken = default) where T : ItemType;
        Task<IEnumerable<T>> GetItemsTypesBySpecyfictionAsync<T>(ISpecyfication<T> filterSpecylfication, CancellationToken cancellationToken = default) where T : ItemType;
        Task<bool> CheckIfItemTypeExistsAsync<T>(ISpecyfication<T> existanceSpecyfication, CancellationToken cancellationToken = default) where T : ItemType;
    }
}
