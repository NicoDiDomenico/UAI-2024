namespace MindFit.Domain.Entities;

public class Attendance : Common.BaseEntity
{
    public Guid MemberId { get; set; }
    public DateTime CheckInTime { get; set; }
    public DateTime? CheckOutTime { get; set; }
    public string? Notes { get; set; }

    // Navigation Properties
    public Member Member { get; set; } = null!;
}
