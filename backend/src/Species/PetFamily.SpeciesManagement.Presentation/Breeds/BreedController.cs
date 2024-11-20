using Microsoft.AspNetCore.Mvc;
using PetFamily.Framework;
using PetFamily.Framework.Extensions;
using PetFamily.SpeciesManagement.Application.Commands.AddBreed;
using PetFamily.SpeciesManagement.Application.Commands.DeleteBreed;
using PetFamily.SpeciesManagement.Application.Queries.GetBreedsBySpeciesId;
using PetFamily.SpeciesManagement.Presentation.Request;

namespace PetFamily.SpeciesManagement.Presentation.Breeds;

public class BreedController
    : BaseController
{
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