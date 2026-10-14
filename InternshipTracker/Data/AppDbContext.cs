using InternshipTracker.Models;
using Microsoft.EntityFrameworkCore;
namespace InternshipTracker.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<JobApplication> JobApplications => Set<JobApplication>();
    
    }
}
