using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Lab333.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace Lab333.Data
{
    public class Context : DbContext
    {
        public DbSet<Workpeople> Workpeoples { get; set; }
        public DbSet<SubdivisionFact> SubdivisionFacts { get; set; }
        public DbSet<SubdivisionPlan> SubdivisionPlans { get; set; }
        public DbSet<PeopleFact> PeopleFacts { get; set; }
        public DbSet<PeoplePlan> PeoplePlans { get; set; }
        public DbSet<Subdivision> Subdivisions { get; set; }
        public Context(DbContextOptions<Context> options) : base(options) { }
    }
}
