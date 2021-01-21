using BugsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BugsApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<BugModel> Bug { get; set; }
    }
}
