using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using webApi.DataTransferObjects.AddressDTO;
using webApi.DataTransferObjects.AuthenticateDTO;
using webApi.DataTransferObjects.ComplaintDTO;
using webApi.DataTransferObjects.OrderDTO;
using webApi.DataTransferObjects.RestaurantDTO;
using webApi.DataTransferObjects.UserDTO;
using webApi.Enums;
using webApi.Exceptions;
using webApi.Models;
using webApi.Services;

namespace webApi.Controllers
{
    /// <summary>
    /// User Controller
    /// </summary>
    [ApiController]
    [Route("user")]
    [EnableCors("AllowOrigin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public class UserController : AuthenticativeController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        /// <summary>
        /// User Controller constructor
        /// </summary>
        public UserController(IO2_RestaurantsContext context, IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [Authorize(Role.admin,Role.employee, Role.restaurateur)]
        [HttpGet("employee")]
        public IActionResult GetEmployee([FromQuery] int? id)
        {
            if(this.Account.Role == (int)Role.employee && Account.Id != id)
            {
                throw new  UnathorisedException("Not authorized Employye");
            }
            var user = _userService.GetUserWithId(id == null ? Account.Id : id);
            var response = _mapper.Map<Employee>(user);
            response.isRestaurateur = user.Role == (int)Role.restaurateur ? true : false;
            response.restaurant = user.Restaurant;
            return Ok(response);
        }
        [HttpPost("employee")]
        public IActionResult PostEmployee([FromBody] NewEmployee value)
        {
            var empolyee = _userService.CreateNewEmployee(value);
           
            return Ok(empolyee.Id);
        }
        [Authorize(Role.admin)]
        [HttpGet("admin")]
        public IActionResult GetAdmin([FromQuery] int? id)
        {
            if (Account.Id != id)
            {
                throw new UnathorisedException("Not authorized Employye");
            }
            var user = _userService.GetUserWithId(id == null ? Account.Id : id);
            var response = _mapper.Map<UserDTO>(user);
            return Ok(response);
        }
        [HttpPost("admin")]
        public IActionResult PostAdmin([FromBody] NewAdministrator value)
        {
            var admin = _userService.CreateNewAdmin(value);
            return Ok(admin.Id);
        }
        [Authorize(Role.admin, Role.customer)]
        [HttpGet("customer")]
        public IActionResult GetCustomer([FromQuery] int? id)
        {
            if (this.Account.Role == (int)Role.customer && Account.Id != id)
            {
                throw new UnathorisedException("Not authorized Employye");
            }
            var user = _userService.GetUserWithId(id == null ? Account.Id : id);
            var response =_mapper.Map<CustomerC>(user);
            response.address = _mapper.Map<AddressDTO>(user.Address);
            response.favouriteRestaurants = new List<RestaurantC>();
            foreach(var el in user.UserRests)
            {
                response.favouriteRestaurants.Add(_mapper.Map<RestaurantC>(el.Restaurant));
            }
            return Ok(response);
        }
        [HttpPost("customer")]
        public IActionResult PostCustomer([FromBody] NewCustomer value)
        {
            var customer = _userService.CreateNewCustomer(value);
            return Ok(customer.Id);
        }

        [HttpDelete]
        [Route("admin")]
        [Route("employee")]
        [Route("customer")]
        [Authorize(Role.admin)]
        public IActionResult DeleteEmployee([FromQuery] int id)
        {
             _userService.DeleteUser(id);
            return Ok();
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
        [Authorize(Role.admin)]
        [HttpGet]
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
        public IActionResult SignUp([FromBody] RegisterRequest value)
        {
            var response = _userService.CreateNewUser(value);
            return Ok(response);
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
        [Authorize(Role.admin)]
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
        [Authorize(Role.customer)]
        [HttpGet("customer/order/all")]
        public IActionResult GetAllOrders()
        {
            IEnumerable<OrderC> orders = _userService.GetAllUserOrders(Account.Id);
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
        [HttpGet("complaint/customer/all")] // TODO: zamienic complaint z customer
        [Authorize(Role.admin,Role.customer)]
        public IActionResult GetAllComplaint([FromQuery] int? id) 
        {
            if(Account.Role ==(int)Role.customer && id != null && id != Account.Id)
            {
                throw new UnathorisedException("Wrong Custoemr");
            }
            else if (Account.Role == (int)Role.admin && id == null)
            {
                throw new BadRequestException("Id is null");
            }
            var complaints = _userService.GetAllUserComplaint(id == null ? Account.Id : id);
            List<ComplaintDTO> response = new List<ComplaintDTO>();
            foreach(var el in complaints)
            {
                response.Add(_mapper.Map<ComplaintDTO>(el));
            }
            return Ok(response);
        }
        /// <summary>
        /// Returns all users
        /// </summary>
        /// <returns> Returns all users  </returns>
        /// <response code="200">Returns all users</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">UnAuthorised</response>
        /// <response code="404">Resource Not Found</response> 
        [Authorize(Role.admin)]
        [HttpGet("{role?}/all")]
        public IActionResult GetAllUsers(string role)
        {
            int userRole;
            switch (role)
            {
                case "customer":
                    userRole = 2;
                    break;
                case "employee":
                    userRole = 3;
                    break;
                case "admin":
                    userRole = 1;
                    break;
                default:
                    throw new BadRequestException("wrong role");
            }
            var users = _userService.GetAllUsers(userRole);
            return Ok(users);
        }
    }
}
