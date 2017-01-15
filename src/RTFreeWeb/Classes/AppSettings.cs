using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RTFreeWeb.Classes
{
    public class AppSettings
    {
        static public string ConnectionString { get; set; }
        static public string StationList      { get; set; }
        static public string ProgramList      { get; set; }
        static public string RTFreePath       { get; set; }

        /// <summary>
        /// radikoプレミアムログイン
        /// </summary>
        static public string RadikoLogin      { get; set; }

        /// <summary>
        /// radikoプレミアムログイン状態チェック
        /// </summary>
        static public string RadikoLoginCheck { get; set; }

    }
}
