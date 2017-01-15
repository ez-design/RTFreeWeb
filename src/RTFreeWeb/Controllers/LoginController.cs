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
    public class LoginController : Controller
    {
        // POST api/values
        [HttpPost]
        async public Task<object> Post([FromBody]User user)
        {
            if (ModelState.IsValid)
            {
                using (Db db = new Db())
                {
                    User data = db.Users.Find(user.Id);
                    if (data != null)
                    {
                        string salt = db.Configs.Find(Define.Config.Salt)?.Value;
                        string pass = Utility.Text.CreatePassword(user.Password + salt);

                        if (data.Password == pass)
                        {
                            // 認証成功
                            var principal = new GenericPrincipal(new GenericIdentity(user.Id, "Forms"), null);
                            await HttpContext.Authentication.SignInAsync("Forms", principal);

                            return new { result = true };
                        }

                    }

                    return new { result = false, message = new string[] { "認証に失敗しました" } };
                }
            }
            else
            {
                return new { result = false, message = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray() };
            };

        }
    }
}
