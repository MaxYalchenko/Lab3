using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab333.Models
{
    public class SubdivisionPlan
    {
        public int subdivisionPlanId { get; set; }
        public DateTime subdivisionPlanDate { get; set; }
        public int subdivisionPlanIndex { get; set; }
        public int subdivisionId { get; set; }
        public Subdivision subdivision { get; set; }

    }
}
