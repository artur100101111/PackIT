using System.Linq.Expressions;

namespace PackIt.Shared.Abstractions.Shared
{
    public interface ISpecyfication<TEntity> where TEntity : class
    {
        Expression<Func<TEntity, bool>> Criteria { get; }
        List<Expression<Func<TEntity, object>>> Includes { get; }
    }
}
