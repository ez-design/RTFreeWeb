using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RTFreeWeb.Entities
{
    public class Config
    {
        [Key]
        public string Id    { get; set; }
        public string Value { get; set; }
    }
}
