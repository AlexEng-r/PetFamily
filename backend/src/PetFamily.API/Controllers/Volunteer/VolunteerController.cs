using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Controllers.Volunteer.Requests;
using PetFamily.API.Extensions;
using PetFamily.API.Processors;
using PetFamily.Application.VolunteerManagement.Commands.AddPetPhoto;
using PetFamily.Application.VolunteerManagement.Commands.AddPets;
using PetFamily.Application.VolunteerManagement.Commands.ChangePetPosition;
using PetFamily.Application.VolunteerManagement.Commands.Create;
using PetFamily.Application.VolunteerManagement.Commands.Delete;
using PetFamily.Application.VolunteerManagement.Commands.DeletePet;
using PetFamily.Application.VolunteerManagement.Commands.DeletePhoto;
using PetFamily.Application.VolunteerManagement.Commands.SetMainPhoto;
using PetFamily.Application.VolunteerManagement.Commands.UpdateMainInfo;
using PetFamily.Application.VolunteerManagement.Commands.UpdatePet;
using PetFamily.Application.VolunteerManagement.Commands.UpdatePetStatus;
using PetFamily.Application.VolunteerManagement.Commands.UpdateRequisites;
using PetFamily.Application.VolunteerManagement.Commands.UpdateSocialNetworks;
using PetFamily.Application.VolunteerManagement.Queries.GetPetById;
using PetFamily.Application.VolunteerManagement.Queries.GetPetsWithPagination;
using PetFamily.Application.VolunteerManagement.Queries.GetVolunteerById;
using PetFamily.Application.VolunteerManagement.Queries.GetVolunteersWithPagination;

namespace PetFamily.API.Controllers.Volunteer;

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

    [HttpGet("pets")]
    public async Task<IActionResult> GetPetsWithPagination([FromQuery] GetPetsWithPaginationRequest request,
        [FromServices] GetPetsWithPaginationHandler getPetsWithPaginationHandler,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand();

        var result = await getPetsWithPaginationHandler.Handle(command, cancellationToken);

        return result.IsFailure
            ? result.Error.ToResponse()
            : Ok(result.Value);
    }

    [HttpGet("pet/{petId:guid}")]
    public async Task<IActionResult> GetPetById([FromRoute] Guid petId,
        [FromServices] GetPetByIdHandler getPetByIdHandler,
        CancellationToken cancellationToken)
    {
        var result = await getPetByIdHandler.Handle(new GetPetByIdQuery(petId), cancellationToken);

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
    public async Task<IActionResult> AddPhotoToPet([FromRoute] Guid id,
        [FromRoute] Guid petId,
        [FromQuery] AddPetPhotoRequest request,
        [FromForm] IFormFileCollection filesCollection,
        [FromServices] AddPetPhotoHandler addPetPhotoHandler,
        CancellationToken cancellationToken)
    {
        await using var fileProcessor = new FileProcessor();
        var files = fileProcessor.Process(filesCollection);

        var command = request.ToCommand(id, petId, files);

        var result = await addPetPhotoHandler.Handle(command, cancellationToken);

        return result.IsFailure
            ? result.Error.ToResponse()
            : Ok(result.Value);
    }

    [HttpDelete("{id:guid}/pet/photos")]
    public async Task<ActionResult> DeletePhotosFromPet(
        [FromRoute] Guid id,
        [FromForm] DeletePhotoFromPetRequest request,
        [FromServices] DeletePhotoFromPetHandler handler,
        CancellationToken cancellationToken
    )
    {
        var result = await handler.Handle(request.ToCommand(id), cancellationToken);

        return result.IsFailure
            ? result.Error.ToResponse()
            : Ok();
    }

    [HttpPut("{id:guid}/pet/{petId:guid}/position")]
    public async Task<IActionResult> ChangePetPosition([FromRoute] Guid id,
        [FromRoute] Guid petId,
        [FromBody] ChangePetPositionRequest request,
        [FromServices] ChangePetPositionHandler changePetPositionHandler,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(id, petId);

        var result = await changePetPositionHandler.Handle(command, cancellationToken);

        return result.IsFailure
            ? result.Error.ToResponse()
            : Ok(result.Value);
    }

    [HttpPut("{id:guid}/pet")]
    public async Task<IActionResult> UpdatePet(
        [FromRoute] Guid id,
        [FromBody] UpdatePetRequest request,
        [FromServices] UpdatePetHandler updatePetHandler,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(id);

        var result = await updatePetHandler.Handle(command, cancellationToken);

        return result.IsFailure
            ? result.Error.ToResponse()
            : Ok(result.Value);
    }

    [HttpPut("{id:guid}/pet/status")]
    public async Task<IActionResult> UpdatePetStatus([FromRoute] Guid id,
        [FromQuery] UpdatePetStatusRequest request,
        [FromServices] UpdatePetStatusHandler updatePetStatusHandler,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(id);

        var result = await updatePetStatusHandler.Handle(command, cancellationToken);

        return result.IsFailure
            ? result.Error.ToResponse()
            : Ok();
    }

    [HttpDelete("{id:guid}/pet")]
    public async Task<IActionResult> DeletePet([FromRoute] Guid id,
        [FromQuery] DeletePetRequest request,
        [FromServices] DeletePetHandler deletePetHandler,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(id);

        var result = await deletePetHandler.Handle(command, cancellationToken);

        return result.IsFailure
            ? result.Error.ToResponse()
            : Ok();
    }

    [HttpPut("{id:guid}/pet/mainPhoto")]
    public async Task<IActionResult> SetMainPhoto([FromRoute] Guid id,
        [FromQuery] SetMainPhotoRequest request,
        [FromServices] SetMainPhotoHandler setMainPhotoHandler,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(id);

        var result = await setMainPhotoHandler.Handle(command, cancellationToken);

        return result.IsFailure
            ? result.Error.ToResponse()
            : Ok();
    }
}