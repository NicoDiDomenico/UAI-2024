namespace MindFit.Domain.Entities;

public class ClassSchedule : Common.BaseEntity
{
    public Guid ClassId { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public string Room { get; set; } = string.Empty;
    public int AvailableSpots { get; set; }
    public string Status { get; set; } = "Scheduled"; // Scheduled, InProgress, Completed, Cancelled

    // Navigation Properties
    public Class Class { get; set; } = null!;
    public ICollection<ClassBooking> ClassBookings { get; set; } = new List<ClassBooking>();
}
