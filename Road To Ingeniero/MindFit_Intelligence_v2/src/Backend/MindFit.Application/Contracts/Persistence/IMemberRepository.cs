using MindFit.Domain.Entities;

namespace MindFit.Application.Contracts.Persistence;

public interface IMemberRepository : IGenericRepository<Member>
{
    Task<Member?> GetByEmailAsync(string email);
    Task<IReadOnlyList<Member>> GetActiveMembersAsync();
    Task<IReadOnlyList<Member>> GetMembersWithExpiredMembershipsAsync();
}
