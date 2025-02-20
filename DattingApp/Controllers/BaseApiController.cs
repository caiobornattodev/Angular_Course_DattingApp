using DattingAppApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace DattingAppApi.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
    }
}
