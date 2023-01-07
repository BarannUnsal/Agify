using Agify.BL.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Agify.API.Controllers
{
    [ApiController]
    [Route("/")]
    public class AgifyController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly ILogger<AgifyController> _logger;

        public AgifyController(ILogger<AgifyController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet(Name = "?name")]
        public async Task<JsonResult> Name([FromQuery] string[]? name)
        {
            try
            {
                if (name != null)
                {
                    var users = await _userService.GetAsync(name);
                    if (users != null)
                    {
                        _logger.LogInformation("Query successfuly");
                        return new JsonResult(users);
                    }
                    else
                    {
                        _logger.LogError("Query not found!!!");
                        return new JsonResult("error: Missing 'name' parameter");
                    }
                }
                return new JsonResult("error: Missing 'name' parameter");
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}