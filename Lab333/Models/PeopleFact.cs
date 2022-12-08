using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab333.Models
{
    public class PeopleFact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("peopleFactId")]
        public int peopleFactId { get; set; }
        public DateTime peopleFactDate { get; set; }
        public int peopleFactIndex { get; set; }
        public Workpeople workpeople { get; set; }
        public int workPeopleId { get; set; }

    }
}
