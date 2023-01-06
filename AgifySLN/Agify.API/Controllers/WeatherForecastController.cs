using Agify.BL.Abstract;
using Agify.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Agify.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IUserService _userService;

        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet(Name = "?name")]
        public async Task<IEnumerable<User>?> Name([FromQuery] string[] name)
        {
            var user = await _userService.Get(name);
            if (user != null)
                return user;
            else
                return null;
        }

    }
}