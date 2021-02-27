using Microsoft.AspNetCore.Mvc;
using Model;

namespace WebAPI.Controllers
{
	[ApiController]
    [Route("[controller]")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return Health.Status;
        }
    }
}
