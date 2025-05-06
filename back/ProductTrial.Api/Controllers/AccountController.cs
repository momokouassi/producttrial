using Microsoft.AspNetCore.Mvc;
using ProductTrial.Data.Dtos;
using ProductTrial.Services.Interfaces;

namespace ProductTrial.Api.Controllers
{
    [ApiController]
    [Route("account")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IUserService _userService;

        public AccountController(ILogger<ProductController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateAsync([FromBody] UserCreationDto user)
        {
            UserDto res = await _userService.CreateAsync(user);
            return Ok(res);
        }
    }
}