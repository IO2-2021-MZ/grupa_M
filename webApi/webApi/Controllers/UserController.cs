using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi.Models;
using webApi.Services;

namespace webApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public IActionResult GetUser([FromQuery] int id)
        {
            
            var user = _userService.GetUserWithId(id);
            if(user == null)
            {
                return BadRequest("Resource not Found");
            }
            return Ok(user);
        }
        [HttpPost]
        public IActionResult PostUser()
        {
            return Ok();
        }
        [HttpDelete]
        public IActionResult DeleteUser()
        {
            return Ok();
        }
       
    }
}
