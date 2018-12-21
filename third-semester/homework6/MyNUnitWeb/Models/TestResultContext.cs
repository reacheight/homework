using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNUnitWeb.Models
{
    public class TestResultContext : DbContext
    {
        public DbSet<TestResultModel> TestResults { get; set; }
        public DbSet<AssemblyTestResultModel> AssemblyTestResults { get; set; }

        public TestResultContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
