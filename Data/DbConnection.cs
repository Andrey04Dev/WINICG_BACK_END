using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using webapi.Models;

namespace webapi.Data
{
    public class DbConnection
    {
        private readonly IConfiguration _configuration;

        public DbConnection(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection GetConnection() => new SqlConnection(_configuration.GetConnectionString("DbConnection"));
        //public DbConnection(DbContextOptions<DbConnection> options):base(options) { }

        //public DbSet<ROLES>? Roles { get; set; }
        //public DbSet<USERS>? Users { get; set; }
        //public DbSet<PROCESS>? Process { get; set; }
        //public DbSet<NO_ACCORDANCE>? No_Accordance { get; set; }
        //public DbSet<COMPLETE_PROCESS_TASK>? Complete_Process_Task { get; set; }
        //public DbSet<COMPANY_POSITION>? Company_Position { get; set; }
        //public DbSet<TASKS>? Tasks { get; set; }
        //public DbSet<RISKS>? Risks { get; set; }
        //public DbSet<ISORULE>? IsoRule { get; set; }
        //public DbSet<FLAGS>? Flags { get; set; }
        //public DbSet<CERTIFICATION>? Certification { get; set; }
        //public DbSet<AUDITS>? Audits { get; set; }
        //public DbSet<FILES>? Files { get; set; }
    }
}
