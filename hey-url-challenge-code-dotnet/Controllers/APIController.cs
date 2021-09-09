using hey_url_challenge_code_dotnet.Models;
using JsonApiDotNetCore.Configuration;
using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Services;
using Microsoft.Extensions.Logging;

namespace hey_url_challenge_code_dotnet.Controllers
{
    
    public class APIController : JsonApiController<Url>
    {
        public APIController(IJsonApiOptions options, ILoggerFactory loggerFactory, IResourceService<Url> resourceService)
            : base(options, loggerFactory, resourceService) 
        { }
    }
}
