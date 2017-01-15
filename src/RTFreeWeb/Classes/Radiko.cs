using RTFreeWeb.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RTFreeWeb.Classes
{
    public class Radiko
    {
        /// <summary>
        /// 放送局一覧取得
        /// </summary>
        /// <returns></returns>
        async static public Task<List<Station>> GetStations()
        {
            string xml = "";
            List<Station> res = new List<Station>();

            using (HttpClient hc = new HttpClient())
            {
                xml = await hc.GetStringAsync(AppSettings.StationList);
            }

            if (!string.IsNullOrWhiteSpace(xml))
            {
                
                XDocument doc = XDocument.Parse(xml);

                //放送局一覧
                int order = 1;
                foreach (var stations in doc.Descendants("stations"))
                {
                    string region_id   = stations.Attribute("region_id").Value;
                    string region_name = stations.Attribute("region_name").Value;

                    foreach (var station in stations.Descendants("station"))
                    {
                        string code = station.Descendants("id").First().Value;
                        string name = station.Descendants("name").First().Value;
                        string url  = station.Descendants("href").First().Value;
                        res.Add(new Station
                        {
                            Id         = code,
                            Name       = name,
                            RegionId   = region_id,
                            RegionName = region_name,
                            OrderNo    = order++
                        });
                    }
                }
            }
            return res;

        }

        /// <summary>
        /// 番組表取得
        /// </summary>
        /// <param name="station_id"></param>
        /// <returns></returns>
        async static public Task<List<Entities.Program>> GetPrograms(string station_id)
        {
            List<Entities.Program> res = new List<Entities.Program>();

            string xml = "";

            using (HttpClient hc = new HttpClient())
            {
                xml = await hc.GetStringAsync(AppSettings.ProgramList.Replace("{station_id}", station_id));
            }

            if (!string.IsNullOrWhiteSpace(xml))
            {
                XDocument doc = XDocument.Parse(xml);
                res = doc.Descendants("prog").Select(x =>
                                               {
                                                   return new Entities.Program
                                                   {
                                                       Id        = station_id + "_" + x.Attribute("ft").Value,
                                                       Start     = Utility.Text.StringToDate(x.Attribute("ft").Value),
                                                       End       = Utility.Text.StringToDate(x.Attribute("to").Value),
                                                       Title     = x.Element("title").Value.Trim(),
                                                       Info      = x.Element("info").Value.Trim(),
                                                       Cast = x.Element("pfm").Value.Trim(),
                                                       StationId = station_id
                                                   };
                                               }).ToList();
            }


            return res;

        }

        /// <summary>
        /// radikoプレミアムログインチェック
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        async static public Task<bool>LoginCheck(string mail, string pass)
        {
            bool res = false;

            if (!string.IsNullOrWhiteSpace(mail) && !string.IsNullOrWhiteSpace(pass))
            {
                CookieContainer cookie = new CookieContainer();
                // メール・パスワードがあればログイン
                try
                {
                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(AppSettings.RadikoLogin);

                    // ヘッダー
                    req.CookieContainer = cookie;
                    req.Method = "post";
                    req.Accept = "application/json";

                    // post
                    string post = "mail" + "=" + WebUtility.UrlEncode(mail) + "&" + "pass" + "=" + WebUtility.UrlEncode(pass);
                    byte[] data = Encoding.ASCII.GetBytes(post);

                    req.ContentType = "application/x-www-form-urlencoded";
                    req.Headers["ContentLength"] = Convert.ToString(data.Length);

                    using (Stream stream = await req.GetRequestStreamAsync())
                    {
                        stream.Write(data, 0, data.Length);
                    }

                    HttpWebResponse webres = (HttpWebResponse)(await req.GetResponseAsync());
                    using (var r = new StreamReader(webres.GetResponseStream(), Encoding.UTF8))
                    {
                        var ss = r.ReadToEnd();
                    }

                }
                catch (Exception e)
                {

                }

                try
                {
                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(AppSettings.RadikoLoginCheck);
                    req.CookieContainer = cookie;
                    HttpWebResponse webres = (HttpWebResponse)(await req.GetResponseAsync());
                    using (var r = new StreamReader(webres.GetResponseStream(), Encoding.UTF8))
                    {
                        var json = r.ReadToEnd();
                        if (json.Contains("areafree"))
                        {
                            res = true;
                        }

                    }
                }
                catch (Exception e)
                {

                }
            }

            
            return res;
        }

    }
}
