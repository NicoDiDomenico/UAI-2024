using MediatR;
using MindFit.Application.Contracts.Persistence;
using MindFit.Application.DTOs.Member;

namespace MindFit.Application.Features.Members.Queries.GetAllMembers;

public class GetAllMembersQueryHandler : IRequestHandler<GetAllMembersQuery, List<MemberDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllMembersQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<MemberDto>> Handle(GetAllMembersQuery request, CancellationToken cancellationToken)
    {
        var members = await _unitOfWork.Members.GetAllAsync();

        return members.Select(m => new MemberDto
        {
            Id = m.Id,
            FirstName = m.FirstName,
            LastName = m.LastName,
            Email = m.Email,
            Phone = m.Phone,
            Address = m.Address,
            DateOfBirth = m.DateOfBirth,
            EmergencyContact = m.EmergencyContact,
            EmergencyPhone = m.EmergencyPhone,
            ProfilePhoto = m.ProfilePhoto,
            MemberSince = m.MemberSince,
            IsActive = m.IsActive
        }).ToList();
    }
}
