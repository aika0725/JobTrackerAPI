using Microsoft.EntityFrameworkCore;
using JobTrackerAPI.Models;

namespace JobTrackerAPI.Data
{
    public class ApiContext : DbContext
    {
        public DbSet<job_applications> JobApplications { get; set; }
        public ApiContext(DbContextOptions<ApiContext> options) 
            :base(options)
        {
            
        }

    }


}
