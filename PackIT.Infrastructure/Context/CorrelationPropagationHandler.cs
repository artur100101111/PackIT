using Microsoft.AspNetCore.Http;

namespace PackIT.Infrastructure.Context
{
    public sealed class CorrelationPropagationHandler : DelegatingHandler
    {
        private IHttpContextAccessor _httpContextAccessor;
        public const string _headerName = CorrelationConstants.HeaderName;
        public const string _itemKey = CorrelationConstants.ItemKey;

        public CorrelationPropagationHandler(IHttpContextAccessor  contextAccessor) 
        {
            _httpContextAccessor = contextAccessor;
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //var correlationId = _httpContextAccessor.HttpContext?.Items[_itemKey] as string;
            var correlationId = CorrelationContext.CorrelationId;

            if (!string.IsNullOrEmpty(correlationId))
            {
                request.Headers.TryAddWithoutValidation(_headerName, correlationId);
            }


            return base.SendAsync(request, cancellationToken);
        }
    }
}
