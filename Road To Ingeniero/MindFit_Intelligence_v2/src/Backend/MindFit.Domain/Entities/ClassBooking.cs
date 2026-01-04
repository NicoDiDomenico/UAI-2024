namespace MindFit.Domain.Entities;

public class ClassBooking : Common.BaseEntity
{
    public Guid MemberId { get; set; }
    public Guid ClassScheduleId { get; set; }
    public DateTime BookingDate { get; set; }
    public string Status { get; set; } = "Confirmed"; // Confirmed, Cancelled, Attended, NoShow

    // Navigation Properties
    public Member Member { get; set; } = null!;
    public ClassSchedule ClassSchedule { get; set; } = null!;
}
