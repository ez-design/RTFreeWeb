using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RTFreeWeb.Entities;
using RTFreeWeb.Classes;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RTFreeWeb.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class StationController : Controller
    {
        /// <summary>
        /// 放送局一覧取得
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        async public Task<IEnumerable<Station>> Get()
        {
            List<Station> stations = new List<Station>();
            using (Db db = new Db())
            {
                stations = db.Stations.ToList();
                if(stations.Count == 0)
                {
                    stations = await Radiko.GetStations();
                    db.Stations.AddRange(stations);
                    db.SaveChanges();
                }
            }

            return stations.OrderBy(s => s.OrderNo);
        }

        /// <summary>
        /// 放送局一覧手動更新
        /// </summary>
        [HttpPost]
        async public Task<object> Post()
        {
            List<Station> stations = await Radiko.GetStations();
            using (Db db = new Db())
            {
                db.Stations.RemoveRange(db.Stations);
                db.SaveChanges();

                db.Stations.AddRange(stations);
                db.SaveChanges();
            }

            return new { result = true };
        }

    }
}
