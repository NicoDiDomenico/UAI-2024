using Microsoft.EntityFrameworkCore;
using MindFit.Application.Contracts.Persistence;
using MindFit.Domain.Entities;
using MindFit.Infrastructure.Persistence;

namespace MindFit.Infrastructure.Repositories;

public class MemberRepository : GenericRepository<Member>, IMemberRepository
{
    public MemberRepository(MindFitDbContext context) : base(context)
    {
    }

    public async Task<Member?> GetByEmailAsync(string email)
    {
        return await _context.Members.FirstOrDefaultAsync(m => m.Email == email);
    }

    public async Task<IReadOnlyList<Member>> GetActiveMembersAsync()
    {
        return await _context.Members
            .Where(m => m.IsActive)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Member>> GetMembersWithExpiredMembershipsAsync()
    {
        var today = DateTime.UtcNow;
        return await _context.Members
            .Include(m => m.Memberships)
            .Where(m => m.Memberships.Any(ms => ms.EndDate < today && ms.Status == "Active"))
            .ToListAsync();
    }
}
