using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab333.Models
{
    public class Workpeople
    {
        public int workpeopleId { get; set; }
        public string peopleName { get; set; }
        public int amountPeople { get; set; }
        public int subdivisionId { get; set; }
        public Subdivision subdivision { get; set; }
        public string Achievements { get; set; }

        ICollection<PeoplePlan> peoplePlans { get; set; }
        ICollection<PeopleFact> peopleFacts { get; set; }
        public Workpeople()
        {
            peoplePlans = new List<PeoplePlan>();
            peopleFacts = new List<PeopleFact>();
        }
    }
}
