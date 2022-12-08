using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab333.Models
{
    public class SubdivisionFact
    {
        public int subdivisionFactId { get; set; }
        public DateTime subdivisionFactDate { get; set; }
        public int subdivisionFactIndex { get; set; }
        public int subdivisionId { get; set; }
        public Subdivision subdivision { get; set; }
    }
}
