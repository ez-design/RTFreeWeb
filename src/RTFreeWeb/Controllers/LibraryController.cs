using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RTFreeWeb.Entities;
using RTFreeWeb.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RTFreeWeb.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class LibraryController : Controller
    {
        // 1ページあたりの表示件数
        private const int PER_PAGE = 10;

        /// <summary>
        /// ライブラリ一覧取得
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public object Get(int page = 1)
        {
            List<Library> libraries = new List<Library>();
            int count = 0;
            int max_page = 0;
            using (Db db = new Db())
            {
                libraries = db.Libraries.Select(l => new Library { Created = l.Created, Program = l.Program }).OrderByDescending(l => l.Created).Skip((page -1) * PER_PAGE ).Take(PER_PAGE).ToList();
                count = db.Libraries.Count();
                max_page = (int)((count - 1) / PER_PAGE) + 1;
            }

            return new { count = 0, maxPage = max_page, data = libraries.OrderByDescending(l => l.Program.Start) };
        }

        /// <summary>
        /// 実際のファイルにリダイレクト
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            Library library;
            using (Db db = new Db())
            {
                library = db.Libraries.Find(id);
            }

            if(library != null)
            {
                return Redirect("~/records/" + library.Path);
            }
            else
            {
                return null;
            }
        }

    }
}
