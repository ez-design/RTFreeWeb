using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RTFreeWeb.Entities;
using RTFreeWeb.Classes;
using System.Security.Principal;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RTFreeWeb.Controllers
{
    [Route("api/[controller]")]
    public class LogoutController : Controller
    {
        async public Task<object> Get()
        {
            await HttpContext.Authentication.SignOutAsync("Forms");
            return new { result = true };
        }
    }
}
