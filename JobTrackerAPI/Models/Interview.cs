using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JobTrackerAPI.Models
{
    [Table("interviews")]
    public class Interview
    {
        [Key]
        [Column("interview_id")]
        public Guid InterviewId { get; set; }

        [Column("application_id")]
        public Guid ApplicationId { get; set; }

        [Column("interview_note", TypeName = "text")]
        public string? InterviewNote { get; set; }

        [Column("interview_date", TypeName = "date")]
        public DateTime? InterviewDate { get; set; }

        [ForeignKey("ApplicationId")]
        [InverseProperty("Interviews")]
        public virtual JobApplication Application { get; set; } = null!;
    }
}
