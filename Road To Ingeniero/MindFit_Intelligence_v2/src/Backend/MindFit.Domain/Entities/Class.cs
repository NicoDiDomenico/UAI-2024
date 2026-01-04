namespace MindFit.Domain.Entities;

public class Class : Common.BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid TrainerId { get; set; }
    public int MaxCapacity { get; set; }
    public int Duration { get; set; } // Duration in minutes
    public string Category { get; set; } = string.Empty; // Yoga, Cardio, Strength, etc.
    public string? ImageUrl { get; set; }

    // Navigation Properties
    public Trainer Trainer { get; set; } = null!;
    public ICollection<ClassSchedule> ClassSchedules { get; set; } = new List<ClassSchedule>();
}
