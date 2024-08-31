using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Repositories;
using PetFamily.Application.Repositories.Volunteers;
using PetFamily.Domain.SeedWork;

namespace PetFamily.Application.Volunteers.Delete;

public class DeleteVolunteerHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ICommonRepository _commonRepository;
    private readonly ILogger<DeleteVolunteerHandler> _logger;

    public DeleteVolunteerHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<DeleteVolunteerHandler> logger,
        ICommonRepository commonRepository)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        _commonRepository = commonRepository;
    }

    public async Task<Result<Guid, Error>> Handle(DeleteVolunteerRequest request,
        CancellationToken cancellationToken)
    {
        var volunteer = await _volunteersRepository.GetById(request.VolunteerId, cancellationToken);
        if (volunteer.IsFailure)
        {
            return volunteer.Error;
        }

        await _volunteersRepository.Delete(volunteer.Value, cancellationToken);
        await _commonRepository.SaveChanges(cancellationToken);

        _logger.LogInformation("Successfully updated volunteer #{Id}", request.VolunteerId);

        return volunteer.Value.Id.Value;
    }
}