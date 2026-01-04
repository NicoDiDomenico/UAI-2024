using MediatR;
using MindFit.Application.Contracts.Persistence;
using MindFit.Application.DTOs.Member;
using MindFit.Domain.Entities;

namespace MindFit.Application.Features.Members.Commands.CreateMember;

public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, MemberDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateMemberCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<MemberDto> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        var member = new Member
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Phone = request.Phone,
            Address = request.Address,
            DateOfBirth = request.DateOfBirth,
            EmergencyContact = request.EmergencyContact,
            EmergencyPhone = request.EmergencyPhone,
            ProfilePhoto = request.ProfilePhoto,
            MemberSince = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        await _unitOfWork.Members.AddAsync(member);
        await _unitOfWork.SaveAsync();

        return new MemberDto
        {
            Id = member.Id,
            FirstName = member.FirstName,
            LastName = member.LastName,
            Email = member.Email,
            Phone = member.Phone,
            Address = member.Address,
            DateOfBirth = member.DateOfBirth,
            EmergencyContact = member.EmergencyContact,
            EmergencyPhone = member.EmergencyPhone,
            ProfilePhoto = member.ProfilePhoto,
            MemberSince = member.MemberSince,
            IsActive = member.IsActive
        };
    }
}
