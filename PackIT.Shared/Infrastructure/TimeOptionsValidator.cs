using Microsoft.Extensions.Options;
using System;

namespace PackIT.Shared.Infrastructure
{
    public class TimeOptionsValidator : IValidateOptions<TimeOptions>
    {
        public ValidateOptionsResult Validate(string? name, TimeOptions options)
        {
            try
            {
                TimeZoneInfo.FindSystemTimeZoneById(options.TimeZoneId);
                return ValidateOptionsResult.Success;
            }
            catch (TimeZoneNotFoundException)
            {
                return ValidateOptionsResult.Fail($"Unknown TimeZoneId: {options.TimeZoneId}");
            }
            catch (InvalidTimeZoneException)
            {
                return ValidateOptionsResult.Fail($"Invalid TimeZoneId: {options.TimeZoneId}");
            }
        }
    }
}
