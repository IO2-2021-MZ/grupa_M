using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi.DataTransferObjects.ComplaintDTO;
using webApi.DataTransferObjects.UserDTO;
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
        private readonly IMapper _mapper;

        /// <summary>
        /// User Controller constructor
        /// </summary>
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
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
            var userRes = _mapper.Map<UserDTO>(user);
            return Ok(userRes);
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
        public IActionResult PostUser([FromBody] NewUserDTO newUserDTO)
        {
            var newUser = _mapper.Map<User>(newUserDTO);
            int id = _userService.CreateNewUser(newUser);
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
            _userService.DeleteUser(id);
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
            var ordersModels = _userService.GetAllUserOrders(id);
            var orders = _mapper.Map<IList<ComplaintDTO>>(ordersModels);
            return Ok(orders);
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
            var complaintsModels = _userService.GetAllUserOrders(id);
            var complaints = _mapper.Map<IList<ComplaintDTO>>(complaintsModels);
            return Ok(complaints);
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
            var usersModel = _userService.GetAllUsers();
            var users = _mapper.Map<UserDTO>(usersModel);
            return Ok(users);
        }


    }
}
