using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace PackIt.Persistance.EF.Options
{
    public class MsSqlOptionsValidator : IValidateOptions<MsSQLOptions>
    {
        public ValidateOptionsResult Validate(string? name, MsSQLOptions options)
        {
            if (string.IsNullOrWhiteSpace(options.ConnectionString))
            {
                return ValidateOptionsResult.Fail("ConnectionString is required.");
            }

            var builder = new SqlConnectionStringBuilder(options.ConnectionString);

            if (string.IsNullOrWhiteSpace(builder.DataSource))
                return ValidateOptionsResult.Fail("Server (DataSource) is missing.");

            if (string.IsNullOrWhiteSpace(builder.InitialCatalog))
                return ValidateOptionsResult.Fail("Database name is missing.");

            if (!builder.IntegratedSecurity && string.IsNullOrWhiteSpace(builder.UserID))
                return ValidateOptionsResult.Fail("User Id is required when not using Integrated Security.");


            return ValidateOptionsResult.Success;

        }
    }
}
