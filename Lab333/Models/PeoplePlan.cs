using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab333.Models
{
    public class PeoplePlan
    {
        public int peoplePlanId { get; set; }
        public DateTime peoplePlanDate { get; set; }
        public int peoplePlanIndex { get; set; }
        public Workpeople workpeople { get; set; }
        public int workPeopleId { get; set; }
    }
}
