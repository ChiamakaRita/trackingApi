using Microsoft.EntityFrameworkCore;
using trackingApi.Models;

namespace trackingApi.Data
{
    public class IssueDbContext : DbContext
    {
        public IssueDbContext(DbContextOptions<IssueDbContext> options):base(options)
        {

        }
        public DbSet<Issue> Issues { get; set; }
     }
}
