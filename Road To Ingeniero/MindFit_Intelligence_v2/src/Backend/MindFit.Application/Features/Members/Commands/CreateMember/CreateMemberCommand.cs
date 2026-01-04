using MediatR;
using MindFit.Application.DTOs.Member;

namespace MindFit.Application.Features.Members.Commands.CreateMember;

public class CreateMemberCommand : IRequest<MemberDto>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string EmergencyContact { get; set; } = string.Empty;
    public string EmergencyPhone { get; set; } = string.Empty;
    public string? ProfilePhoto { get; set; }
}
