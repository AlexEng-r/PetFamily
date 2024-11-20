using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects.SocialNetworks;
using PetFamily.VolunteerManagement.Application.Repositories;

namespace PetFamily.VolunteerManagement.Application.Commands.UpdateSocialNetworks;

public class UpdateSocialNetworkHandler
    : ICommandHandler<UpdateSocialNetworkCommand, Guid>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IVolunteerUnitOfWork _volunteerUnitOfWork;
    private readonly ILogger<UpdateSocialNetworkHandler> _logger;
    private readonly IValidator<UpdateSocialNetworkCommand> _validator;

    public UpdateSocialNetworkHandler(
        IVolunteersRepository volunteersRepository,
        IVolunteerUnitOfWork volunteerUnitOfWork,
        ILogger<UpdateSocialNetworkHandler> logger,
        IValidator<UpdateSocialNetworkCommand> validator)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        _validator = validator;
        _volunteerUnitOfWork = volunteerUnitOfWork;
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

        await _volunteerUnitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Successfully updated volunteer #{Id}", command.VolunteerId);

        return volunteer.Value.Id.Value;
    }
}