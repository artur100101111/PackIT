using Microsoft.AspNetCore.Mvc;

namespace PackIT.Infrastructure.Exceptions
{
    public class CustomProblemDetails: ProblemDetails
    {
        public Dictionary<string,object> Extensions { get; set; } = new Dictionary<string, object>();
    }
}
