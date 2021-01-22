using System;
using System.Threading.Tasks;
using Inventory.Domain.Entity;
using Inventory.Domain.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Inventory.Service.Controllers
{
    [Produces("application/xml")]
    [ApiController]
    [Route("TestDevWebService/services/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet("{idUser}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync(int idUser)
        {
            try
            {
                var user = await _userService.GetUserItemsAsync(idUser);
                if (user is null)
                    return NotFound();

                return Ok(user);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error getting items for userid '{idUser}'.";
                _logger.LogError(ex, errorMessage);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
