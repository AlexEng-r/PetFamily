using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Extensions;
using PetFamily.Application.Volunteers.CreateVolunteer;

namespace PetFamily.API.Controllers;

public class VolunteerController
    : BaseController
{
    [HttpPost("create")]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateVolunteerRequest request,
        [FromServices] CreateVolunteersService createVolunteersService,
        CancellationToken cancellationToken)
    {
        var result = await createVolunteersService.Create(request, cancellationToken);
        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }

        return CreatedAtAction(nameof(Create), result.Value.ToString());
    }
}