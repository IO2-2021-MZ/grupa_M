using Microsoft.AspNetCore.Mvc;
using webApi.DataTransferObjects.AuthenticateDTO;
using webApi.DataTransferObjects.UserDTO;
using webApi.Services;

namespace webApi.Controllers
{
    
    [ApiController]
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
        [HttpPost("authenticate")]
        public IActionResult SignIn([FromBody] AuthenticateRequest value)
        {
            var response = _accountService.Authenticate(value, ipAddress());
            return Ok(response);
        }
        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}

