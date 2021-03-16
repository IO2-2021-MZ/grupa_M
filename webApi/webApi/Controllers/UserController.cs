using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi.DataTransferObjects.User;
using webApi.Models;
using webApi.Services;

namespace webApi.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Returns user Details
        /// </summary>
        /// <param name="id"> User Id </param>
        /// <returns> Returns user Details </returns>
        /// <response code="200">Return User details</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">UnAuthorised</response>
        /// <response code="404">Resource Not Found</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUser([FromQuery] int? id)
        {

            var user = _userService.GetUserWithId(id);
            if (user == null)
            {
                return BadRequest("Resource not Found");
            }
            return Ok(user);
        }
        [HttpPost]
        public IActionResult PostUser([FromBody] NewUser newUser)
        {
            // Mapping exapmple
            return Ok();
        }
        [HttpDelete]
        public IActionResult DeleteUser([FromQuery] int? id)
        {
            return Ok();
        }
        [HttpGet("order/all")]
        public IActionResult GetAllOrders([FromQuery] int? id)
        {
            return Ok();
        }
        [HttpGet("complaint/all")]
        public IActionResult GetAllComplaint([FromQuery] int? id)
        {
            return Ok();
        }
        [HttpGet("all")]
        public IActionResult GetAllUsers()
        {
            return Ok();
        }


    }
}
