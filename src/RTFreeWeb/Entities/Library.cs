using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RTFreeWeb.Entities
{
    public class Library
    {
        [Key]
        public string ProgramId { get; set; }
        public string Path      { get; set; }
        public DateTime Created { get; set; }

        [ForeignKey("ProgramId")]
        public Program Program  { get; set; }
    }
}
