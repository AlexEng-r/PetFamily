using Microsoft.AspNetCore.Mvc;
using PetFamily.Framework;
using PetFamily.Framework.Extensions;
using PetFamily.SpeciesManagement.Application.Commands.CreateSpecies;
using PetFamily.SpeciesManagement.Application.Commands.DeleteSpecies;
using PetFamily.SpeciesManagement.Application.Queries.GetSpeciesWithoutPagination;
using PetFamily.SpeciesManagement.Presentation.Request;

namespace PetFamily.SpeciesManagement.Presentation.Species;

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
}