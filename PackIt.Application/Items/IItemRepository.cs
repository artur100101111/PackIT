using PackIt.Shared.Abstractions.Persistance;
using PackIt.Shared.Abstractions.Shared;
using PackIT.Domain.Items;

namespace PackIt.Application.Items
{
    public interface IItemRepository : IWriteRepository<Item, ItemId>
    {
        Task<T?> GetItemBySpecyfictionAsync<T>(ISpecyfication<T> filterSpecylfication, CancellationToken cancellationToken = default) where T : Item;
        Task<IEnumerable<T>> GetItemsBySpecyfictionAsync<T>(ISpecyfication<T> filterSpecylfication, CancellationToken cancellationToken = default) where T : Item;
        Task<bool> CheckIfItemExistsAsync<T>(ISpecyfication<T> existanceSpecyfication, CancellationToken cancellationToken = default) where T : Item;
    }
}
