using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RTFreeWeb.Entities
{
    public class User
    {
        [Required(ErrorMessage = "IDを入力してください")]
        [Key]
        public string Id       { get; set; }

        [Required(ErrorMessage = "パスワードを入力してください")]
        public string Password { get; set; }
    }
}
