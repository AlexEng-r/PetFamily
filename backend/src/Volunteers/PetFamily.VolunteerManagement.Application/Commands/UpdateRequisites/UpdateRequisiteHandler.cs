using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects.Requisites;
using PetFamily.VolunteerManagement.Application.Repositories;

namespace PetFamily.VolunteerManagement.Application.Commands.UpdateRequisites;

public class UpdateRequisiteHandler
    : ICommandHandler<UpdateRequisiteCommand, Guid>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IVolunteerUnitOfWork _volunteerUnitOfWork;
    private readonly ILogger<UpdateRequisiteHandler> _logger;
    private readonly IValidator<UpdateRequisiteCommand> _validator;

    public UpdateRequisiteHandler(
        IVolunteersRepository volunteersRepository,
        IVolunteerUnitOfWork volunteerUnitOfWork,
        ILogger<UpdateRequisiteHandler> logger,
        IValidator<UpdateRequisiteCommand> validator)
    {
        _volunteersRepository = volunteersRepository;
        _volunteerUnitOfWork = volunteerUnitOfWork;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(UpdateRequisiteCommand command,
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

        var requisites = command.Requisites
            .Select(x => Requisite.Create(x.Name, x.Description))
            .Select(x => x.Value)
            .ToList();

        volunteer.Value.SetRequisites(requisites);

        await _volunteerUnitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Successfully updated volunteer #{Id}", command.VolunteerId);

        return volunteer.Value.Id.Value;
    }
}