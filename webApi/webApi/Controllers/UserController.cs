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
    /// <summary>
    /// User Controller
    /// </summary>
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        /// <summary>
        /// User Controller constructor
        /// </summary>
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
        /// <summary>
        /// Creates new User
        /// </summary>
        /// <param name="newUser"> New user data </param>
        /// <returns> Returns user  id  </returns>
        /// <response code="200">Returns user  id</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">UnAuthorised</response>
        /// <response code="404">Resource Not Found</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PostUser([FromBody] NewCustomer newUser)
        {
            // Mapping exapmple
            return Ok();
        }
        /// <summary>
        /// Delete user with exact id
        /// </summary>
        /// <param name="id"> User Id </param>
        /// <response code="200">Return Ok status </response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">UnAuthorised</response>
        /// <response code="404">Resource Not Found</response> 
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteUser([FromQuery] int? id)
        {
            return Ok();
        }
        /// <summary>
        /// Returns orders of exact user
        /// </summary>
        /// <param name="id"> User Id </param>
        /// <returns> Returns user Details </returns>
        /// <response code="200">Return user's orders </response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">UnAuthorised</response>
        /// <response code="404">Resource Not Found</response> 
        [HttpGet("order/all")]
        public IActionResult GetAllOrders([FromQuery] int? id)
        {
            return Ok();
        }
        /// <summary>
        /// Returns all complaints of user with exact id
        /// </summary>
        /// <param name="id"> User Id </param>
        /// <returns> List of Complaints </returns>
        /// <response code="200"> List of Complaints</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">UnAuthorised</response>
        /// <response code="404">Resource Not Found</response> 
        [HttpGet("complaint/all")]
        public IActionResult GetAllComplaint([FromQuery] int? id)
        {
            return Ok();
        }
        /// <summary>
        /// Returns all users
        /// </summary>
        /// <returns> Returns all users  </returns>
        /// <response code="200">Returns all users</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">UnAuthorised</response>
        /// <response code="404">Resource Not Found</response> 
        [HttpGet("all")]
        public IActionResult GetAllUsers()
        {
            return Ok();
        }


    }
}
