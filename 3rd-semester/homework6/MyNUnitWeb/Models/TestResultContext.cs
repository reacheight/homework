using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNUnitWeb.Models
{
    /// <summary>
    /// Represents app main DbContext
    /// </summary>
    public class TestResultContext : DbContext
    {
        /// <summary>
        /// Set of assembly test result
        /// </summary>
        public DbSet<AssemblyTestResultModel> AssemblyTestResults { get; set; }

        /// <summary>
        /// Initializes new instance of <see cref="TestResultContext"/>
        /// checks if db is created,
        /// creates it if it's not created
        /// </summary>
        /// <param name="options">context option</param>
        public TestResultContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
