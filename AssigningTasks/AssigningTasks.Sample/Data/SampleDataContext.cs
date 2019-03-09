using System;
using Microsoft.EntityFrameworkCore;

namespace AssigningTasks.Sample.Data
{
    public class SampleDataContext : DbContext
    {
        public SampleDataContext(DbContextOptions<SampleDataContext> options) :
            base(options)
        {
        }

        public DbSet<Target> Targets { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
