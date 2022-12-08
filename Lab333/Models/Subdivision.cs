using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab333.Models
{
    public class Subdivision
    {
        public int subdivisionId { get; set; }
        public string subdivisonName { get; set; }
        public int amountSubdivision { get; set; }
        ICollection<SubdivisionFact> subdivisionFacts { get; set; }
        ICollection<SubdivisionPlan> subdivisionPlans { get; set; }
        public Subdivision()
        {
            subdivisionFacts = new List<SubdivisionFact>();
            subdivisionPlans = new List<SubdivisionPlan>();
        }
    }
}
