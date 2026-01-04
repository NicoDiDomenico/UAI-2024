namespace MindFit.Domain.Entities;

public class Membership : Common.BaseEntity
{
    public Guid MemberId { get; set; }
    public Guid MembershipPlanId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Status { get; set; } = "Active"; // Active, Expired, Suspended, Cancelled
    public bool AutoRenewal { get; set; }

    // Navigation Properties
    public Member Member { get; set; } = null!;
    public MembershipPlan MembershipPlan { get; set; } = null!;
}
