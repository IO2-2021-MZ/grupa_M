using Microsoft.AspNetCore.Mvc;
using webApi.DataTransferObjects.AuthenticateDTO;
using webApi.DataTransferObjects.UserDTO;
using webApi.Services;
namespace webApi.Controllers
{
    
    [ApiController]
    [Route("user/admin/login")]
    [Route("user/employee/login")]
    [Route("user/customer/login")]
    [Route("user/restaurer/login")]

    public class LoginController : AuthenticativeController
    {

        private readonly IAccountService _accountService;
        


        public LoginController(IAccountService service)
        {
            _accountService = service;
        }
        /// <summary>
        /// Zwraca dane o użytkowniku wraz z krótkoterminowym tokenem dostępu i 
        /// długoterminowym refresh tokenem umiesczonym w plikach cookies. 
        /// </summary>
        /// <param name="value"> Email i hasło użytkownika </param>
        /// <returns> Dane o użytkowniku </returns>
        [HttpGet]
        public IActionResult SignIn([FromQuery] AuthenticateRequest value)
        {
            
            var response = _accountService.Authenticate(value);
            return Ok(response);
        }
    }
}

