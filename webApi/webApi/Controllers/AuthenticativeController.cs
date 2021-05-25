using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webApi.Models;

namespace webApi.Controllers
{
    [Controller]
    public abstract class AuthenticativeController : ControllerBase
    {
        public User Account => (User)HttpContext.Items["Account"];
    }
}