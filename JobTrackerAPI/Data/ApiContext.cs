using Microsoft.EntityFrameworkCore;
using JobTrackerAPI.Models;
using Microsoft.Extensions.Configuration;


namespace JobTrackerAPI.Data
{
    public partial class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) 
            :base(options)
        {
        }

        public virtual DbSet<ApplicationStatusLookup> ApplicationStatusLookup { get; set; }

        public virtual DbSet<Interview> Interviews { get; set; }

        public virtual DbSet<JobApplication> JobApplications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Interview>(entity =>
            {
                entity.HasKey(e => e.InterviewId).HasName("PK__intervie__141E5552DC2452A7");

                entity.Property(e => e.InterviewId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Application).WithMany(p => p.Interviews)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_interviews_job_applications");
            });

            modelBuilder.Entity<JobApplication>(entity =>
            {
                entity.HasKey(e => e.ApplicationId).HasName("PK__job_appl__3BCBDCF2C32CD6F4");

                entity.Property(e => e.ApplicationId).HasDefaultValueSql("(newid())");

                //entity.HasOne(d => d.ApplicationStatusNavigation).WithMany(p => p.JobApplications).HasConstraintName("fk_application_status_lookup");
                entity.HasOne(d => d.ApplicationStatusNavigation);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }


}
