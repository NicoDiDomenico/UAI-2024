using MediatR;
using Microsoft.AspNetCore.Mvc;
using MindFit.Application.Features.Members.Commands.CreateMember;
using MindFit.Application.Features.Members.Queries.GetAllMembers;

namespace MindFit.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase
{
    private readonly IMediator _mediator;

    public MembersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var members = await _mediator.Send(new GetAllMembersQuery());
        return Ok(members);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMemberCommand command)
    {
        var member = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAll), new { id = member.Id }, member);
    }
}
