namespace PackIT.Domain.Shared
{
    public interface IDateTimeService
    {
        DateTime UtcNow { get; }
        DateTime LocalNow { get; }
        DateTime ToUtc(DateTime localDateTime);
        DateTime ToLocal(DateTime utcDateTime);
    }
}
