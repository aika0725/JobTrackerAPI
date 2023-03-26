using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JobTrackerAPI.Models
{
    [Table("application_status_lookup")]
    public partial class ApplicationStatusLookup
    {
        [Key]
        [Column("status_value")]
        public int StatusValue { get; set; }

        [Column("status_text")]
        [StringLength(50)]
        [Unicode(false)]
        public string StatusText { get; set; } = null!;

        //[InverseProperty("ApplicationStatusNavigation")]
        //public virtual ICollection<JobApplication> JobApplications { get; } = new List<JobApplication>();
    }
}
