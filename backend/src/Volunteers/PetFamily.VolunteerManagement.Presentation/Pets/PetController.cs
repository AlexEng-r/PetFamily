using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetFamily.Framework;
using PetFamily.Framework.Extensions;
using PetFamily.VolunteerManagement.Application.Commands.AddPetPhoto;
using PetFamily.VolunteerManagement.Application.Commands.AddPets;
using PetFamily.VolunteerManagement.Application.Commands.ChangePetPosition;
using PetFamily.VolunteerManagement.Application.Commands.DeletePet;
using PetFamily.VolunteerManagement.Application.Commands.DeletePhoto;
using PetFamily.VolunteerManagement.Application.Commands.SetMainPhoto;
using PetFamily.VolunteerManagement.Application.Commands.UpdatePet;
using PetFamily.VolunteerManagement.Application.Commands.UpdatePetStatus;
using PetFamily.VolunteerManagement.Application.Queries.GetPetById;
using PetFamily.VolunteerManagement.Application.Queries.GetPetsWithPagination;
using PetFamily.VolunteerManagement.Presentation.Processors;
using PetFamily.VolunteerManagement.Presentation.Requests;

namespace PetFamily.VolunteerManagement.Presentation.Pets;

public class PetController
    : BaseController
{
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