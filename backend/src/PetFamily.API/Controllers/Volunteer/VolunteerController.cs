using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Controllers.Volunteer.Requests;
using PetFamily.API.Extensions;
using PetFamily.API.Processors;
using PetFamily.Application.Volunteers.AddPetPhoto;
using PetFamily.Application.Volunteers.AddPets;
using PetFamily.Application.Volunteers.Create;
using PetFamily.Application.Volunteers.Delete;
using PetFamily.Application.Volunteers.UpdateMainInfo;
using PetFamily.Application.Volunteers.UpdateRequisites;
using PetFamily.Application.Volunteers.UpdateSocialNetworks;

namespace PetFamily.API.Controllers.Volunteer;

public class VolunteerController
    : BaseController
{
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
        [FromServices] IValidator<UpdateMainInfoCommand> validator,
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
        [FromServices] IValidator<UpdateSocialNetworkCommand> validator,
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
        [FromServices] IValidator<UpdateRequisiteCommand> validator,
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
        [FromServices] IValidator<DeleteVolunteerCommand> validator,
        CancellationToken cancellationToken)
    {
        var request = new DeleteVolunteerCommand(id);

        var result = await deleteVolunteerHandler.Handle(request, cancellationToken);

        return result.IsFailure
            ? result.Error.ToResponse()
            : Ok(result.Value);
    }

    [HttpPost("{id:guid}/pet")]
    public async Task<ActionResult> AddPet(
        [FromRoute] Guid id,
        [FromBody] AddPetRequest request,
        [FromServices] AddPetHandler addPetHandler,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(id);

        var result = await addPetHandler.Handle(command, cancellationToken);

        return result.IsFailure
            ? result.Error.ToResponse()
            : CreatedAtAction(nameof(AddPet), result.Value);
    }

    [HttpPost("{id:guid}/pet/{petId:guid}/photo")]
    public async Task<ActionResult> AddPhotoToPet([FromRoute] Guid id,
        [FromRoute] Guid petId,
        [FromForm] IFormFileCollection filesCollection,
        [FromServices] AddPetPhotoHandler addPetPhotoHandler,
        CancellationToken cancellationToken)
    {
        await using var fileProcessor = new FileProcessor();
        var files = fileProcessor.Process(filesCollection);

        var command = new AddPetPhotoCommand(id, petId, files);

        var result = await addPetPhotoHandler.Handle(command, cancellationToken);

        return result.IsFailure
            ? result.Error.ToResponse()
            : Ok(result.Value);
    }
}