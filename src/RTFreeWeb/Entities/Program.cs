using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RTFreeWeb.Entities
{
    public class Program
    {
        [Key]
        public string Id               { get; set; }
        public string StationId        { get; set; }
        public string Title            { get; set; }
        public string Cast             { get; set; }
        public string Info             { get; set; }
        [JsonIgnore]
        public DateTime Start          { get; set; }
        [JsonProperty(PropertyName = "start")]
        public string StartString
        {
            get { return this.Start.ToString("yyyy-MM-dd hh:mm:ss"); }
        }

        [JsonIgnore]
        public DateTime End            { get; set; }
        [JsonProperty(PropertyName="end")]
        public string EndString
        {
            get { return this.End.ToString("yyyy-MM-dd hh:mm:ss"); }
        }

        [ForeignKey("StationId")]
        public Station Station         { get; set; }

        public List<Library> Libraries { get; set; }

    }
}
