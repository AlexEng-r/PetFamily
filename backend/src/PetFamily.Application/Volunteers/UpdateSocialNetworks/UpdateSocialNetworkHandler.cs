using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Application.Repositories.Volunteers;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects.SocialNetworks;

namespace PetFamily.Application.Volunteers.UpdateSocialNetworks;

public class UpdateSocialNetworkHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateSocialNetworkHandler> _logger;
    private readonly IValidator<UpdateSocialNetworkCommand> _validator;

    public UpdateSocialNetworkHandler(
        IVolunteersRepository volunteersRepository,
        IUnitOfWork unitOfWork,
        ILogger<UpdateSocialNetworkHandler> logger,
        IValidator<UpdateSocialNetworkCommand> validator)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> Handle(UpdateSocialNetworkCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorList();
        }
        
        var volunteer = await _volunteersRepository.GetById(command.VolunteerId, cancellationToken);
        if (volunteer.IsFailure)
        {
            return volunteer.Error.ToErrorList();
        }

        var socialNetworks = command.SocialNetworks
            .Select(x => SocialNetwork.Create(x.Name, x.Link))
            .Select(x => x.Value)
            .ToList();

        volunteer.Value.SetSocialNetworks(socialNetworks);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Successfully updated volunteer #{Id}", command.VolunteerId);

        return volunteer.Value.Id.Value;
    }
}