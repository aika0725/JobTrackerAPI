using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JobTrackerAPI.Models
{
    [Table("job_applications")]
    public class JobApplication
    {
        [Key]
        [Column("application_id")]
        public Guid ApplicationId { get; set; }

        [Column("position_title")]
        [StringLength(255)]
        [Unicode(false)]
        public string PositionTitle { get; set; } = null!;

        [Column("company_name")]
        [StringLength(255)]
        [Unicode(false)]
        public string CompanyName { get; set; } = null!;

        [Column("location")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Location { get; set; }

        [Column("website")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Website { get; set; }

        [Column("note", TypeName = "text")]
        public string? Note { get; set; }

        [Column("applied_date", TypeName = "date")]
        public DateTime? AppliedDate { get; set; }

        [Column("application_status")]
        public int? ApplicationStatus { get; set; }

        [ForeignKey("ApplicationStatus")]
        //[InverseProperty("JobApplications")]
        public virtual ApplicationStatusLookup? ApplicationStatusNavigation { get; set; }

        [InverseProperty("Application")]
        public virtual ICollection<Interview> Interviews { get; } = new List<Interview>();
    }

}
