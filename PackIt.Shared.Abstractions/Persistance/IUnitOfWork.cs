namespace PackIt.Shared.Abstractions.Persistance
{
    public interface IUnitOfWork
    {
        Task SaveAsync(CancellationToken cancellationToken = default);
    }


}
