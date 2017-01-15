using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RTFreeWeb.Entities;
using RTFreeWeb.Classes;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RTFreeWeb.Controllers
{
    public class InitController : Controller
    {
        public IActionResult Index()
        {
            using (Db db = new Db())
            {
                string salt    = db.Configs.Find(Define.Config.Salt)?.Value;
                string enc_key = db.Configs.Find(Define.Config.EncKey)?.Value;
                if (!string.IsNullOrEmpty(salt) && !string.IsNullOrEmpty(enc_key))
                {
                    // 初期化済みと判定
                }
                else
                {
                    // 初期化実行
                    salt = Guid.NewGuid().ToString();
                    enc_key = Guid.NewGuid().ToString();

                    db.Configs.Add(new Config { Id = Define.Config.Salt, Value = salt });
                    db.Configs.Add(new Config { Id = Define.Config.EncKey, Value = enc_key });

                    db.SaveChanges();
                }


                if(db.Users.Count() == 0)
                {
                    // ユーザー作成
                    return View();
                }
                else
                {
                    // 全て初期化済みと判定
                    return View("completed");

                }
            }
        }

        [HttpPost]
        public IActionResult Index(User user)
        {
            if(ModelState.IsValid)
            {
                using (Db db = new Db())
                {
                    string salt = db.Configs.Find(Define.Config.Salt).Value;
                    user.Password = Utility.Text.CreatePassword(user.Password + salt);
                    db.Users.Add(user);
                    db.SaveChanges();
                }
                return View("complete");

            }
            else
            {
                return View();
            }
        }
    }
}
