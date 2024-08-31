using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Extensions;
using PetFamily.Application.Volunteers.Create;
using PetFamily.Application.Volunteers.UpdateMainInfo;
using PetFamily.Application.Volunteers.UpdateRequisites;
using PetFamily.Application.Volunteers.UpdateSocialNetworks;

namespace PetFamily.API.Controllers;

public class VolunteerController
    : BaseController
{
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateVolunteerRequest request,
        [FromServices] CreateVolunteersHandler createVolunteersHandler,
        CancellationToken cancellationToken)
    {
        var result = await createVolunteersHandler.Handle(request, cancellationToken);

        return result.IsFailure
            ? result.Error.ToResponse()
            : CreatedAtAction(nameof(Create), result.Value.ToString());
    }

    [HttpPut("{id:guid}/main-info")]
    public async Task<ActionResult> Update([FromRoute] Guid id,
        [FromBody] UpdateMainInfoDto dto,
        [FromServices] UpdateMainInfoHandler updateMainInfoHandler,
        [FromServices] IValidator<UpdateMainInfoRequest> validator,
        CancellationToken cancellationToken)
    {
        var request = new UpdateMainInfoRequest(id, dto);

        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToValidationErrorResponse();
        }

        var result = await updateMainInfoHandler.Handle(request, cancellationToken);

        return result.IsFailure
            ? result.Error.ToResponse()
            : Ok(result.Value);
    }

    [HttpPut("{id:guid}/social-networks")]
    public async Task<ActionResult> UpdateSocialNetworks([FromRoute] Guid id,
        [FromBody] UpdateSocialNetworkDto dto,
        [FromServices] UpdateSocialNetworkHandler updateSocialNetworkHandler,
        [FromServices] IValidator<UpdateSocialNetworkRequest> validator,
        CancellationToken cancellationToken)
    {
        var request = new UpdateSocialNetworkRequest(id, dto);

        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToValidationErrorResponse();
        }

        var result = await updateSocialNetworkHandler.Handle(request, cancellationToken);

        return result.IsFailure
            ? result.Error.ToResponse()
            : Ok(result.Value);
    }

    [HttpPut("{id:guid}/requisites")]
    public async Task<ActionResult> UpdateRequisites([FromRoute] Guid id,
        [FromBody] UpdateRequisiteDto dto,
        [FromServices] UpdateRequisiteHandler updateRequisiteHandler,
        [FromServices] IValidator<UpdateRequisiteRequest> validator,
        CancellationToken cancellationToken)
    {
        var request = new UpdateRequisiteRequest(id, dto);

        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToValidationErrorResponse();
        }

        var result = await updateRequisiteHandler.Handle(request, cancellationToken);

        return result.IsFailure
            ? result.Error.ToResponse()
            : Ok(result.Value);
    }
}