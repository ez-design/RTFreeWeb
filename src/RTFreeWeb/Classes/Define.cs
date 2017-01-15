using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RTFreeWeb.Classes
{
    public class Define
    {
        public class Config
        {
            public const string Salt = "salt";
            public const string EncKey = "enc_key";
            public const string RadikoEmail = "radiko_email";
            public const string RadikoPassword = "radiko_password";

            /// <summary>
            /// ログイン
            /// </summary>
            public const string Login = "https://radiko.jp/ap/member/login/login";

            /// <summary>
            /// ログイン状態確認
            /// </summary>
            public const string LoginCheck = "https://radiko.jp/ap/member/webapi/member/login/check";

        }
    }
}
