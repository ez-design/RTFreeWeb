using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RTFreeWeb.Entities;
using RTFreeWeb.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RTFreeWeb.Controllers
{
  

    [Authorize]
    [Route("api/[controller]/[action]")]
    public class ConfigController : Controller
    {
        public class Password
        {
            public string password { get; set; }
        }

        public class RadikoData
        {
            public string RadikoEmail { get; set; }
            public string RadikoPassword { get; set; }
        }


        [HttpPost]
        public object Pass([FromBody]Password post)
        {
            if (!string.IsNullOrWhiteSpace(post.password))
            {
                using (Db db = new Db())
                {
                    string salt = db.Configs.Find(Define.Config.Salt).Value;

                    User user = db.Users.First();
                    user.Password = Utility.Text.CreatePassword(post.password + salt);
                    db.SaveChanges();
                }
                return new { result = true };
            }
            else
            {
                return new { result = false, message = "パスワードを入力してください" };
            }

            
        }


        [HttpPost]
        public object Radiko([FromBody]RadikoData post)
        {
            if (!string.IsNullOrWhiteSpace(post.RadikoEmail) && !string.IsNullOrWhiteSpace(post.RadikoPassword))
            {
                using (Db db = new Db())
                {
                    Config mail = db.Configs.Find(Define.Config.RadikoEmail);
                    Config pass = db.Configs.Find(Define.Config.RadikoPassword);
                    string enc_key = db.Configs.Find(Define.Config.EncKey).Value;

                    // 暗号化
                    post.RadikoEmail = db.Configs.FromSql<Config>("SELECT 'email' AS Id,  HEX(AES_ENCRYPT(@mail, @enc_key)) AS Value", new MySqlParameter { ParameterName = "mail", Value = post.RadikoEmail }, new MySqlParameter { ParameterName = "enc_key", Value = enc_key }).First().Value;
                    post.RadikoPassword = db.Configs.FromSql<Config>("SELECT 'pass' AS Id,  HEX(AES_ENCRYPT(@pass, @enc_key)) AS Value", new MySqlParameter { ParameterName = "pass", Value = post.RadikoPassword }, new MySqlParameter { ParameterName = "enc_key", Value = enc_key }).First().Value;


                    if (mail != null)
                    {
                        mail.Value = post.RadikoEmail;
                    }
                    else
                    {
                        db.Configs.Add(new Config { Id = Define.Config.RadikoEmail, Value = post.RadikoEmail });
                    }

                    if(pass != null)
                    {
                        pass.Value = post.RadikoPassword;
                    }
                    else
                    {
                        db.Configs.Add(new Config { Id = Define.Config.RadikoPassword, Value = post.RadikoPassword });
                    }
                    db.SaveChanges();
                }
                return new { result = true };
            }
            else
            {
                return new { result = false, message = "メールアドレスとパスワードを入力してください" };
            }


        }

        /// <summary>
        /// radikoプレミアムログインチェック
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        async public Task<object> Check([FromBody]RadikoData post)
        {
            if (!string.IsNullOrWhiteSpace(post.RadikoEmail) && !string.IsNullOrWhiteSpace(post.RadikoPassword))
            {
                bool res = await Classes.Radiko.LoginCheck(post.RadikoEmail, post.RadikoPassword);
                return new { result = res };
            }
            else
            {
                return new { result = false, message = "メールアドレスとパスワードを入力してください" };
            }


        }

    }
}
