using LogManager.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace LogManager.Data.Context
{
    public class LogManagerContext : DbContext
    {
        public LogManagerContext(DbContextOptions options)
            : base(options)
        {

        }
        
        public DbSet<RequestLog> RequestLog { get; set; }
    }
}
