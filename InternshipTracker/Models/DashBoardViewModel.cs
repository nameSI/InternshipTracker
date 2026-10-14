namespace InternshipTracker.Models
{
    public class DashBoardViewModel
    {
        public Dictionary<ApplicationStatus, int> Counts { get; set; } = new();
        public List<JobApplication> Upcoming { get; set; } = new();
    }
}
