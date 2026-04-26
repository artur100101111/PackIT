namespace PackIT.Infrastructure.Context
{
    public static class CorrelationContext
    {
        private static AsyncLocal<string> _correlationId = new AsyncLocal<string>();

        public static string? CorrelationId 
        { 
            get => _correlationId.Value; 
            set => _correlationId.Value = value; 
        }


        public static CorrelationScope BeginScope(string correlationId)
        {
            string? previous = CorrelationId;

            CorrelationId = correlationId;

            return new CorrelationScope(previous);
        }

        public readonly struct CorrelationScope : IDisposable
        {
            private readonly string? _previousCorrelationId;
            public CorrelationScope(string? previousCorrelationId)
            {
                _previousCorrelationId = previousCorrelationId;
            }
            public void Dispose()
            {
                CorrelationId = _previousCorrelationId;
            }
        }
    }
}
