using Microsoft.AspNetCore.Http;
using static System.Net.WebRequestMethods;
using static System.Reflection.Metadata.BlobBuilder;

namespace PackIT.Infrastructure.Context
{
    public sealed class CorrelationPropagationHandler : DelegatingHandler
    {
        private IHttpContextAccessor _httpContextAccessor;
        public const string _headerName = "X-Correlation-Id";

        //        OrderService(Service A)
        //  ↓ HTTP
        //PaymentService(Service B)
        //  ↓ HTTP
        //ShippingService(Service C)
        //Goal:
        //One CorrelationId flows through ALL services
        public CorrelationPropagationHandler(IHttpContextAccessor  contextAccessor) 
        {
            _httpContextAccessor = contextAccessor;
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var correlationId = _httpContextAccessor.HttpContext?.Items["correlationId"] as string;

            if (string.IsNullOrEmpty(correlationId))
            {
                request.Headers.TryAddWithoutValidation(_headerName, correlationId);
            }


            return base.SendAsync(request, cancellationToken);
        }
    }
}
