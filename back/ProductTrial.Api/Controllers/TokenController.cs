using Microsoft.AspNetCore.Mvc;
using ProductTrial.Data.Dtos;
using ProductTrial.Services.Interfaces;

namespace ProductTrial.Api.Controllers
{
    [ApiController]
    [Route("token")]
    public class TokenController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IUserService _userService;

        public TokenController(ILogger<ProductController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> LogInAsync([FromBody] UserConnectionDto user)
        {
            string res = await _userService.LogInAsync(user);
            return Ok(res);
        }
    }
}