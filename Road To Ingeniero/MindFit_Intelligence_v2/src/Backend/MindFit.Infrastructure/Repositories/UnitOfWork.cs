using MindFit.Application.Contracts.Persistence;
using MindFit.Domain.Entities;
using MindFit.Infrastructure.Persistence;

namespace MindFit.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly MindFitDbContext _context;
    private IMemberRepository? _memberRepository;
    private IGenericRepository<MembershipPlan>? _membershipPlanRepository;
    private IGenericRepository<Membership>? _membershipRepository;
    private IGenericRepository<Trainer>? _trainerRepository;
    private IGenericRepository<Class>? _classRepository;
    private IGenericRepository<ClassSchedule>? _classScheduleRepository;
    private IGenericRepository<ClassBooking>? _classBookingRepository;
    private IGenericRepository<Payment>? _paymentRepository;
    private IGenericRepository<Attendance>? _attendanceRepository;
    private IGenericRepository<User>? _userRepository;

    public UnitOfWork(MindFitDbContext context)
    {
        _context = context;
    }

    public IMemberRepository Members =>
        _memberRepository ??= new MemberRepository(_context);

    public IGenericRepository<MembershipPlan> MembershipPlans =>
        _membershipPlanRepository ??= new GenericRepository<MembershipPlan>(_context);

    public IGenericRepository<Membership> Memberships =>
        _membershipRepository ??= new GenericRepository<Membership>(_context);

    public IGenericRepository<Trainer> Trainers =>
        _trainerRepository ??= new GenericRepository<Trainer>(_context);

    public IGenericRepository<Class> Classes =>
        _classRepository ??= new GenericRepository<Class>(_context);

    public IGenericRepository<ClassSchedule> ClassSchedules =>
        _classScheduleRepository ??= new GenericRepository<ClassSchedule>(_context);

    public IGenericRepository<ClassBooking> ClassBookings =>
        _classBookingRepository ??= new GenericRepository<ClassBooking>(_context);

    public IGenericRepository<Payment> Payments =>
        _paymentRepository ??= new GenericRepository<Payment>(_context);

    public IGenericRepository<Attendance> Attendances =>
        _attendanceRepository ??= new GenericRepository<Attendance>(_context);

    public IGenericRepository<User> Users =>
        _userRepository ??= new GenericRepository<User>(_context);

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
