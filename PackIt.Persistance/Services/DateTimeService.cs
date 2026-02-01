using Microsoft.Extensions.Options;
using PackIT.Domain.Shared;
using PackIT.Shared.Infrastructure;

namespace PackIt.Persistance.Services
{
    internal class DateTimeService : IDateTimeService
    {
        private readonly TimeZoneInfo _timeZoneInfo;
        public string TimeZoneId { get; private set; }

        public DateTimeService(IOptions<TimeOptions> options)
        {
            TimeZoneId = options.Value.TimeZoneId;
            _timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneId);
        }
        public DateTime UtcNow => DateTime.UtcNow;

        public DateTime LocalNow => TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _timeZoneInfo);

        public DateTime ToLocal(DateTime utcDateTime)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, _timeZoneInfo);
        }

        public DateTime ToUtc(DateTime localDateTime)
        {
            return TimeZoneInfo.ConvertTimeToUtc(localDateTime, _timeZoneInfo);
        }
    }
}
