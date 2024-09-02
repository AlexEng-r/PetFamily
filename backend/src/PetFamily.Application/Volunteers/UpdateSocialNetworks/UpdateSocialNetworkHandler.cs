using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Repositories;
using PetFamily.Application.Repositories.Volunteers;
using PetFamily.Domain.SeedWork;
using PetFamily.Domain.SocialNetworks;

namespace PetFamily.Application.Volunteers.UpdateSocialNetworks;

public class UpdateSocialNetworkHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ICommonRepository _commonRepository;
    private readonly ILogger<UpdateSocialNetworkHandler> _logger;

    public UpdateSocialNetworkHandler(
        IVolunteersRepository volunteersRepository,
        ICommonRepository commonRepository,
        ILogger<UpdateSocialNetworkHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _commonRepository = commonRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(UpdateSocialNetworkRequest request,
        CancellationToken cancellationToken)
    {
        var volunteer = await _volunteersRepository.GetById(request.VolunteerId, cancellationToken);
        if (volunteer.IsFailure)
        {
            return volunteer.Error;
        }

        var socialNetworks = request.Dto.SocialNetworks
            .Select(x => SocialNetwork.Create(x.Name, x.Link))
            .ToArray();

        volunteer.Value.SetSocialNetworks(socialNetworks.Select(x => x.Value));

        await _commonRepository.SaveChanges(cancellationToken);

        _logger.LogInformation("Successfully updated volunteer #{Id}", request.VolunteerId);

        return volunteer.Value.Id.Value;
    }
}