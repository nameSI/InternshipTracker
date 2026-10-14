using System.ComponentModel.DataAnnotations;
namespace InternshipTracker.Models;

public enum ApplicationStatus
{
    Wishlist,
    Applied,
    Interview,
    Offer,
    Rejected
}
public class JobApplication
{
    [Key]
    public int Id { get; set; }
    [Required, StringLength(100)]
    public string Company { get; set; } = "";
    [Required, StringLength(100)]
    public string Role { get; set; } = "";
    [Display(Name = "Job link"), Url]
    public string? JobUrl { get; set; }
    public ApplicationStatus Status { get; set; } = ApplicationStatus.Wishlist;
    [DataType(DataType.Date), Display(Name = "DAte Applied")]
    public DateTime? DateApplied { get; set; }
    [DataType(DataType.Date)]
    public DateTime? Deadline { get; set; }
    [StringLength(1000)]
    public string? Notes { get; set; }
}