using Microsoft.AspNetCore.Mvc;
using Model;

namespace WebAPI.Controllers
{
	[ApiController]
    [Route("[controller]")]
    public class CheckController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return Check.Status;
        }
    }
}
