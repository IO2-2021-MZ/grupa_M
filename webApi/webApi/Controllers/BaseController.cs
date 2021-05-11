using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webApi.Models;

namespace BackEnd.Controllers
{
    [Controller]
    public abstract class BaseController : ControllerBase
    {
        public User Account => (User)HttpContext.Items["Account"];
    }
}