using Microsoft.AspNetCore.Mvc;
using PetFamily.Framework;
using PetFamily.Framework.Extensions;
using PetFamily.VolunteerManagement.Application.Commands.Create;
using PetFamily.VolunteerManagement.Application.Commands.Delete;
using PetFamily.VolunteerManagement.Application.Commands.UpdateMainInfo;
using PetFamily.VolunteerManagement.Application.Commands.UpdateRequisites;
using PetFamily.VolunteerManagement.Application.Commands.UpdateSocialNetworks;
using PetFamily.VolunteerManagement.Application.Queries.GetVolunteerById;
using PetFamily.VolunteerManagement.Application.Queries.GetVolunteersWithPagination;
using PetFamily.VolunteerManagement.Presentation.Requests;

namespace PetFamily.VolunteerManagement.Presentation.Volunteers;

public class VolunteerController
    : BaseController
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetVolunteersWithPaginationRequest request,
        [FromServices] GetVolunteersWithPaginationHandler handler, CancellationToken cancellationToken)
    {
        var query = request.ToQuery();

        var result = await handler.Handle(query, cancellationToken);
        return result.IsFailure
            ? result.Error.ToResponse()
            : Ok(result.Value);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id,
        [FromServices] GetVolunteerByIdHandler handler,
        CancellationToken cancellationToken)
    {
        var query = new GetVolunteerByIdQuery(id);
        var result = await handler.Handle(query, cancellationToken);

        return result.IsFailure
            ? result.Error.ToResponse()
            : Ok(result.Value);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateVolunteerRequest request,
        [FromServices] CreateVolunteersHandler createVolunteersHandler,
        CancellationToken cancellationToken)
    {
        var result = await createVolunteersHandler.Handle(request.ToCommand(), cancellationToken);

        return result.IsFailure
            ? result.Error.ToResponse()
            : CreatedAtAction(nameof(Create), result.Value.ToString());
    }

    [HttpPut("{id:guid}/main-info")]
    public async Task<ActionResult> Update([FromRoute] Guid id,
        [FromBody] UpdateMainInfoRequest request,
        [FromServices] UpdateMainInfoHandler updateMainInfoHandler,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(id);

        var result = await updateMainInfoHandler.Handle(command, cancellationToken);

        return result.IsFailure
            ? result.Error.ToResponse()
            : Ok(result.Value);
    }

    [HttpPut("{id:guid}/social-networks")]
    public async Task<ActionResult> UpdateSocialNetworks([FromRoute] Guid id,
        [FromBody] UpdateSocialNetworkRequest request,
        [FromServices] UpdateSocialNetworkHandler updateSocialNetworkHandler,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(id);

        var result = await updateSocialNetworkHandler.Handle(command, cancellationToken);

        return result.IsFailure
            ? result.Error.ToResponse()
            : Ok(result.Value);
    }

    [HttpPut("{id:guid}/requisites")]
    public async Task<ActionResult> UpdateRequisites([FromRoute] Guid id,
        [FromBody] UpdateRequisiteRequest request,
        [FromServices] UpdateRequisiteHandler updateRequisiteHandler,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(id);

        var result = await updateRequisiteHandler.Handle(command, cancellationToken);

        return result.IsFailure
            ? result.Error.ToResponse()
            : Ok(result.Value);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete([FromRoute] Guid id,
        [FromServices] DeleteVolunteerHandler deleteVolunteerHandler,
        CancellationToken cancellationToken)
    {
        var request = new DeleteVolunteerCommand(id);

        var result = await deleteVolunteerHandler.Handle(request, cancellationToken);

        return result.IsFailure
            ? result.Error.ToResponse()
            : Ok(result.Value);
    }
}