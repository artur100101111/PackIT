using Microsoft.Extensions.DependencyInjection;
using PackIt.Shared.Abstractions.Queries;

namespace PackIt.Shared.Queries
{
    public class InMemoryQueryDispatcher : IQueryDispatcher
    {
        private IServiceProvider _serviceProvider;
        public InMemoryQueryDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            var handler = scope.ServiceProvider.GetRequiredService(handlerType);

            return await (Task<TResult>)handlerType.GetMethod(nameof(IQueryHandler<IQuery<TResult>,TResult>.HandleAsync))?
                .Invoke(handler, new object[] { query, cancellationToken })!;
        }
    }
}
