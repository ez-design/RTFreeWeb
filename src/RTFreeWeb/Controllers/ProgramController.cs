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
    public class ProgramController : Controller
    {
        /// <summary>
        /// 番組表取得
        /// </summary>
        /// <param name="id"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet("{id}/{date}")]
        async public Task<IEnumerable<Entities.Program>> Get(string id, DateTime date)
        {
            List<Entities.Program> programs = new List<Entities.Program>();
            if(date == default(DateTime))
            {
                date = DateTime.Now;
            }
            DateTime start = date.Date.AddHours(5);
            DateTime end   = date.Date.AddDays(1).AddHours(5);

            using (Db db = new Db())
            {
                programs = db.Programs.Where(p => p.StationId == id && p.Start >= start && p.Start < end).ToList();
                if(programs.Count == 0)
                {
                    programs = await Radiko.GetPrograms(id);

                    // ライブラリに紐付かない古い番組データを削除
                    db.Programs.RemoveRange(db.Programs.Where(p => p.StationId == id && !db.Libraries.Any(l => l.ProgramId == p.Id)));

                    // xmlの取得範囲のデータを削除
                    db.Programs.RemoveRange(db.Programs.Where(p => p.StationId == id && p.Start >= programs.Min(p2 => p2.Start) && p.Start < programs.Max(p2 => p2.End)));
                    db.SaveChanges();

                    db.Programs.AddRange(programs);

                    programs = programs.Where(p => p.StationId == id && p.Start >= start && p.Start < end).ToList();

                    db.SaveChanges();
                }
               
            }
            return programs.OrderBy(p => p.Start);
        }

    }
}
