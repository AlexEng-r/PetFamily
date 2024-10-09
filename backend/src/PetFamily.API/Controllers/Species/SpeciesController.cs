using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Controllers.Species.Request;
using PetFamily.API.Extensions;
using PetFamily.Application.SpeciesManagement.Commands.AddBreed;
using PetFamily.Application.SpeciesManagement.Commands.CreateSpecies;
using PetFamily.Application.SpeciesManagement.Commands.DeleteBreed;
using PetFamily.Application.SpeciesManagement.Commands.DeleteSpecies;
using PetFamily.Application.SpeciesManagement.Queries.GetBreedsBySpeciesId;
using PetFamily.Application.SpeciesManagement.Queries.GetSpeciesWithoutPagination;

namespace PetFamily.API.Controllers.Species;

public class SpeciesController
    : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAllSpecies(
        [FromServices] GetSpeciesWithoutPaginationHandler getSpeciesWithoutPaginationHandler,
        CancellationToken cancellationToken)
    {
        var result = await getSpeciesWithoutPaginationHandler.Handle(cancellationToken);

        return result.IsFailure
            ? result.Error.ToResponse()
            : Ok(result.Value);
    }

    [HttpGet("{id:guid}/breeds")]
    public async Task<IActionResult> GetBreeds([FromRoute] Guid id,
        [FromServices] GetBreedsBySpeciesIdHandler getBreedsBySpeciesIdHandler,
        CancellationToken cancellationToken)
    {
        var command = new GetBreedsBySpeciesIdQuery(id);

        var result = await getBreedsBySpeciesIdHandler.Handle(command, cancellationToken);

        return result.IsFailure
            ? result.Error.ToResponse()
            : Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSpeciesRequest request,
        [FromServices] CreateSpeciesHandler createSpeciesHandler,
        CancellationToken cancellationToken)
    {
        var result = await createSpeciesHandler.Handle(request.ToCommand(), cancellationToken);

        return result.IsFailure
            ? result.Error.ToResponse()
            : CreatedAtAction(nameof(Create), result.Value.ToString());
    }

    [HttpPost("{id:guid}/breed")]
    public async Task<IActionResult> AddBreed([FromRoute] Guid id,
        [FromBody] AddBreedRequest request,
        [FromServices] AddBreedHandler createBreedHandler,
        CancellationToken cancellationToken)
    {
        var result = await createBreedHandler.Handle(request.ToCommand(id), cancellationToken);

        return result.IsFailure
            ? result.Error.ToResponse()
            : Ok(result.Value.ToString());
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteSpecies([FromRoute] Guid id,
        [FromServices] DeleteSpeciesHandler deleteSpeciesHandler,
        CancellationToken cancellationToken)
    {
        var command = new DeleteSpeciesCommand(id);

        var result = await deleteSpeciesHandler.Handle(command, cancellationToken);
        return result.IsFailure
            ? result.Error.ToResponse()
            : Ok();
    }

    [HttpDelete("{id:guid}/breed")]
    public async Task<IActionResult> DeleteBreedFromSpecies([FromRoute] Guid id,
        [FromBody] DeleteBreedFromSpeciesRequest request,
        [FromServices] DeleteBreedFromSpeciesHandler deleteBreedFromSpeciesHandler,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(id);

        var result = await deleteBreedFromSpeciesHandler.Handle(command, cancellationToken);
        return result.IsFailure
            ? result.Error.ToResponse()
            : Ok();
    }
}