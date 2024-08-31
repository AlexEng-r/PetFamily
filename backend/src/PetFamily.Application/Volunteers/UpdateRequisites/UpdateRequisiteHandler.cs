using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Repositories;
using PetFamily.Application.Repositories.Volunteers;
using PetFamily.Domain.Requisites;
using PetFamily.Domain.SeedWork;

namespace PetFamily.Application.Volunteers.UpdateRequisites;

public class UpdateRequisiteHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ICommonRepository _commonRepository;
    private readonly ILogger<UpdateRequisiteHandler> _logger;

    public UpdateRequisiteHandler(
        IVolunteersRepository volunteersRepository,
        ICommonRepository commonRepository,
        ILogger<UpdateRequisiteHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _commonRepository = commonRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(UpdateRequisiteRequest request,
        CancellationToken cancellationToken)
    {
        var volunteer = await _volunteersRepository.GetById(request.VolunteerId, cancellationToken);
        if (volunteer.IsFailure)
        {
            return volunteer.Error;
        }

        var requisites = request.Dto.Requisites
            .Select(x => Requisite.Create(x.Name, x.Description))
            .ToArray();

        volunteer.Value.SetRequisites(requisites.Select(x => x.Value).ToArray());

        await _commonRepository.SaveChanges(cancellationToken);

        _logger.LogInformation("Successfully updated volunteer #{Id}", request.VolunteerId);

        return volunteer.Value.Id.Value;
    }
}