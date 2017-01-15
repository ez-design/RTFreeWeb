using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RTFreeWeb.Entities;
using Microsoft.EntityFrameworkCore;
using RTFreeWeb.Classes;

namespace RTFreeWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            using (Db db = new Db())
            {
                string salt = db.Configs.Find(Define.Config.Salt)?.Value;
                string enc_key = db.Configs.Find(Define.Config.EncKey)?.Value;
                if (string.IsNullOrEmpty(salt) || string.IsNullOrEmpty(enc_key) || db.Users.Count() == 0)
                {
                    // 初期化未実行の場合
                    return Redirect("~/init/");
                }
               
            }

            return View();
        }
    }
}
