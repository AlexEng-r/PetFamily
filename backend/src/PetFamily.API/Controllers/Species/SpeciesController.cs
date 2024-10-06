using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Controllers.Species.Request;
using PetFamily.API.Extensions;
using PetFamily.Application.SpeciesManagement.Commands.AddBreed;
using PetFamily.Application.SpeciesManagement.Commands.Create;

namespace PetFamily.API.Controllers.Species;

public class SpeciesController
    : BaseController
{
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
}