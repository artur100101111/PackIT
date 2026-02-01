using Microsoft.Extensions.Configuration;
namespace PackIt.Shared.Abstractions.Options
{
    public static class Extensions
    {
        /// <summary>
        /// Section Name -> like 'MsSQL' section or 'Time' section with values required for configuraiton.
        /// Binder assigns values to Options Properties by the same Property name.
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="configuration"></param>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        public static TOptions GetOptions<TOptions>(this IConfiguration configuration, string sectionName)
            where TOptions : new()
        {
            var options = new TOptions();
            configuration.GetSection(sectionName).Bind(options);

            return options;
        }

    }
}
