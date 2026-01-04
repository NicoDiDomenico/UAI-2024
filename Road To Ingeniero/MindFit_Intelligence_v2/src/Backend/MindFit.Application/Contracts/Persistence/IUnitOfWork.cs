namespace MindFit.Application.Contracts.Persistence;

public interface IUnitOfWork : IDisposable
{
    IMemberRepository Members { get; }
    IGenericRepository<Domain.Entities.MembershipPlan> MembershipPlans { get; }
    IGenericRepository<Domain.Entities.Membership> Memberships { get; }
    IGenericRepository<Domain.Entities.Trainer> Trainers { get; }
    IGenericRepository<Domain.Entities.Class> Classes { get; }
    IGenericRepository<Domain.Entities.ClassSchedule> ClassSchedules { get; }
    IGenericRepository<Domain.Entities.ClassBooking> ClassBookings { get; }
    IGenericRepository<Domain.Entities.Payment> Payments { get; }
    IGenericRepository<Domain.Entities.Attendance> Attendances { get; }
    IGenericRepository<Domain.Entities.User> Users { get; }

    Task<int> SaveAsync();
}
