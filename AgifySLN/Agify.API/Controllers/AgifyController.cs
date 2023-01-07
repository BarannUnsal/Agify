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

        [HttpGet(Name = "{name?}{names?}")]
        public async Task<JsonResult> Name([FromQuery] string[]? names, [FromQuery] string? name)
        {
            if (names == null && name == null)
            {
                return new JsonResult("error: Missing 'name[]' parameter");
            }

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
                        return new JsonResult("error: User not found");
                    }
                }
                else if (names != null)
                {
                    var users = await _userService.GetArrayAsync(names);
                    if (users != null)
                    {
                        _logger.LogInformation("Query successfuly");
                        return new JsonResult(users);
                    }
                    else
                    {
                        _logger.LogError("Query not found!!!");
                        return new JsonResult("error: Users not found");
                    }
                }
                return new JsonResult("Missing 'name' or 'name[]' parameter");
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Error");
                return new JsonResult("error: Error request");
            }
        }
    }
}