using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RTFreeWeb.Entities
{
    public class Station
    {
        [Key]
        public string Id              { get; set; }
        public string Name            { get; set; }
        public string RegionId        { get; set; }
        public string RegionName      { get; set; }
        public int OrderNo            { get; set; }

        public List<Program> Programs { get; set; }
    }
}
