namespace JobTrackerAPI.Models
{
    public class job_applications
    {
        public int id { get; set; }
        public string position_title { get; set; }
        public string company { get; set; }
        public string website_link { get; set; }
        public string location { get; set; }
        public string? note { get; set; }
        public string application_status { get; set; }
        public string? date_applied { get; set; }
        public string? interview1_date { get; set; }
        public string? interview2_date { get; set; }
        public string? interview3_date { get; set; }
        public string? days_since_applying { get; set; }
        public string? days_since_interview1 { get; set; }
        public string? days_since_interview2 { get; set; }
        public string? days_since_interview3 { get; set; }
    }

}
