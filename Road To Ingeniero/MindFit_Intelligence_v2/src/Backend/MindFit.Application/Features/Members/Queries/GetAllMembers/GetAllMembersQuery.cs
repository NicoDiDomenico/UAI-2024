using MediatR;
using MindFit.Application.DTOs.Member;

namespace MindFit.Application.Features.Members.Queries.GetAllMembers;

public class GetAllMembersQuery : IRequest<List<MemberDto>>
{
}
