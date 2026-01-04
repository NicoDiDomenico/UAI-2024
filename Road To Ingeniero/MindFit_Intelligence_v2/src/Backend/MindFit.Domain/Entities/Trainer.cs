namespace MindFit.Domain.Entities;

public class Trainer : Common.BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public string? Certifications { get; set; }
    public string? Bio { get; set; }
    public string? ProfilePhoto { get; set; }
    public DateTime HireDate { get; set; }
    public decimal HourlyRate { get; set; }

    // Navigation Properties
    public ICollection<Class> Classes { get; set; } = new List<Class>();
}
