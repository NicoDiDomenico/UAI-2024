namespace MindFit.Domain.Entities;

public class MembershipPlan : Common.BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int DurationDays { get; set; }
    public int MaxClasses { get; set; }
    public bool HasPersonalTrainer { get; set; }
    public bool HasNutritionist { get; set; }
    public string? Benefits { get; set; }

    // Navigation Properties
    public ICollection<Membership> Memberships { get; set; } = new List<Membership>();
}
