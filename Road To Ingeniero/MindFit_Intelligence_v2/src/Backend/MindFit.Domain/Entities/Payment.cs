namespace MindFit.Domain.Entities;

public class Payment : Common.BaseEntity
{
    public Guid MemberId { get; set; }
    public Guid MembershipId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string PaymentMethod { get; set; } = string.Empty; // Cash, Card, Transfer, etc.
    public string Status { get; set; } = "Pending"; // Pending, Completed, Failed, Refunded
    public string? TransactionId { get; set; }
    public string? Notes { get; set; }

    // Navigation Properties
    public Member Member { get; set; } = null!;
    public Membership Membership { get; set; } = null!;
}
