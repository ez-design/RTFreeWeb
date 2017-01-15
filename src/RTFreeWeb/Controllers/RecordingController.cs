using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using RTFreeWeb.Classes;
using System.IO;
using RTFreeWeb.Entities;
using Microsoft.AspNetCore.Authorization;
using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RTFreeWeb.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class RecordingController : Controller
    {
        static Process rtfree;
        private IHostingEnvironment env;

        public RecordingController(IHostingEnvironment env)
        {
            this.env = env;
        }

        [HttpGet("{id}/")]
        public object Get(string id)
        {
            // 多重起動は許可しない
            if(rtfree != null && !rtfree.HasExited)
            {
                return new { result = false, message = "RTFreeが稼働中です" };
            }


            Entities.Program program;
            Library library;
            string mail = "";
            string pass = "";

            using (Db db = new Db())
            {
                program = db.Programs.Find(id);
                library = db.Libraries.Find(id);

                string enc_key = db.Configs.Find(Define.Config.EncKey).Value;
                mail = db.Configs.Find(Define.Config.RadikoEmail)?.Value;
                pass = db.Configs.Find(Define.Config.RadikoPassword)?.Value;

                if(!string.IsNullOrWhiteSpace(mail) && !string.IsNullOrWhiteSpace(pass))
                {
                    // 復号化
                    mail = db.Configs.FromSql<Config>("SELECT 'email' AS Id,  CAST(AES_DECRYPT(UNHEX(@mail), @enc_key) AS CHAR) AS Value", new MySqlParameter { ParameterName = "mail", Value = mail }, new MySqlParameter { ParameterName = "enc_key", Value = enc_key }).First().Value;
                    pass = db.Configs.FromSql<Config>("SELECT 'pass' AS Id,  CAST(AES_DECRYPT(UNHEX(@pass), @enc_key) AS CHAR) AS Value", new MySqlParameter { ParameterName = "pass", Value = pass }, new MySqlParameter { ParameterName = "enc_key", Value = enc_key }).First().Value;

                }
            }

            if (program != null && library == null && (rtfree == null || rtfree.HasExited))
            {
                Directory.CreateDirectory(Path.Combine(this.env.WebRootPath, "records"));


                string filename = Guid.NewGuid().ToString();
                rtfree          = new Process();
                rtfree.StartInfo = new ProcessStartInfo()
                {
                    WorkingDirectory = Path.Combine(this.env.WebRootPath, "records"),
                    Arguments = $"{AppSettings.RTFreePath} -s {program.StationId} -f {program.Start.ToString("yyyyMMddHHmm")} -t {program.End.ToString("yyyyMMddHHmm")} -n {filename} -m {mail} -p {pass}",
                    FileName = "dotnet"
                };
                rtfree.Start();
                rtfree.WaitForExit();

                using (Db db = new Db())
                {
                    db.Libraries.Add(new Library { Path = filename + ".aac", ProgramId = program.Id, Created = DateTime.Now });
                    db.SaveChanges();
                }

            }

            return new { result = true };
        }

       
    }
}
